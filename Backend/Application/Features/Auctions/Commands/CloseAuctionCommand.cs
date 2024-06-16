using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Application.Features.Auctions.Dtos;
using Application.Common.Errors;
using Domain.Assets;
using Application.Features.Notifications.Dtos;
using Application.Contracts.Services;

namespace Application.Features.Auctions.Commands
{
    public class CloseAuctionCommand : IRequest<ErrorOr<bool>>
    {
        public AuctionEndedEventDto _event;
    }

    public class CloseAuctionCommandHandler
        : IRequestHandler<CloseAuctionCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CloseAuctionCommandHandler> _logger;
        private readonly INotificationService _notificationService;

        public CloseAuctionCommandHandler(IUnitOfWork unitOfWork, ILogger<CloseAuctionCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(
            CloseAuctionCommand command,
            CancellationToken cancellationToken
        )
        {

            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync((long)command._event.AuctionId);

            if (auction == null)
                return ErrorFactory.NotFound("Auction", "Auction not found");

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(auction.TokenId);

            if (asset == null)
                return ErrorFactory.NotFound("Asset", "Asset not found");

            var collection = await _unitOfWork.CollectionRepository.GetByIdAsync((long)asset.CollectionId);

            if (collection == null)
                return ErrorFactory.NotFound("Collection", "Collection not found");

            var user = await _unitOfWork.UserRepository.GetUserByAddress(command._event.Winner);
            if (user == null)
                return ErrorFactory.NotFound("User", "User not found");


            var oldOwnerId = asset.OwnerId;

            collection.LatestPrice = auction.HighestBid;
            asset.Status = AssetStatus.NotOnSale;
            asset.OwnerId = user.Id;

            // update  old owner  info
            await _unitOfWork.UserRepository.UpdateVolume(oldOwnerId, auction.HighestBid, 1);

            _unitOfWork.CollectionRepository.UpdateAsync(collection);
            _unitOfWork.AssetRepository.UpdateAsync(asset);

            if (await _unitOfWork.SaveAsync() == 0)
                return ErrorFactory.InternalServerError("Auction", "Error closing auction");

            var notificationForBuyer = new CreateNotificationDto
            {
                Title = "Auction Closed",
                Content = $"You have Won Auction on {asset.Name}",
                UserId = user.Id
            };

            var notificationForSeller = new CreateNotificationDto
            {
                Title = "Auction Closed",
                Content = $"Your Auction on {asset.Name} is closed, {user.UserName} has won the auction.",
                UserId = oldOwnerId
            };

            if (asset.Royalty != 0 && asset.CreatorId != oldOwnerId)
            {
                var royalty = asset.Royalty / 100 * auction.HighestBid;

                // update creator  info
                await _unitOfWork.UserRepository.UpdateVolume(asset.CreatorId, royalty);

                var notificationForCreator = new CreateNotificationDto
                {
                    Title = "Recieved Royalty",
                    Content = $"You have received {royalty} ETH as royalty for the sale of {asset.Name} asset.",
                    UserId = asset.CreatorId
                };

                await _notificationService.SendNotification(notificationForCreator);
            }

            await _notificationService.SendNotification(notificationForSeller);
            await _notificationService.SendNotification(notificationForBuyer);
            await _notificationService.NotifyRemoveAssetFromView(asset.Id);

            _logger.LogInformation($"\nCloseAuctionEvent\nAuctionId: {command._event.AuctionId}\nWinner: {command._event.Winner}");
            return true;
        }
    }
}

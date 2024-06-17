using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Application.Features.Auctions.Dtos;
using Application.Common.Errors;
using Domain.Assets;
using Application.Features.Notifications.Dtos;
using Application.Contracts.Services;
using Domain.Collections;


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

            var auction = await _unitOfWork.AuctionRepository.GetByAuctionId((long)command._event.AuctionId);

            // if (auction == null)
            //     return ErrorFactory.NotFound("Auction", "Auction not found");
            _logger.LogInformation("*********************************** 1");
            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(auction.TokenId);

            // if (asset == null)
            //     return ErrorFactory.NotFound("Asset", "Asset not found");
            _logger.LogInformation("*********************************** 2");

            Collection collection = null;
            if (asset.CollectionId != null)
            {
                collection = await _unitOfWork.CollectionRepository.GetByIdAsync((long)asset.CollectionId);
            }

            _logger.LogInformation("*********************************** 3");

            var user = await _unitOfWork.UserRepository.GetUserByAddress(command._event.Winner);
            // if (user == null)
            //     return ErrorFactory.NotFound("User", "User not found");
            _logger.LogInformation("*********************************** 4");

            var oldOwnerId = asset.OwnerId;
            if (collection != null)
            {
                collection.LatestPrice = auction.HighestBid;
                collection.Volume += auction.HighestBid;
                _unitOfWork.CollectionRepository.UpdateAsync(collection);
            }

            _logger.LogInformation("*********************************** 5");
            asset.Status = AssetStatus.NotOnSale;
            asset.OwnerId = user.Id;

            // update  old owner  info
            await _unitOfWork.UserRepository.UpdateVolume(oldOwnerId, auction.HighestBid, 1);

            _unitOfWork.AssetRepository.UpdateAsync(asset);
            _logger.LogInformation("*********************************** 6");
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
                Content = $"Your Auction on {asset.Name} is closed, {user.Profile.UserName} has won the auction.",
                UserId = oldOwnerId
            };

            await _unitOfWork.SaveAsync();

            if (asset.Royalty != 0 && asset.CreatorId != oldOwnerId)
            {
                var royalty = asset.Royalty / 100 * auction.HighestBid;

                // update creator  info
                await _unitOfWork.UserRepository.UpdateVolume(asset.CreatorId, royalty);
                await _unitOfWork.SaveAsync();

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

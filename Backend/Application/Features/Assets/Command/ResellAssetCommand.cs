using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Assets;
using Application.Common.Errors;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;
using Nethereum.Web3;
using Domain.Auctions;

namespace Application.Features.Auctions.Commands
{
    public class ResellAssetCommand : IRequest<ErrorOr<bool>>
    {
        public ResellAssetEventDto _event;
    }

    public class ResellAssetCommandHandler
        : IRequestHandler<ResellAssetCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResellAssetCommandHandler> _logger;
        private readonly INotificationService _notificationService;
        private readonly IAuctionManagementService _auctionManager;

        public ResellAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<ResellAssetCommandHandler> logger, INotificationService notificationService, IAuctionManagementService auctionManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
            _auctionManager = auctionManager;
        }

        public async Task<ErrorOr<bool>> Handle(
            ResellAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var evenData = command._event;

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId((long)evenData.TokenId);
            if (asset == null) return ErrorFactory.NotFound("Asset", "Asset Not Found");

            asset.Price =  (double)Web3.Convert.FromWei(evenData.Price);

            if (evenData.Auction && evenData.AuctionId != 0 && evenData.AuctionEnd != 0)
            {
                var auction = asset.Auction;
                if (asset.Auction == null) {
                    auction = new Auction {};
                }

                auction.AuctionId = (long)evenData.AuctionId;
                auction.Seller = asset.Owner;
                auction.FloorPrice = asset.Price;
                auction.AuctionEnd = (long)evenData.AuctionEnd;
                auction.HighestBid = asset.Price;
                auction.TokenId = asset.TokenId;
                auction.JobId =  _auctionManager.Schedule(auction.Seller.Address, auction.AuctionId, auction.AuctionEnd);

                if (asset.Auction != null) {
                    _unitOfWork.AuctionRepository.UpdateAsync(auction);
                } else {
                    auction = await _unitOfWork.AuctionRepository.AddAsync(auction);
                    asset.Auction = auction;
                }
                asset.Status = AssetStatus.OnAuction;
            }
            else
            {
                asset.Status = AssetStatus.OnFixedSale;
            }

            _unitOfWork.AssetRepository.UpdateAsync(asset);
            await _unitOfWork.SaveAsync();

            var notification = new CreateNotificationDto
            {
                UserId = asset.OwnerId,
                Title = "ReSell",
                Content = $"Your asset {asset.Name} is on sell by {asset.Price}",
            };

            var followers = await _unitOfWork.UserRepository.GetAllFollowersAsync(asset.Owner.Address);

            var notificationDto = new CreateNotificationDto
            {
                Title = "New Asset",
                Content = $"{asset.Owner.Profile.UserName} has set asset {asset.Name} on sell",
            };
            await _notificationService.SendNotificationsForMultipleUsers(followers.Select(x => x.Id).ToList(), notificationDto);
            await _notificationService.SendNotification(notification);

            _logger.LogInformation($"\nResellAssetEvent\nTokenID: {command._event.TokenId}\nAuction: {command._event.Auction}\nPrice: {command._event.Price}\nAuctionId: {command._event.AuctionId}\nAuctionEnd: {command._event.AuctionEnd}\n");
            return true;
        }
    }
}

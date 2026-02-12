using MediatR;
using Application.Contracts.Persistance;
using ErrorOr;
using Domain.Assets;
using Application.Contracts.Services;
using Application.Features.Auctions.Dtos;
using Application.Features.Notifications.Dtos;
using Nethereum.Web3;

namespace Application.Features.Assets.Command
{
    public class CancelAuctionAssetCommand : IRequest<ErrorOr<Unit>>
    {
        public AuctionCancelledEventDto _event;
    }


    public class CancelAuctionAssetCommandHandler : IRequestHandler<CancelAuctionAssetCommand, ErrorOr<Unit>>
    {
        private readonly IAuctionManagementService _auctionManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public CancelAuctionAssetCommandHandler(IUnitOfWork unitOfwork, INotificationService notificationService, IAuctionManagementService auctionManager)
        {
            _auctionManager = auctionManager;
            _unitOfWork = unitOfwork;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Unit>> Handle(CancelAuctionAssetCommand request, CancellationToken cancellationToken)
        {

            var asset = await _unitOfWork.AssetRepository.GetAssetByAuctionId((long)request._event.AuctionId);

            if (asset != null)
            {
                var higestBidder = await _unitOfWork.UserRepository.GetUserByAddress(request._event.HighestBidder);

                asset.AuctionId = null;
                asset.Auction = null;
                asset.Status = AssetStatus.NotOnSale;
                _unitOfWork.AssetRepository.UpdateAsync(asset);
                await _unitOfWork.SaveAsync();

                var hightestBid = (double)Web3.Convert.FromWei(request._event.HighestBid);

                var notificationDto = new CreateNotificationDto
                {
                    Title = "Auction Cancelled",
                    Content = $"Due to Auction cancel on {asset.Name} you are refunded {hightestBid} ETH",
                    UserId = higestBidder.Id
                };

                await _notificationService.SendNotification(notificationDto);
                await _notificationService.NotifyRemoveAssetFromView(asset.Id);
                _auctionManager.CancelAuction(asset.Auction.JobId);
            }


            return Unit.Value;
        }
    }
}
using ErrorOr;
using MediatR;
using Domain.Bids;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;
using Application.Common.Errors;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;

namespace Application.Features.Auctions.Commands
{
    public class PlaceBidCommand : IRequest<ErrorOr<bool>>
    {
        public BidPlacedEventDto _event;
    }

    public class PlaceBidCommandHandler
        : IRequestHandler<PlaceBidCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlaceBidCommandHandler> _logger;
        private readonly INotificationService _notificationService;

        public PlaceBidCommandHandler(IUnitOfWork unitOfWork, ILogger<PlaceBidCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        private static double WeiToEther(BigInteger wei)
        {
            return ((long)wei) / 1e+18;
        }

        public async Task<ErrorOr<bool>> Handle(
            PlaceBidCommand command,
            CancellationToken cancellationToken
        )
        {
            var bidder = await _unitOfWork.UserRepository.GetUserByAddress(command._event.Bidder);
            var asset = await _unitOfWork.AssetRepository.GetAssetByAuctionId((long)command._event.AuctionId);

            _logger.LogInformation($"\nBidPlacedEvent\nAuctionId: {command._event.AuctionId}\nAmount: {command._event.Amount}");

            Bid newBid = new()
            {
                BidderId = bidder.Id,
                AssetId = asset.Id,
                Amount = WeiToEther(command._event.Amount),
                TransactionHash = command._event.TransactionHash
            };
            await _unitOfWork.BidRepository.AddAsync(newBid);

            var auction = await _unitOfWork.AuctionRepository.GetByAuctionId((long)command._event.AuctionId);
            auction.HighestBid = WeiToEther(command._event.Amount);
            auction.HighestBidderId = bidder.Id;

            _unitOfWork.AuctionRepository.UpdateAsync(auction);

            if (await _unitOfWork.SaveAsync() == 0)
            {
                return ErrorFactory.InternalServerError("Bid", "Error Processing BidPlacedEvent");
            }

            var notificationFoOwner = new CreateNotificationDto
            {
                UserId = asset.OwnerId,
                Title = "Bid Placed",
                Content = $"New bid of amount {newBid.Amount} on {asset.Name}"
            };

            await _notificationService.SendNotification(notificationFoOwner);
            await _notificationService.NotifyAssetBidsRefetch(asset.Id);

            return true;
        }
    }
}

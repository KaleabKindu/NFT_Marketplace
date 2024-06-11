using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Assets;
using Application.Common.Errors;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;

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

        public ResellAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<ResellAssetCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(
            ResellAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var evenData = command._event;

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(evenData.TokenId);
            if (asset == null) return ErrorFactory.NotFound("Asset", "Asset Not Found");

            asset.Price = (double)evenData.Price;

            if (evenData.Auction && evenData.AuctionId != 0 && evenData.AuctionEnd == null)
            {
                asset.Auction.AuctionId = (long)evenData.AuctionId;
                asset.AuctionId = asset.Auction.AuctionId;
                asset.Auction.AuctionEnd = (long)evenData.AuctionEnd;

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
            await _notificationService.SendNotification(notification);

            _logger.LogInformation($"\nResellAssetEvent\nTokenID: {command._event.TokenId}\nAuction: {command._event.Auction}\nPrice: {command._event.Price}\nAuctionEnd: {command._event.AuctionEnd}\n");
            return true;
        }
    }
}

using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Provenances;
using Application.Common.Errors;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;
using Domain.Assets;

namespace Application.Features.Auctions.Commands
{
    public class SellAssetCommand : IRequest<ErrorOr<bool>>
    {
        public AssetSoldEventDto _event;
    }

    public class SellAssetCommandHandler
        : IRequestHandler<SellAssetCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SellAssetCommandHandler> _logger;
        private readonly INotificationService _notificationService;

        public SellAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<SellAssetCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(
            SellAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId((long)command._event.TokenId);

            if (asset == null) return ErrorFactory.NotFound("Asset", "Asset not found");

            var buyerUser = await _unitOfWork.UserRepository.GetUserByAddress(command._event.To);

            if (buyerUser == null) return ErrorFactory.NotFound("User", "User not found");


            if (asset.CollectionId != null)
            {
                var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(asset.CollectionId ?? 0);

                if (collection != null)
                {
                    collection.LatestPrice = asset.Price;
                    _unitOfWork.CollectionRepository.UpdateAsync(collection);
                }
            }

            var oldOwnerId = asset.OwnerId;

            asset.OwnerId = buyerUser.Id;
            asset.Status = AssetStatus.NotOnSale;


            // create provinance
            var provenance = new Provenance
            {
                AssetId = asset.Id,
                FromId = oldOwnerId,
                ToId = buyerUser.Id,
                Event = Event.Sale,
                Price = asset.Price,
                TransactionHash = command._event.TransactionHash
            };
            await _unitOfWork.UserRepository.UpdateVolume(asset.OwnerId, asset.Price, 1);
            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);
            _unitOfWork.AssetRepository.UpdateAsync(asset);
            await _unitOfWork.SaveAsync();

            if (asset.Royalty != 0 && asset.CreatorId != oldOwnerId)
            {
                var royalty = asset.Price * asset.Royalty / 100;
                await _unitOfWork.UserRepository.UpdateVolume(asset.CreatorId, royalty);
                var creatorNotification = new CreateNotificationDto
                {
                    Title = "Get Royalty",
                    Content = $"You have received {royalty} ETH as royalty for the sale of {asset.Name}.",
                    UserId = asset.CreatorId
                };
                await _notificationService.SendNotification(creatorNotification);
            }

            var notificationForSeller = new CreateNotificationDto
            {
                Title = "Asset Sold",
                Content = $"Your Asset {asset.Name} has been sold to {buyerUser.Profile.UserName} by {asset.Price} ETH.",
                UserId = oldOwnerId,
            };

            await _notificationService.SendNotification(notificationForSeller);
            await _notificationService.NotifyRemoveAssetFromView(asset.Id);

            _logger.LogInformation($"\nAssetSoldEvent\nTokenID: {command._event.TokenId}\nTo: {command._event.To}");
            return true;
        }
    }
}

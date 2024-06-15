using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Provenances;
using Application.Common.Errors;
using Application.Contracts.Services;
using Application.Features.Notifications.Dtos;

namespace Application.Features.Auctions.Commands
{
    public class TransferAssetCommand : IRequest<ErrorOr<bool>>
    {
        public TransferAssetEventDto _event;
    }

    public class TransferAssetCommandHandler
        : IRequestHandler<TransferAssetCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TransferAssetCommandHandler> _logger;
        private readonly INotificationService _notificationService;

        public TransferAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<TransferAssetCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(
            TransferAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var eventData = command._event;

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(eventData.TokenId);
            if (asset == null) return ErrorFactory.NotFound("Asset", "Asset Not Found");

            var newOwner = await _unitOfWork.UserRepository.GetUserByAddress(eventData.NewOwner);
            if (newOwner == null) return ErrorFactory.NotFound("User", "User Not Found");

            var oldOwner = asset.Owner; ;
            asset.OwnerId = newOwner.Id;

            var provenance = new Provenance
            {
                AssetId = asset.Id,
                FromId = oldOwner.Id,
                ToId = newOwner.Id,
                Event = Event.Transfer,
                Price = 0,
                TransactionHash = command._event.TransactionHash
            };

            _unitOfWork.AssetRepository.UpdateAsync(asset);
            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);
            await _unitOfWork.SaveAsync();


            var notification = new CreateNotificationDto
            {
                UserId = newOwner.Id,
                Title = "Transfer Asset",
                Content = $"You have received asset ${asset.Name} from  address ${oldOwner.Address} by transfer.",
            };

            await _notificationService.SendNotification(notification);
            await _notificationService.NotifyAssetRefetch(asset.Id);
            await _notificationService.NotifyAssetProvenanceRefetch(asset.Id);

            _logger.LogInformation($"\nTransferAssetEvent\nTokenID: {command._event.TokenId}\nNewOwner: {command._event.NewOwner}");
            return true;
        }
    }
}

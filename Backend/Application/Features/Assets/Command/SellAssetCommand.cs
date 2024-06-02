using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Provenances;
using Application.Common.Errors;

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


        public SellAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<SellAssetCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            SellAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var assetResponse = await _unitOfWork.AssetRepository.SellAsset(command._event.TokenId);

            if (assetResponse.IsError) return assetResponse.Errors;

            var buyerUser = await _unitOfWork.UserRepository.GetUserByAddress(command._event.To);

            if (buyerUser == null) return ErrorFactory.NotFound("User", "User not found");

            var asset = assetResponse.Value;

            // create provinance
            var provenance = new Provenance
            {
                AssetId = asset.Id,
                FromId = asset.OwnerId,
                ToId = buyerUser.Id,
                Event = Event.Sale,
                Price = asset.Price,
                TransactionHash = command._event.TransactionHash
            };

            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"\nAssetSoldEvent\nTokenID: {command._event.TokenId}\nTo: {command._event.To}");
            return true;
        }
    }
}

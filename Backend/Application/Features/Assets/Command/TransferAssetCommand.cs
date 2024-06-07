using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Domain.Provenances;
using Domain.Assets;

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


        public TransferAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<TransferAssetCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        public async Task<ErrorOr<bool>> Handle(
            TransferAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            var assetReponse = await _unitOfWork.AssetRepository.TransferAsset(command._event);

            if (assetReponse.IsError) return assetReponse.Errors;

            string oldOwnerId = assetReponse.Value.Item1;
            Asset asset = assetReponse.Value.Item2;

            var provenance = new Provenance
            {
                AssetId = asset.Id,
                FromId = oldOwnerId,
                ToId = asset.OwnerId,
                Event = Event.Sale,
                Price = 0,
                TransactionHash = command._event.TransactionHash
            };

            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);

            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"\nTransferAssetEvent\nTokenID: {command._event.TokenId}\nNewOwner: {command._event.NewOwner}");
            return true;
        }
    }
}

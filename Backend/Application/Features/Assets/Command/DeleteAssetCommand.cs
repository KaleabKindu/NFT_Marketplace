using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;
using Application.Common.Errors;

namespace Application.Features.Assets.Commands
{
    public class DeleteAssetCommand : IRequest<ErrorOr<bool>>
    {
        public DeleteAssetEventDto _event;
    }

    public class DeleteAssetCommandHandler
        : IRequestHandler<DeleteAssetCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteAssetCommandHandler> _logger;

        public DeleteAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteAssetCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            DeleteAssetCommand command,
            CancellationToken cancellationToken
        )
        {

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(command._event.TokenId);


            if (asset == null)
                return ErrorFactory.NotFound("Asset", "Asset not found");
            
            _unitOfWork.AssetRepository.DeleteAsset(asset);

            if (await _unitOfWork.SaveAsync() == 0)
                return ErrorFactory.InternalServerError("Asset", "Error deleting asset");
        
            
            _logger.LogInformation($"\nDeleteAssetEvent\nTokenID: {command._event.TokenId}\n");

            return true;
        }
    }
}

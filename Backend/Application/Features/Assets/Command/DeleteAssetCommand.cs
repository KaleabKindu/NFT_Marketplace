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
        public string Address { get; set; }
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

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(((long)command._event.TokenId));
            var user = await _unitOfWork.UserRepository.GetUserByAddress(command.Address);

            if (asset == null)
                return ErrorFactory.NotFound("Asset", "Asset not found");
            
            if (user == null)
                return ErrorFactory.NotFound("User", "User not found");
            
            if (asset.Owner.Address != command.Address)
                return ErrorFactory.AuthorizationError("Asset", "User is not the owner of the asset");
            
            _logger.LogInformation($"\nDeleteAssetEvent\nTokenID: {command._event.TokenId}\n");

            return true;
        }
    }
}

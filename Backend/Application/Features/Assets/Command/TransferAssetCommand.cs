using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;

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
            _logger.LogInformation($"\nTransferAssetEvent\nTokenID: {command._event.TokenId}\nNewOwner: {command._event.NewOwner}");
            return true;
        }
    }
}

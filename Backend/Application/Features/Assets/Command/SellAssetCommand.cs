using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;

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
            _logger.LogInformation($"\nAssetSoldEvent\nTokenID: {command._event.TokenId}\nTo: {command._event.To}");
            return true;
        }
    }
}

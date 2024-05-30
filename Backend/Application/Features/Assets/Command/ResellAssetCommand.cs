using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Features.Assets.Dtos;
using Application.Contracts.Persistance;

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

        public ResellAssetCommandHandler(IUnitOfWork unitOfWork, ILogger<ResellAssetCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            ResellAssetCommand command,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation($"\nResellAssetEvent\nTokenID: {command._event.TokenId}\nAuction: {command._event.Auction}\nPrice: {command._event.Price}\nAuctionEnd: {command._event.AuctionEnd}\n");
            return true;
        }
    }
}

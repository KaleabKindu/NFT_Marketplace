using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Application.Features.Auctions.Dtos;

namespace Application.Features.Auctions.Commands
{
    public class CreateAuctionCommand : IRequest<ErrorOr<bool>>
    {
        public AuctionCreatedEventDto _event;
    }

    public class CreateAuctionCommandHandler
        : IRequestHandler<CreateAuctionCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateAuctionCommandHandler> _logger;

        public CreateAuctionCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateAuctionCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CreateAuctionCommand command,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation($"\nCreateAuctionEvent\nAuctionId: {command._event.AuctionId}\nTokenId: {command._event.TokenId}\nSeller: {command._event.Seller}\nFloorPrice: {command._event.FloorPrice}\nAuctionEnd: {command._event.AuctionEnd}");
            return true;
        }
    }
}

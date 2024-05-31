using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Application.Features.Auctions.Dtos;

namespace Application.Features.Auctions.Commands
{
    public class CloseAuctionCommand : IRequest<ErrorOr<bool>>
    {
        public AuctionEndedEventDto _event;
    }

    public class CloseAuctionCommandHandler
        : IRequestHandler<CloseAuctionCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CloseAuctionCommandHandler> _logger;

        public CloseAuctionCommandHandler(IUnitOfWork unitOfWork, ILogger<CloseAuctionCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CloseAuctionCommand command,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation($"\nCloseAuctionEvent\nAuctionId: {command._event.AuctionId}\nWinner: {command._event.Winner}");
            return true;
        }
    }
}

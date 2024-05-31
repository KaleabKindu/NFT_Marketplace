using ErrorOr;
using MediatR;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Auctions.Commands
{
    public class PlaceBidCommand : IRequest<ErrorOr<bool>>
    {
        public BidPlacedEventDto _event;
    }

    public class PlaceBidCommandHandler
        : IRequestHandler<PlaceBidCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlaceBidCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(
            PlaceBidCommand command,
            CancellationToken cancellationToken
        )
        {
            return true;
        }
    }
}

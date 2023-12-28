using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Bids.Queries
{
    public class GetBidByIdQuery : IRequest<ErrorOr<BidDto>>
    {
        public long Id { get; set; }
    }

    public class GetBidByIdQueryHandler
        : IRequestHandler<GetBidByIdQuery, ErrorOr<BidDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBidByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BidDto>> Handle(
            GetBidByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(query.Id);

            if (Bid == null) return Error.NotFound("Bid.NotFound", "Bid not found");
            return _mapper.Map<BidDto>(Bid);
        }
    }
}

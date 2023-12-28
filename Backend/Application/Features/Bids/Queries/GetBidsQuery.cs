using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;

namespace Application.Features.Bids.Queries
{
    public class GetBidsQuery : IRequest<ErrorOr<List<BidsListDto>>> { }

    public class GetBidsQueryHandler
        : IRequestHandler<GetBidsQuery, ErrorOr<List<BidsListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBidsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<BidsListDto>>> Handle(
            GetBidsQuery query,
            CancellationToken cancellationToken
        )
        {
            var bids = await _unitOfWork.BidRepository.GetAllAsync();

            return _mapper.Map<List<BidsListDto>>(bids);
        }
    }
}

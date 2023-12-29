using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;
using Rideshare.Application.Responses;

namespace Application.Features.Bids.Queries
{
    public class GetBidsQuery : IRequest<ErrorOr<PaginatedResponse<BidsListDto>>> { 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetBidsQueryHandler
        : IRequestHandler<GetBidsQuery, ErrorOr<PaginatedResponse<BidsListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBidsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<BidsListDto>>> Handle(
            GetBidsQuery query,
            CancellationToken cancellationToken
        )
        {
            var bids = await _unitOfWork.BidRepository.GetAllAsync(query.PageNumber, query.PageSize);
            var total_count = await _unitOfWork.BidRepository.Count();

            return new PaginatedResponse<BidsListDto>(){
                Message="Bid lists fetched successfully",
                PageNumber=query.PageNumber,
                PageSize=query.PageSize,
                Count=total_count,
                Value=_mapper.Map<List<BidsListDto>>(bids)
            };
        }
    }
}

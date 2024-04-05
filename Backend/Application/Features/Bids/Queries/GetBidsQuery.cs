using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;
using Application.Responses;

namespace Application.Features.Bids.Queries
{
    public class GetBidsQuery : IRequest<ErrorOr<PaginatedResponse<BidsListDto>>> { 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TokenId { get; set; }
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
            var result = await _unitOfWork.BidRepository.GetAllBidsAsync(query.TokenId, query.PageNumber, query.PageSize);
            if (result.IsError) return result.Errors;
            
            return new PaginatedResponse<BidsListDto>(){
                Message="Bid lists fetched successfully",
                PageNumber=query.PageNumber,
                PageSize=query.PageSize,
                Count=result.Value.Item1,
                Value= result.Value.Item2
            };
        }
    }
}

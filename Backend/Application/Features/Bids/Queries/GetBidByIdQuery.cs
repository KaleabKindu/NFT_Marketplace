using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;
using Application.Common.Errors;
using Application.Common.Responses;

namespace Application.Features.Bids.Queries
{
    public class GetBidByIdQuery : IRequest<ErrorOr<BaseResponse<BidDto>>>
    {
        public long Id { get; set; }
    }

    public class GetBidByIdQueryHandler
        : IRequestHandler<GetBidByIdQuery, ErrorOr<BaseResponse<BidDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBidByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<BidDto>>> Handle(
            GetBidByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(query.Id);

            if (Bid == null) return ErrorFactory.NotFound("Bid","Bid not found");
            
            return new BaseResponse<BidDto>(){
                Message="Bid details fetched successfully",
                Value=_mapper.Map<BidDto>(Bid)
            };
        }
    }
}

using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;
using Rideshare.Application.Responses;

namespace Application.Features.Offers.Queries
{
    public class GetAllOfferQuery : IRequest<ErrorOr<PaginatedResponse<OfferDto>>> { 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllOfferQueryHandler
        : IRequestHandler<GetAllOfferQuery, ErrorOr<PaginatedResponse<OfferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOfferQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PaginatedResponse<OfferDto>>> Handle(
            GetAllOfferQuery request,
            CancellationToken cancellationToken
        )
        {
            var offers = await _unitOfWork.OfferRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var total_count = await _unitOfWork.OfferRepository.Count();

            return new PaginatedResponse<OfferDto>(){
                Message="Offers list fetched successfully",
                PageNumber=request.PageNumber,
                PageSize=request.PageSize,
                Count=total_count,
                Value=_mapper.Map<List<OfferDto>>(offers)
            };
        }
    }
}

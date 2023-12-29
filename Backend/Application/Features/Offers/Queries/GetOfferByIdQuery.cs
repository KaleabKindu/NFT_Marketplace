using Application.Common.Errors;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Offers.Queries
{
    public class GetOfferByIdQuery : IRequest<ErrorOr<BaseResponse<OfferDto>>>
    {
        public long Id { get; set; }
    }

    public class GetOfferByIdQueryHandler
        : IRequestHandler<GetOfferByIdQuery, ErrorOr<BaseResponse<OfferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOfferByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<OfferDto>>> Handle(
            GetOfferByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(request.Id);

            if (offer == null) return ErrorFactory.NotFound("Offer");
            return new BaseResponse<OfferDto>(){
                Message="Offer details fetched successfully",
                Value=_mapper.Map<OfferDto>(offer)
            };
        }
    }
}

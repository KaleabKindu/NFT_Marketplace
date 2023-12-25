using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Offers.Queries
{
    public class GetAllOfferQuery : IRequest<ErrorOr<List<OfferDto>>> { }

    public class GetAllOfferQueryHandler
        : IRequestHandler<GetAllOfferQuery, ErrorOr<List<OfferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOfferQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<OfferDto>>> Handle(
            GetAllOfferQuery request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetAllAsync();

            return _mapper.Map<List<OfferDto>>(offer);
        }
    }
}

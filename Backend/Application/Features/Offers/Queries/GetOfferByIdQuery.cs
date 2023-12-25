using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain.Offers;
using ErrorOr;
using MediatR;

namespace Application.Features.Offers.Queries
{
    public class GetOfferByIdQuery : IRequest<ErrorOr<OfferDto>>
    {
        public long Id { get; set; }
    }

    public class GetOfferByIdQueryHandler
        : IRequestHandler<GetOfferByIdQuery, ErrorOr<OfferDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOfferByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<OfferDto>> Handle(
            GetOfferByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(request.Id);

            if (offer == null) return OfferError.NotFound;
            return _mapper.Map<OfferDto>(offer);
        }
    }
}

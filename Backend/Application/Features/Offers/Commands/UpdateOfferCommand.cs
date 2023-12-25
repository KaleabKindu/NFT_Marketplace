using Application.Common;
using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain.Offers;
using ErrorOr;
using MediatR;

namespace Application.Features.Offers.Commands
{
    public class UpdateOfferCommand : IRequest<ErrorOr<Unit>>
    {
        public UpdateOfferDto Offer { get; set; }
    }

    public class UpdateOfferCommandHandler
        : IRequestHandler<UpdateOfferCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOfferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(
            UpdateOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(
                request.Offer.Id
            );

            if (offer == null) return OfferError.NotFound;
        
            _mapper.Map(request.Offer, offer);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;
            
            return Unit.Value;
        }
    }
}

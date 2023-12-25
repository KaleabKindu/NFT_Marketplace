using Application.Common;
using Application.Contracts.Persistance;
using Application.Features.Offers.Dtos;
using AutoMapper;
using Domain.Offers;
using ErrorOr;
using MediatR;

namespace Application.Features.Offers.Commands
{
    public class CreateOfferCommand : IRequest<ErrorOr<long>>
    {
        public CreateOfferDto Offer { get; set; }
    }

    public class CreateOfferCommandHandler
        : IRequestHandler<CreateOfferCommand, ErrorOr<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOfferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<long>> Handle(
            CreateOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = _mapper.Map<Offer>(request.Offer);
            await _unitOfWork.OfferRepository.AddAsync(offer);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                return CommonError.ErrorSavingChanges;
            
            return offer.Id;
        }

    }
}

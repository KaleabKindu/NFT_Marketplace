using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Features.Offers.Dtos;
using Application.Contracts.Persistance;
using Domain.Offers;

namespace Application.Features.Offers.Commands
{
    public class CreateOfferCommand : IRequest<ErrorOr<BaseResponse<OfferDto>>>
    {
        public CreateOfferDto Offer { get; set; }
    }

    public class CreateOfferCommandHandler
        : IRequestHandler<CreateOfferCommand, ErrorOr<BaseResponse<OfferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOfferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<OfferDto>>> Handle(
            CreateOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = _mapper.Map<Offer>(request.Offer);
            await _unitOfWork.OfferRepository.AddAsync(offer);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to save changes to database");
            
            return new BaseResponse<OfferDto>(){
                Message="Offer created successfully",
                Value=_mapper.Map<OfferDto>(offer)
            };
        }

    }
}

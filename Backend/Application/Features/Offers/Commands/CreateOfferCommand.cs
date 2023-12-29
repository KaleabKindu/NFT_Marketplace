using Domain;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Features.Offers.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Offers.Commands
{
    public class CreateOfferCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateOfferDto Offer { get; set; }
    }

    public class CreateOfferCommandHandler
        : IRequestHandler<CreateOfferCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOfferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(
            CreateOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = _mapper.Map<Offer>(request.Offer);
            await _unitOfWork.OfferRepository.AddAsync(offer);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to save changes to database");
            
            return new BaseResponse<long>(){
                Message="Offer created successfully",
                Value=offer.Id
            };
        }

    }
}

using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Features.Offers.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Offers.Commands
{
    public class UpdateOfferCommand : IRequest<ErrorOr<BaseResponse<OfferDto>>>
    {
        public UpdateOfferDto Offer { get; set; }
    }

    public class UpdateOfferCommandHandler
        : IRequestHandler<UpdateOfferCommand, ErrorOr<BaseResponse<OfferDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOfferCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<OfferDto>>> Handle(
            UpdateOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(
                request.Offer.Id
            );

            if (offer == null) return ErrorFactory.NotFound("Offer","Offer not found");
        
            _mapper.Map(request.Offer, offer);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<OfferDto>(){
                Message="Offer updated successfully",
                Value=_mapper.Map<OfferDto>(offer)
            };        
        }
    }
}

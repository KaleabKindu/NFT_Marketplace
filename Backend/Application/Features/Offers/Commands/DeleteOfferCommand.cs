using ErrorOr;
using MediatR;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;

namespace Application.Features.Offers.Commands
{
    public class DeleteOfferCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public long Id { get; set; }
    }

    public class DeleteOfferCommandHandler
        : IRequestHandler<DeleteOfferCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOfferCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(request.Id);

            if (offer == null) return ErrorFactory.NotFound("Offer");
            
            _unitOfWork.OfferRepository.DeleteAsync(offer);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");

            return new BaseResponse<Unit>(){
                Message="Offer deleted successfully",
                Value=Unit.Value
            };
        }
    }
}

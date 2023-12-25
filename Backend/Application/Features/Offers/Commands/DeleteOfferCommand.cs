using Application.Common;
using Application.Contracts.Persistance;
using Domain.Offers;
using ErrorOr;

using MediatR;

namespace Application.Features.Offers.Commands
{
    public class DeleteOfferCommand : IRequest<ErrorOr<Unit>>
    {
        public long Id { get; set; }
    }

    public class DeleteOfferCommandHandler
        : IRequestHandler<DeleteOfferCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOfferCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteOfferCommand request,
            CancellationToken cancellationToken
        )
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(request.Id);

            if (offer == null) return OfferError.NotFound;
            
            _unitOfWork.OfferRepository.DeleteAsync(offer);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;

            return Unit.Value;
        }
    }
}

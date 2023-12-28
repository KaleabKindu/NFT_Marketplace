using ErrorOr;
using MediatR;
using Application.Common;
using Application.Contracts.Persistance;


namespace Application.Features.Bids.Commands
{
    public class DeleteBidCommand : IRequest<ErrorOr<Unit>>
    {
        public long Id { get; set; }
    }

    public class DeleteBidCommandHandler
        : IRequestHandler<DeleteBidCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBidCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(
            DeleteBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(request.Id);

            if (Bid == null) return Error.NotFound("Bid.NotFound", "Bid not found");
            
            _unitOfWork.BidRepository.DeleteAsync(Bid);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;

            return Unit.Value;
        }
    }
}

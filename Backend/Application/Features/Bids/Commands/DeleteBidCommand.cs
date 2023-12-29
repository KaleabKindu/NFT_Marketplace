using ErrorOr;
using MediatR;
using Application.Common;
using Application.Contracts.Persistance;
using Application.Common.Exceptions;
using Application.Common.Errors;
using Application.Common.Responses;


namespace Application.Features.Bids.Commands
{
    public class DeleteBidCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public long Id { get; set; }
    }

    public class DeleteBidCommandHandler
        : IRequestHandler<DeleteBidCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBidCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(request.Id);

            if (Bid == null) return ErrorFactory.NotFound("Bid");
            
            _unitOfWork.BidRepository.DeleteAsync(Bid);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<Unit>(){
                Message="Bid deleted successfully",
                Value=Unit.Value
            };
        }
    }
}

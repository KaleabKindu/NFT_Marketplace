using Application.Common;
using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Bids.Commands
{
    public class UpdateBidCommand : IRequest<ErrorOr<Unit>>
    {
        public UpdateBidDto Bid { get; set; }
    }

    public class UpdateBidCommandHandler
        : IRequestHandler<UpdateBidCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Unit>> Handle(
            UpdateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(
                request.Bid.Id
            );

            if (Bid == null) return Error.NotFound("Bid.NotFound", "Bid not found");
        
            _mapper.Map(request.Bid, Bid);

            if (await _unitOfWork.SaveAsync() == 0)
                return CommonError.ErrorSavingChanges;
            
            return Unit.Value;
        }
    }
}

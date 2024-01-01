using Application.Common;
using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Bids.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Bids.Commands
{
    public class UpdateBidCommand : IRequest<ErrorOr<BaseResponse<BidDto>>>
    {
        public UpdateBidDto Bid { get; set; }
    }

    public class UpdateBidCommandHandler
        : IRequestHandler<UpdateBidCommand, ErrorOr<BaseResponse<BidDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<BidDto>>> Handle(
            UpdateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(
                request.Bid.Id
            );

            if (Bid == null) return ErrorFactory.NotFound("Bid", "Bid not found");
        
            _mapper.Map(request.Bid, Bid);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<BidDto>(){
                Message="Bid updated successfully",
                Value=_mapper.Map<BidDto>(Bid)
            };
        }
    }
}

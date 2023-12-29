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
    public class UpdateBidCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public UpdateBidDto Bid { get; set; }
    }

    public class UpdateBidCommandHandler
        : IRequestHandler<UpdateBidCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            UpdateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var Bid = await _unitOfWork.BidRepository.GetByIdAsync(
                request.Bid.Id
            );

            if (Bid == null) return ErrorFactory.NotFound("Bid");
        
            _mapper.Map(request.Bid, Bid);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<Unit>(){
                Message="Bid updated successfully",
                Value=Unit.Value
            };
        }
    }
}

using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Domain.Bids;
using Application.Common.Errors;

namespace Application.Features.Bids.Commands
{
    public class CreateBidCommand : IRequest<ErrorOr<BaseResponse<BidDto>>>
    {
        public string Bidder { set; get; }
        public CreateBidDto Bid { get; set; }
    }

    public class CreateBidCommandHandler
        : IRequestHandler<CreateBidCommand, ErrorOr<BaseResponse<BidDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<BidDto>>> Handle(
            CreateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            Bid bid = _mapper.Map<Bid>(request.Bid);

            bid.Asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.Bid.AssetId);
            
            if (bid.Asset == null) return ErrorFactory.NotFound("Asset", "Asset not found");

            bid.Bidder = await _unitOfWork.UserRepository.GetUserByAddress(request.Bidder);
            
            await _unitOfWork.BidRepository.AddAsync(bid);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<BidDto>(){
                Message="Bid created successfully",
                Value=_mapper.Map<BidDto>(bid)
            };
        }
    }
}

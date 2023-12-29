using Domain;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;
using Application.Common.Exceptions;
using Application.Common.Responses;

namespace Application.Features.Bids.Commands
{
    public class CreateBidCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateBidDto Bid { get; set; }
    }

    public class CreateBidCommandHandler
        : IRequestHandler<CreateBidCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(
            CreateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            Bid bid = _mapper.Map<Bid>(request.Bid);
            await _unitOfWork.BidRepository.AddAsync(bid);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<long>(){
                Message="Bid created successfully",
                Value=bid.Id
            };
        }
    }
}

using Domain;
using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common;
using Application.Features.Bids.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Bids.Commands
{
    public class CreateBidCommand : IRequest<ErrorOr<long>>
    {
        public CreateBidDto Bid { get; set; }
    }

    public class CreateBidCommandHandler
        : IRequestHandler<CreateBidCommand, ErrorOr<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<long>> Handle(
            CreateBidCommand request,
            CancellationToken cancellationToken
        )
        {
            var bid = _mapper.Map<Bid>(request.Bid);
            await _unitOfWork.BidRepository.AddAsync(bid);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                return CommonError.ErrorSavingChanges;
            
            return bid.Id;
        }

    }
}

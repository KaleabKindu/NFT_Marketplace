using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using Application.Common.Responses;
using Application.Features.Busy.Dtos;
using Application.Common.Errors;
using Application.Common.Exceptions;
using Domain.Provenances;
using Domain.Transactions;

namespace Application.Features.Buys.Commands
{
    public class BuyAssetCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public BuyAssetDto BuyAsset { get; set; }
        public string UserAddress { get; set; }
    }

    public class BuyAssetCommandHandler
        : IRequestHandler<BuyAssetCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BuyAssetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            BuyAssetCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await _unitOfWork.UserRepository.AddressExists(request.UserAddress))
                return ErrorFactory.BadRequestError("User","User not found");

            var user = await _unitOfWork.UserRepository.CreateOrFetchUserAsync(request.UserAddress);

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.BuyAsset.AssetId);

            asset.Owner = user;
        
            var transaction  = new Transaction()
            {
                Type = TransactionType.Sell,
                Asset = asset,
                Buyer = user,
                Seller = asset.Owner,
                Amount = request.BuyAsset.Price,
                BlockchainTxHash = request.BuyAsset.Hash
            };
            
            var provenance = new Provenance
            {
                Event = Event.Sale,
                From = asset.Owner,
                To = user,
                Price = request.BuyAsset.Price,
                TransactionHash = request.BuyAsset.Hash
            };

            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);
            
            
            await _unitOfWork.TransactionRepository.AddAsync(transaction);
    
            if (await _unitOfWork.SaveAsync() == 0) 
                throw new DbAccessException("Unable to save to database");
            
            return new BaseResponse<Unit>(){
                Message="Asset transfered successfully",
                Value=Unit.Value
            };
        }
    }
}

using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain.Assets;
using Domain.Auctions;
using Domain.Collections;
using Domain.Provenances;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Command
{
    public class CreateAssetCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateAssetDto CreateAssetDto { get; set; }
        public string Address { get; set; }


    }

    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<long>();

            var user = await _unitOfWork.UserRepository.GetUserByAddress(request.Address);

            if (user == null)
                return ErrorFactory.NotFound(nameof(user), "user not found");



            var asset = _mapper.Map<Asset>(request.CreateAssetDto);
            asset.Creator = user;
            asset.Owner = user;

            Collection collection;


            if (request.CreateAssetDto.CollectionId != null)
            {
                collection = await _unitOfWork.CollectionRepository.GetByIdAsync(request.CreateAssetDto.CollectionId ?? 0);

                if (collection == null)
                    return ErrorFactory.NotFound(nameof(collection), "collection not found");

                collection.Volume += asset.Price;
                collection.Items += 1;
                collection.FloorPrice = collection.FloorPrice == 0 ? asset.Price : Math.Min(collection.FloorPrice, asset.Price);

                _unitOfWork.CollectionRepository.UpdateAsync(collection);
            }

            if (request.CreateAssetDto.Auction != null)
            {
                var auction = new Auction
                {
                    AuctionId = request.CreateAssetDto.Auction.AuctionId,
                    TokenId = request.CreateAssetDto.TokenId,
                    Seller = user,
                    FloorPrice = request.CreateAssetDto.Price,
                    AuctionEnd = request.CreateAssetDto.Auction.AuctionEnd,
                    HighestBid = request.CreateAssetDto.Price,
                };

                asset.Auction = auction;
                asset.Status = AssetStatus.OnAuction;
            }
            else
            {
                asset.Status = AssetStatus.OnFixedSale;
            }

            var provenance = new Provenance
            {
                Event = Event.Mint,
                Asset = asset,
                From = user,
                TransactionHash = request.CreateAssetDto.TransactionHash

            };
            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);

            await _unitOfWork.AssetRepository.AddAsync(asset);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To Save");

            response.Message = "Creation Succesful";
            response.Value = asset.Id;

            return response;
        }
    }


}
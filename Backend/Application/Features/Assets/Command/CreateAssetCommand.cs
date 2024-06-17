using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Contracts.Services;
using Application.Features.Assets.Dtos;
using Application.Features.Notifications.Dtos;
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
        private readonly IAuctionManagementService _auctionManager;
        private readonly INotificationService _notificationService;

        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork, IAuctionManagementService auctionManager, INotificationService notificationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            _auctionManager = auctionManager;
            _notificationService = notificationService;
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
                auction.JobId = _auctionManager.Schedule(request.Address, auction.AuctionId, auction.AuctionEnd);

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
                ToId = user.Id,
                TransactionHash = request.CreateAssetDto.TransactionHash

            };
            await _unitOfWork.ProvenanceRepository.AddAsync(provenance);

            await _unitOfWork.AssetRepository.AddAsync(asset);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To Save");

            var followers = await _unitOfWork.UserRepository.GetAllFollowersAsync(user.Address);

            var notificationDto = new CreateNotificationDto
            {
                Title = "New Asset",
                Content = $"{user.Profile.UserName} has created a new asset {asset.Name}",
            };

            await _notificationService.SendNotificationsForMultipleUsers(followers.Select(x => x.Id).ToList(), notificationDto);

            response.Message = "Asset Created Successfully";
            response.Value = asset.Id;

            return response;
        }
    }


}
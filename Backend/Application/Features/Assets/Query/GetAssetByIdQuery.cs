using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace Application.Features.Assets.Query
{
    public class GetAssetByIdQuery : IRequest<ErrorOr<BaseResponse<AssetDetailDto>>>
    {
        public int Id {get; set;}
        public string UserId { get; set; }
        
    }

     public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, ErrorOr<BaseResponse<AssetDetailDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAssetByIdQuery> _logger;

        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfwork, ILogger<GetAssetByIdQuery> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            _logger = logger;
        }

        public async Task<ErrorOr<BaseResponse<AssetDetailDto>>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<AssetDetailDto>();

            var asset = await _unitOfWork.AssetRepository.GetAssetWithDetail(request.Id,request.UserId);
            if (asset.IsError) return asset.Errors;


            // var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(asset.Value.Auction.AuctionId);

            response.Message = "Fetch Successful";
            response.Value = asset.Value;

            // _logger.LogInformation(auction.HighestBid.ToString());
            // _logger.LogInformation(auction.AuctionEnd.ToString());
            // _logger.LogInformation(response.Value.Auction.HighestBid);
            return response;
        }
    }
}
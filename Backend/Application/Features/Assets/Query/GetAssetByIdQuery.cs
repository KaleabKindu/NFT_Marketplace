using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using ErrorOr;
using Application.Common.Errors;
using Microsoft.Extensions.Logging;

namespace Application.Features.Assets.Query
{
    public class GetAssetByIdQuery : IRequest<ErrorOr<BaseResponse<AssetDetailDto>>>
    {
        public int Id {get; set;}
        
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

            var asset = await _unitOfWork.AssetRepository.GetAssetWithDetail(request.Id);

            if (asset == null)
                return ErrorFactory.NotFound("Asset","Asset not found");

            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync(asset.Auction.Id);

            response.Message = "Fetch Successful";
            response.Value = _mapper.Map<AssetDetailDto>(asset);

            _logger.LogInformation(auction.HighestBid);
            _logger.LogInformation(auction.AuctionEnd.ToString());
            _logger.LogInformation(asset.Auction.HighestBid);
            _logger.LogInformation(response.Value.Auction.CurrentPrice);
            return response;
        }
    }
}
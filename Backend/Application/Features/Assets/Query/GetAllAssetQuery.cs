using Application.Features.Assets.Dtos;
using Application.Features.Common;
using MediatR;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using Application.Responses;
using Domain.Assets;

namespace Application.Features.Assets.Query
{
    public sealed class GetAllAssetQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        public string Query { get; set; } = "";
        public double MaxPrice { get; set; } = -1;
        public double MinPrice { get; set; } = -1;
        public AssetCategory? Category { get; set; } = null;
        public string SortBy { get; set; } = "Id";
        public string? SaleType { get; set; } = null;
        public long? CollectionId { get; set; } = null;
        public string? CreatorId { get; set; } = null;        
    }


     public class GetAllAssetQueryHandler : IRequestHandler<GetAllAssetQuery, ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAssetQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        } 

        public async Task<ErrorOr<PaginatedResponse<AssetListDto>>> Handle(GetAllAssetQuery request, CancellationToken cancellationToken)
        {           
            
            var result = await _unitOfWork.AssetRepository
                .GetFilteredAssets(request.Query,request.MinPrice, request.MaxPrice, request.Category, request.SortBy, request.SaleType, request.CollectionId, request.CreatorId, request.PageNumber, request.PageSize);

            var response = new PaginatedResponse<AssetListDto>{
                Message = "Fetch Succesful",
                Value = _mapper.Map<List<AssetListDto>>(result.Item2),
                Count = result.Item1,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            };

            return response;


        }
    }
}
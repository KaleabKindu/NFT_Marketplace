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
        public string? UserId { get; set; }
        public string Query { get; set; } = "";
        public double MaxPrice { get; set; } = -1;
        public double MinPrice { get; set; } = -1;
        public AssetCategory? Category { get; set; } = null;
        public string SortBy { get; set; } = "Id";
        public string? SaleType { get; set; } = null;
        public long? CollectionId { get; set; } = null;
        public string? Creator { get; set; } = null;
        public string? SemanticSearchQuery { get; set; }
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
            List<AssetListDto> filteredResult;
            int count;
            if (request.SemanticSearchQuery != null)
            {
                var temp = await _unitOfWork.AssetRepository.SemanticBasedAssetSearch(request.SemanticSearchQuery, request.PageNumber, request.PageSize);
                filteredResult = _mapper.Map<List<AssetListDto>>(temp.Value);
                count = temp.Count;
            }
            else
            {
                var result = await _unitOfWork.AssetRepository
                .GetFilteredAssets(request.UserId, request.Query, request.MinPrice, request.MaxPrice, request.Category, request.SortBy, request.SaleType, request.CollectionId, request.Creator, request.PageNumber, request.PageSize);
                if (result.IsError) return result.Errors;
                filteredResult = result.Value.Item2.ToList();
                count = result.Value.Item1;
            }

            var response = new PaginatedResponse<AssetListDto>
            {
                Message = "Fetch Succesful",
                Value = filteredResult,
                Count = count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return response;


        }
    }
}
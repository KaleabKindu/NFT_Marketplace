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
    public sealed class GetTrendingAssetQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        public string? UserId { get; set; }
    }


     public class GetTrendingAssetQueryHandler : IRequestHandler<GetTrendingAssetQuery, ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTrendingAssetQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        } 

        public async Task<ErrorOr<PaginatedResponse<AssetListDto>>> Handle(GetTrendingAssetQuery request, CancellationToken cancellationToken)
        {           
            
            var result = await _unitOfWork.AssetRepository
                .GetTrendingAssets(request.UserId, request.PageNumber, request.PageSize);
            
            if (result.IsError) return result.Errors;

            var response = new PaginatedResponse<AssetListDto>{
                Message = "Fetch Succesful",
                Value = _mapper.Map<List<AssetListDto>>(result.Value.Item2),
                Count = result.Value.Item1,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            };

            return response;


        }
    }
}
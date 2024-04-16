using Application.Common.Errors;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Categories.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;
using Domain.Assets;

namespace Application.Features.Categories.Queries
{
    public class GetAssetCountByCategoryQuery : IRequest<ErrorOr<BaseResponse<GetAssetsCountDto>>>
    {
        public AssetCategory CategoryName { get; set; }
    }

    public class GetAssetCountByCategoryQueryHandler
        : IRequestHandler<GetAssetCountByCategoryQuery, ErrorOr<BaseResponse<GetAssetsCountDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAssetCountByCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<GetAssetsCountDto>>> Handle(
            GetAssetCountByCategoryQuery request,
            CancellationToken cancellationToken
        )
        // here i am 
        {
            var category = await _unitOfWork.AssetRepository.GetByAssetAsync(request.CategoryName);

            if (category == null) return ErrorFactory.NotFound("Category", "Category not found");

            var assetsCount = category.Count(); 

            var responseDto = new GetAssetsCountDto
            {
                name = request.CategoryName.ToString(),
                count = assetsCount.ToString() 
            };

            return new BaseResponse<GetAssetsCountDto>()
                {
                    Message = "Asset count fetched successfully",
                    Value = responseDto
                };
        }
    }
}

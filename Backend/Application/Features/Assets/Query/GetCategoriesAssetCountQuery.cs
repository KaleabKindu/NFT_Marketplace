using Application.Common.Responses;
using Application.Contracts.Persistance;
using ErrorOr;
using MediatR;
using Domain.Assets;

namespace Application.Features.Categories.Queries
{
    public class GetCategoriesAssetCountQuery : IRequest<ErrorOr<BaseResponse<Dictionary<string, int>>>>
    {
        public AssetCategory CategoryName { get; set; }
    }

    public class GetCategoriesAssetCountQueryHandler
        : IRequestHandler<GetCategoriesAssetCountQuery, ErrorOr<BaseResponse<Dictionary<string, int>>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesAssetCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Dictionary<string, int>>>> Handle(
            GetCategoriesAssetCountQuery request,
            CancellationToken cancellationToken
        )
        {
            var response = await _unitOfWork.AssetRepository.GetCategoriesAssetCount();

            if (response.IsError) return response.Errors;

            return new BaseResponse<Dictionary<string, int>>()
            {
                Message = "Asset count fetched successfully",
                Value = response.Value
            };
        }
    }
}

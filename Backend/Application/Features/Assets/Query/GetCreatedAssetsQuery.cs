using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using Application.Features.Common;
using Application.Responses;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetCreatedAssetsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        public string userId;
    }

    public class GetCreatedAssetsQueryHandler
        : IRequestHandler<GetCreatedAssetsQuery, ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCreatedAssetsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PaginatedResponse<AssetListDto>>> Handle(
            GetCreatedAssetsQuery request,
            CancellationToken cancellationToken
        )
        {
            var assetsResponse = await _unitOfWork.AssetRepository.GetCreatedAssetsAsync(request.userId, request.PageNumber, request.PageSize);
            if (assetsResponse.IsError) return assetsResponse.Errors;

            var response = new PaginatedResponse<AssetListDto>
            {
                Message = "Fetch Succesful",
                Value = assetsResponse.Value.Item2,
                Count = assetsResponse.Value.Item1,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return response;
        }
    }
}

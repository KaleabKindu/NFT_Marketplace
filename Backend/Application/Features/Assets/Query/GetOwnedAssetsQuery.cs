using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using Application.Features.Common;
using Application.Responses;
using ErrorOr;
using MediatR;

namespace Application.Features.Categories.Queries
{
    public class GetOwnedAssetsQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        public string userId;
    }

    public class GetOwnedAssetsQueryHandler
        : IRequestHandler<GetOwnedAssetsQuery, ErrorOr<PaginatedResponse<AssetListDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOwnedAssetsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<PaginatedResponse<AssetListDto>>> Handle(
            GetOwnedAssetsQuery request,
            CancellationToken cancellationToken
        )
        {
            var assetsResponse = await _unitOfWork.AssetRepository.GetOwnedAssetsAsync(request.userId, request.PageNumber, request.PageSize);
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

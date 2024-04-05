using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using ErrorOr;

namespace Application.Features.Assets.Query
{
    public class GetAssetByIdQuery : IRequest<ErrorOr<BaseResponse<AssetDetailDto>>>
    {
        public int Id {get; set;}
        public string? UserId { get; set; }
        
    }

     public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, ErrorOr<BaseResponse<AssetDetailDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
        }

        public async Task<ErrorOr<BaseResponse<AssetDetailDto>>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<AssetDetailDto>();

            var asset = await _unitOfWork.AssetRepository.GetAssetWithDetail(request.Id,request.UserId);
            if (asset.IsError) return asset.Errors;

            response.Message = "Fetch Successful";
            response.Value = asset.Value;

            return response;
        }
    }
}
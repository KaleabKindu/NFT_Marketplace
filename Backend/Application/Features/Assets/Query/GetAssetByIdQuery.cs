using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;
using AutoMapper;
using Application.Contracts.Persistance;
using ErrorOr;
using Application.Common.Errors;

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

        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<BaseResponse<AssetDetailDto>>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<AssetDetailDto>();

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.Id);

            if (asset == null)
                return ErrorFactory.NotFound("Asset","Asset not found");

            response.Message = "Fetch Successful";
            response.Value = _mapper.Map<AssetDetailDto>(asset);
            return response;
        }
    }
}
using Application.Features.Assets.Dtos;
using Application.Features.Common;
using MediatR;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using Application.Responses;

namespace Application.Features.Assets.Query
{
    public class GetAllAssetQuery : PaginatedQuery, IRequest<ErrorOr<PaginatedResponse<AssetDto>>>
    {
        
    }


     public class GetAllAssetQueryHandler : IRequestHandler<GetAllAssetQuery, ErrorOr<PaginatedResponse<AssetDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAssetQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        } 

        public async Task<ErrorOr<PaginatedResponse<AssetDto>>> Handle(GetAllAssetQuery request, CancellationToken cancellationToken)
        {
            
            var result = await _unitOfWork.AssetRepository.GetAllAsync(request.PageNumber, request.PageSize);
            var count  = await _unitOfWork.AssetRepository.Count();

            var response = new PaginatedResponse<AssetDto>{
                Message = "Fetch Succesful",
                Value = _mapper.Map<List<AssetDto>>(result),
                Count = count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            };

            return response;


        }
    }
}
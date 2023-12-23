using System;
using Application.Contracts.Presistence;
using Application.Features.Assets.CQRS.Query;
using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Assets.CQRS.Handler
{
    public class GetAllAssetQueryHandler : IRequestHandler<GetAllAssetQuery, PaginatedResponse<AssetDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAssetQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        } 

        public async Task<PaginatedResponse<AssetDto>> Handle(GetAllAssetQuery request, CancellationToken cancellationToken)
        {
            
            var result = await _unitOfWork.AssetRepository.GetAllAsync(request.PageNumber, request.PageSize);

            var response = new PaginatedResponse<AssetDto>{
                Message = "Fetch Succesful",
                Value = _mapper.Map<IReadOnlyList<AssetDto>>(result),
                Count = result.Count(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize

            };

            return response;


        }
    }
}
using System;
using Application.Common.Exceptions;
using Application.Contracts.Presistence;
using Application.Features.Assets.CQRS.Query;
using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Assets.CQRS.Handler
{
    public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, BaseResponse<AssetDto>>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAssetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<BaseResponse<AssetDto>> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
        {

            var response = new BaseResponse<AssetDto>();

            var asset = _unitOfWork.AssetRepository.GetByIdAsync(request.Id);

            if (asset == null)
                throw new NotFoundException("Asset NotFound");

            response.Success = true;
            response.Message = "Fetch Successful";
            response.Value = _mapper.Map<AssetDto>(asset);
            return response;
        }
    }
}
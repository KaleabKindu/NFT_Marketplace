using System;
using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Assets.CQRS.Query
{
    public class GetAssetByIdQuery : IRequest<BaseResponse<AssetDto>>
    {
        public int Id {get; set;}
        
    }
}
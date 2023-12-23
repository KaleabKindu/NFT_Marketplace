using System;
using Application.Features.Assets.Dtos;
using Application.Features.Common;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Assets.CQRS.Query
{
    public class GetAllAssetQuery : PaginatedQuery, IRequest<PaginatedResponse<AssetDto>>
    {
        
    }
}
using System;
using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Assets.CQRS.Command
{
    public class UpdateAssetCommand : IRequest<BaseResponse<Unit>>
    {
        
        public UpdateAssetDto UpdateAssetDto {get; set;}
    }
}
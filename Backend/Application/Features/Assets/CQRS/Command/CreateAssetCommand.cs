using System;
using Application.Common.Responses;
using Application.Features.Assets.Dtos;
using MediatR;

namespace Application.Features.Assets.CQRS.Command
{
    public class CreateAssetCommand : IRequest<BaseResponse<int>>
    {
        public CreateAssetDto CreateAssetDto { get; set; }

        
    }
}
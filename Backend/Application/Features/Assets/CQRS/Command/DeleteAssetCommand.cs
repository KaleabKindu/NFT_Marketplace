using System;
using Application.Common.Responses;
using MediatR;

namespace Application.Features.Assets.CQRS.Command
{
    public class DeleteAssetCommand :IRequest<BaseResponse<Unit>>
    {
        public int Id {get; set;}
        
    }
}
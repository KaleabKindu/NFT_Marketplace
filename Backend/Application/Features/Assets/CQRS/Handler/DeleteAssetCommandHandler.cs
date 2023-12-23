using System;
using Application.Common.Exceptions;
using Application.Contracts.Presistence;
using Application.Features.Assets.CQRS.Command;
using Application.Common.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Assets.CQRS.Handler
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, BaseResponse<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DeleteAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        }

        public async Task<BaseResponse<Unit>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
           var response = new BaseResponse<Unit>();

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.Id);

            if (asset == null)
                throw new NotFoundException("Asset Not Found");

            _unitOfWork.AssetRepository.DeleteAsync(asset) ;

            if (await _unitOfWork.Save() == 0)
                throw new InternalServerErrorException("Database Error: Unable To Save");

            response.Success = true;
            response.Message = "Deletion Succeeded";
            response.Value = Unit.Value;


            return response;
        }
    }
}
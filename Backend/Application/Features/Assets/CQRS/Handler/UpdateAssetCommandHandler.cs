using System;
using Application.Common.Exceptions;
using Application.Contracts.Presistence;
using Application.Features.Assets.CQRS.Command;
using Application.Features.Assets.Dtos.Validators;
using Application.Common.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Assets.CQRS.Handler
{
    public class UpdateAssetCommandHandler :  IRequestHandler<UpdateAssetCommand, BaseResponse<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
            
        }

        public async Task<BaseResponse<Unit>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Unit>();
            var validator = new UpdateAssetDtoValidator();

            var validationResult = await validator.ValidateAsync(request.UpdateAssetDto);


            if (!validationResult.IsValid)
                throw new ValidationException("Asset Not Valid", validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.UpdateAssetDto.Id);

            if (asset == null )
                throw new NotFoundException("Resource Not Found");

            _mapper.Map(request.UpdateAssetDto, asset);
            _unitOfWork.AssetRepository.UpdateAsync(asset);

            if ( await _unitOfWork.Save() == 0)
                throw new InternalServerErrorException("Database Error: Unable To Save");


            response.Success = true;
            response.Message = "Update Successful";
            response.Value = Unit.Value;



            return response;
        }
    }
}
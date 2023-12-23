using System;
using Application.Common.Exceptions;
using Application.Contracts.Presistence;
using Application.Features.Assets.CQRS.Command;
using Application.Features.Assets.Dtos.Validators;
using Application.Common.Responses;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Assets.CQRS.Handler
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, BaseResponse<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<BaseResponse<int>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<int>();
            var validator = new CreateAssetDtoValidator();

            var validationResult = await validator.ValidateAsync(request.CreateAssetDto);


            if (!validationResult.IsValid)
                throw new ValidationException("Asset not valid", validationResult.Errors.Select(q => q.ErrorMessage).ToList().First());


            var asset = _mapper.Map<Asset>(request.CreateAssetDto);
            await _unitOfWork.AssetRepository.AddAsync(asset);


            if (await _unitOfWork.Save() == 0)
                throw new InternalServerErrorException("Database Error: Unable To Save");

            response.Success = true;
            response.Message = "Creation Succesful";
            response.Value = asset.Id;



            return response;
        }
    }
}
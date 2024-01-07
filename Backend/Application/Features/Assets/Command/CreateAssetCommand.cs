using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Assets.Dtos;
using AutoMapper;
using Domain.Assets;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Command
{
    public class CreateAssetCommand : IRequest<ErrorOr<BaseResponse<long>>>
    {
        public CreateAssetDto CreateAssetDto { get; set; }

        
    }

    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, ErrorOr<BaseResponse<long>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CreateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<BaseResponse<long>>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<long>();

            var asset = _mapper.Map<Asset>(request.CreateAssetDto);
            await _unitOfWork.AssetRepository.AddAsync(asset);


            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To Save");

            response.Message = "Creation Succesful";
            response.Value = asset.Id;

            return response;
        }
    }

    
}
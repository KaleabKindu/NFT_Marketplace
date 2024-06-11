using Application.Features.Assets.Dtos;
using Application.Common.Responses;
using MediatR;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using Application.Common.Errors;

namespace Application.Features.Assets.Command
{
    public class UpdateAssetCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        
        public UpdateAssetDto UpdateAssetDto {get; set;}
    }


    public class UpdateAssetCommandHandler :  IRequestHandler<UpdateAssetCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
            
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Unit>();        

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.UpdateAssetDto.Id);

            if (asset == null )
                return ErrorFactory.NotFound("Resource","Resource Not Found");

            var oldDesc = asset.Description;
            _mapper.Map(request.UpdateAssetDto, asset);
            _unitOfWork.AssetRepository.UpdateAsync(asset);

            if ( await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To SaveAsync");

            if(oldDesc != asset.Description){
                await _unitOfWork.AssetRepository.MarkEmbeddingUpdate(request.UpdateAssetDto.Id);
            }

            response.Message = "Update Successful";
            response.Value = Unit.Value;



            return response;
        }
    }
}
using Application.Common.Errors;
using Application.Common.Exceptions;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Command
{
    public class DeleteAssetCommand :IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public int Id {get; set;}
        
    }


    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DeleteAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;
            
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
           var response = new BaseResponse<Unit>();

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.Id);

            if (asset == null)
                return ErrorFactory.NotFound("Asset","Asset Not Found");

            _unitOfWork.AssetRepository.DeleteAsync(asset) ;

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To SaveAsync");

            response.Message = "Deletion Succeeded";
            response.Value = Unit.Value;


            return response;
        }
    }
}
using Application.Common.Responses;
using Application.Contracts.Persistance;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Assets.Command
{
    public class ToggleLikeAssetCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public int Id { get; set; }
        public string UserId { get; set; }

    }


    public class ToggleLikeAssetCommandHandler : IRequestHandler<ToggleLikeAssetCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ToggleLikeAssetCommandHandler(IMapper mapper, IUnitOfWork unitOfwork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfwork;

        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(ToggleLikeAssetCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<Unit>();

            var asset = await _unitOfWork.AssetRepository.ToggleAssetLike(request.Id, request.UserId);
            if (asset.IsError) return asset.Errors;

            await _unitOfWork.SaveAsync();

            response.Message = "Like Toggle Succeeded";
            response.Value = Unit.Value;

            return response;
        }
    }
}
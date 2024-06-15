using MediatR;
using Application.Common.Exceptions;
using Application.Contracts.Persistance;
using ErrorOr;
using Application.Common.Errors;
using Domain.Assets;
using Application.Contracts.Services;

namespace Application.Features.Assets.Command
{
    public class CancelAssetCommand : IRequest<ErrorOr<Unit>>
    {
        public long Id { get; set; }
    }


    public class CancelAssetCommandHandler : IRequestHandler<CancelAssetCommand, ErrorOr<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public CancelAssetCommandHandler(IUnitOfWork unitOfwork, INotificationService notificationService)
        {
            _unitOfWork = unitOfwork;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Unit>> Handle(CancelAssetCommand request, CancellationToken cancellationToken)
        {

            var asset = await _unitOfWork.AssetRepository.GetByIdAsync(request.Id);

            if (asset == null)
                return ErrorFactory.NotFound("Resource", "Resource Not Found");

            if (asset.AuctionId != null)
                return ErrorFactory.BadRequest("Asset", "Auction Asset cancel is not supported by this endpoint");

            asset.Status = AssetStatus.NotOnSale;

            _unitOfWork.AssetRepository.UpdateAsync(asset);

            if (await _unitOfWork.SaveAsync() == 0)
                throw new DbAccessException("Database Error: Unable To SaveAsync");
            await _notificationService.NotifyRemoveAssetFromView(asset.Id);

            return Unit.Value;
        }
    }
}
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Domain.Assets;
using Application.Contracts.Services;


namespace Application.Features.Auctions.Commands
{
    public class AuctionFailureCommand : IRequest<ErrorOr<bool>>
    {
        public long AuctionId;
    }

    public class AuctionFailureCommandHandler
        : IRequestHandler<AuctionFailureCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuctionFailureCommandHandler> _logger;
        private readonly INotificationService _notificationService;

        public AuctionFailureCommandHandler(IUnitOfWork unitOfWork, ILogger<AuctionFailureCommandHandler> logger, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<bool>> Handle(
            AuctionFailureCommand command,
            CancellationToken cancellationToken
        )
        {

            var auction = await _unitOfWork.AuctionRepository.GetByAuctionId(command.AuctionId);

            // if (auction == null)
            //     return ErrorFactory.NotFound("Auction", "Auction not found");
            _logger.LogInformation("*********************************** 1");
            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(auction.TokenId);

            // if (asset == null)
            //     return ErrorFactory.NotFound("Asset", "Asset not found");
            _logger.LogInformation("*********************************** 2");
            asset.Status = AssetStatus.NotOnSale;



            await _unitOfWork.SaveAsync();


            await _notificationService.NotifyRemoveAssetFromView(asset.Id);

            _logger.LogInformation($"\nAuctionFailureEvent\nAuctionId: {command.AuctionId}");
            return true;
        }
    }
}

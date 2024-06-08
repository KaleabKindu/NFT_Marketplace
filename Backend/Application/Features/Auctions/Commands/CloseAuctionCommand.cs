using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Contracts.Persistance;
using Application.Features.Auctions.Dtos;
using Application.Common.Errors;

namespace Application.Features.Auctions.Commands
{
    public class CloseAuctionCommand : IRequest<ErrorOr<bool>>
    {
        public AuctionEndedEventDto _event;
    }

    public class CloseAuctionCommandHandler
        : IRequestHandler<CloseAuctionCommand, ErrorOr<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CloseAuctionCommandHandler> _logger;

        public CloseAuctionCommandHandler(IUnitOfWork unitOfWork, ILogger<CloseAuctionCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ErrorOr<bool>> Handle(
            CloseAuctionCommand command,
            CancellationToken cancellationToken
        )
        {

            var auction = await _unitOfWork.AuctionRepository.GetByIdAsync((long)command._event.AuctionId);

            if (auction == null)
                return ErrorFactory.NotFound("Auction", "Auction not found");

            var asset = await _unitOfWork.AssetRepository.GetAssetByTokenId(auction.TokenId);

            if (asset == null)
                return ErrorFactory.NotFound("Asset", "Asset not found");

            var collection = await _unitOfWork.CollectionRepository.GetByIdAsync((long)asset.CollectionId);

            if (collection == null)
                return ErrorFactory.NotFound("Collection", "Collection not found");


            collection.LatestPrice = auction.HighestBid;
        

            _unitOfWork.CollectionRepository.UpdateAsync(collection);

            if (await _unitOfWork.SaveAsync() == 0)
                return ErrorFactory.InternalServerError("Auction", "Error closing auction");





            _logger.LogInformation($"\nCloseAuctionEvent\nAuctionId: {command._event.AuctionId}\nWinner: {command._event.Winner}");
            return true;
        }
    }
}

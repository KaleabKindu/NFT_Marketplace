// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract NFTAuction {

    using Counters for Counters.Counter;

    Counters.Counter private auctionIds;

    struct Auction {
        uint256 auctionId;
        address tokenContract;
        uint256 tokenId;
        address payable seller;
        uint256 floorPrice;
        uint256 highestBid; 
        address highestBidder; 
        uint256 biddingEnd;
        bool ended;
    }

    mapping(uint256 => Auction) public auctions;

    event AuctionCreated(
        uint256 indexed auctionId,
        address tokenContract,
        uint256 tokenId,
        address seller,
        uint256 floorPrice,
        uint256 biddingEnd
    );

    event BidPlaced(uint256 indexed auctionId, address bidder, uint256 amount);
    event AuctionEnded(uint256 indexed auctionId, address winner, uint256 amount);

    function createAuction(
        address _tokenContract,
        uint256 _tokenId,
        uint256 _floorPrice,
        uint256 _biddingDuration
    ) external returns (uint256)  {
        require(_biddingDuration > 0, "Auction duration must be greater than 0");

        auctionIds.increment();
        uint256 auctionId = auctionIds.current();

        auctions[auctionId] = Auction({
            auctionId: auctionId,
            tokenContract: _tokenContract,
            tokenId: _tokenId,
            seller: payable(msg.sender),
            floorPrice: _floorPrice,
            highestBid: _floorPrice,
            highestBidder: address(0),
            biddingEnd: block.timestamp + _biddingDuration,
            ended: false
        });

        emit AuctionCreated(auctionId, _tokenContract, _tokenId, msg.sender, _floorPrice, auctions[auctionId].biddingEnd);

        return auctionId;
    }

    function placeBid(uint256 _auctionId) external payable {
        Auction storage auction = auctions[_auctionId];

        require(auction.auctionId != 0, "Auction does not exist");
        require(!auction.ended, "Auction has already ended");
        require(block.timestamp < auction.biddingEnd, "Auction has ended");

        require(msg.value > auction.highestBid, "Bid must be greater than the highest bid");

        // Refund the previous highest bidder
        if (auction.seller != address(0)) {
            auction.seller.transfer(auction.highestBid);
        }

        auction.seller = payable(msg.sender);
        auction.highestBid = msg.value;

        emit BidPlaced(_auctionId, msg.sender, msg.value);
    }

    function endAuction(uint256 _auctionId) external {
        Auction storage auction = auctions[_auctionId];

        require(auction.auctionId != 0, "Auction does not exist");
        require(!auction.ended, "Auction has already ended");
        require(block.timestamp >= auction.biddingEnd, "Auction has not ended yet");

        auction.ended = true;

        IERC721(auction.tokenContract).transferFrom(address(this), msg.sender, auction.tokenId);

        payable(auction.seller).transfer(auction.highestBid);

        emit AuctionEnded(_auctionId, auction.seller, auction.floorPrice);
    }
}

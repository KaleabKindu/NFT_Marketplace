
// SPDX-License-Identifier: MIT
pragma solidity  ^0.8.20;

import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "hardhat/console.sol";

contract Marketplace is ERC721URIStorage, Ownable {
    using Counters for Counters.Counter;

    Counters.Counter private _tokenIds;
    Counters.Counter private auctionIds;
    Counters.Counter private _productsSold;

    uint256 _listingPrice = 0.00000000025 ether;

    mapping(uint256 => Product) public idToProduct;
    mapping(uint256 => Auction) public idToAuction;

    struct Product {
        uint256 tokenId;
        address payable creator;
        uint256 price;
        uint256 auctionId;
        uint256 royalty;
    }
    struct Auction {
        uint256 auctionId;
        uint256 tokenId;
        address payable seller;
        uint256 floorPrice;
        uint256 highestBid; 
        address payable highestBidder; 
        uint256 auctionEnd;
    }

    event AuctionCreated(
        uint256 indexed auctionId,
        uint256 tokenId,
        address seller,
        uint256 floorPrice,
        uint256 auctionEnd
    );
    event AuctionCanceled(
        uint256 indexed auctionId,
        address highestBidder,
        uint256 highestBid
    );

    event BidPlaced(uint256 indexed auctionId, address bidder, uint256 amount);
    event AuctionEnded(uint256 indexed auctionId, address winner);
    event AssetSold(uint256 indexed tokenId, address to);
    event ResellAsset(uint256 indexed tokenId, bool auction, uint256 price, uint256 auctionId, uint256 auctionEnd);
    event TransferAsset(uint256 indexed tokenId, address newOwner);
    event DeleteAsset(uint256 tokenid);

    constructor() ERC721("Asset", "AST")
    {
        Ownable(msg.sender);
    }

    function updateListingPrice(uint256 price) public onlyOwner {
        _listingPrice = price;
    }

    function getListingPrice() public view returns(uint256){
        return _listingPrice;
    }

    function mintProduct(
        string memory uri,
        uint256 price,
        bool auction,
        uint256 auctionEnd,
        uint256 royalty
        ) public payable returns(uint256, uint256) {

        require(price > 0, 'Price must greater than 0');
        require(msg.value == _listingPrice, 'Listing Payment must be equal to listing price');
        require(royalty <= 10, 'Royalty percentage must be between 0 and 10');
        _tokenIds.increment();
        
        uint256 nextTokenId =_tokenIds.current();
        _safeMint(msg.sender, nextTokenId);
        _setTokenURI(nextTokenId, uri);
        uint256 auctionId = listProduct(
            nextTokenId,
            msg.sender,
            price,
            auction,
            auctionEnd,
            royalty
        );


        return (nextTokenId, auctionId);
    }

    function getTokenUri(uint256 tokenId) public view returns (string memory) {
        require(_exists(tokenId), "ERC721Metadata: URI query for nonexistent token");
        console.log("Owner %s - Sender %s", ownerOf(tokenId), msg.sender);

        string memory _tokenURI = tokenURI(tokenId);
        console.log("URI %s", _tokenURI);
        return _tokenURI;
    }

    function listProduct(
        uint256 tokenId, 
        address creator,
        uint256 price,
        bool auction,
        uint256 auctionEnd,
        uint256 royalty
        ) private returns(uint256) {
        uint256 auctionId = 0;
        if (auction) {
            auctionId = createAuction(tokenId, price, auctionEnd);
        }
        idToProduct[tokenId] = Product(
             tokenId,
             payable(creator),
             price,
             auctionId,
             royalty
        );
        return auctionId;
    }

    function resellProduct(uint256 tokenId, uint256 price, bool auction, uint256 auctionEnd) public payable{

        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can resale this Product");

        idToProduct[tokenId].price = price;

        if(auction){
            idToAuction[idToProduct[tokenId].auctionId].floorPrice = price;
            idToAuction[idToProduct[tokenId].auctionId].auctionEnd = auctionEnd;
        }
        emit ResellAsset(tokenId, auction, price, idToProduct[tokenId].auctionId, auctionEnd);
    }

    function changePrice(uint256 tokenId, uint256 newPrice) public {
        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can change the price");
        uint256 price = idToProduct[tokenId].price;
        console.log("NFT Original Price %s", price);
        idToProduct[tokenId].price = newPrice;
        console.log("NFT New Price %s", idToProduct[tokenId].price);
    }

    function buyAsset( uint256 tokenId) public payable {
        uint256 price = idToProduct[tokenId].price;
        uint256 royalty = (price * idToProduct[tokenId].royalty) / 100;
        console.log("Price %s - Value %s - Equal %s", price, msg.value, msg.value == price);
        require(msg.value == price, "Please Top up the asking price to purchase the product");
        console.log("Price %s and Royalty %s", price, royalty);
        _productsSold.increment();

        address owner = ownerOf(tokenId);
        address creator = idToProduct[tokenId].creator;

        if(creator != owner){
            console.log("Transfer %s Ether to %s", price - royalty, owner);
            payable(owner).transfer(price - royalty);
            console.log("Transfer Complete");
            console.log("Royalty Transfer %s Ether to %s", royalty, creator);
            payable(creator).transfer(royalty); 
            console.log("Transfer Complete");
        }else{
            console.log("Transfer %s Ether to %s", price, owner);
            payable(owner).transfer(price);
            console.log("Transfer Complete");  
        }

        console.log("NFT Transfer Begin - Owner %s", ownerOf(tokenId));
        _transfer(ownerOf(tokenId), msg.sender, tokenId);
        console.log("NFT Transfer Complete - Owner %s", ownerOf(tokenId));

        emit AssetSold(tokenId, msg.sender);
    }

    function transferAsset(uint256 tokenId, address to) public {
        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can change the price");
        console.log("NFT Transfer Begin - Owner %s", ownerOf(tokenId));
        _transfer(ownerOf(tokenId), to, tokenId);
        console.log("NFT Transfer Complete - Owner %s", ownerOf(tokenId));
        emit TransferAsset(tokenId, to);
    }

    function deleteAsset(uint256 tokenId) public {
        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can delete the asset");
        console.log("NFT Burn Begin - Owner %s", ownerOf(tokenId));
        _burn(tokenId);
        console.log("NFT Burn Complete - Asset Burnt %s", _exists(tokenId));   
        if(idToProduct[tokenId].auctionId != 0){
            delete idToAuction[idToProduct[tokenId].auctionId];
        }
        delete idToProduct[tokenId];
        emit DeleteAsset(tokenId);
    }


    // Auction Related Functions
    function createAuction(
        uint256 _tokenId,
        uint256 _floorPrice,
        uint256 auctionEnd
    ) private returns (uint256)  {
        require(auctionEnd > block.timestamp, "Auction End must be different from now");

        auctionIds.increment();
        uint256 auctionId = auctionIds.current();

        idToAuction[auctionId] = Auction({
            auctionId: auctionId,
            tokenId: _tokenId,
            seller: payable(msg.sender),
            floorPrice: _floorPrice,
            highestBid: _floorPrice,
            highestBidder: payable(address(0)),
            auctionEnd: auctionEnd
        });

        emit AuctionCreated(auctionId, _tokenId, msg.sender, _floorPrice, auctionEnd);

        return auctionId;
    }


    function placeBid(uint256 _auctionId) external payable {
        Auction storage auction = idToAuction[_auctionId];
        require(auction.auctionId != 0, "Auction does not exist");
        require(block.timestamp < auction.auctionEnd, "Auction has already ended");

        require(msg.value > auction.highestBid, "Bid must be greater than the highest bid");

        if(auction.highestBidder != address(0)){
        // Refund the previous highest bidder
        auction.highestBidder.transfer(auction.highestBid);
        }

        idToAuction[_auctionId].highestBid = msg.value;
        idToAuction[_auctionId].highestBidder = payable(msg.sender);

        emit BidPlaced(_auctionId, msg.sender, msg.value);
    }

    function endAuction(uint256 _auctionId) external {
        Auction storage auction = idToAuction[_auctionId];
        Product storage product = idToProduct[auction.tokenId];
        uint256 price = auction.highestBid;
        uint256 royaltyAmount = (price * product.royalty) / 100;

        console.log("time %s %s %s", block.timestamp, auction.auctionEnd, block.timestamp >= auction.auctionEnd);
        require(auction.auctionId != 0, "Auction does not exist");
        require(auction.highestBidder != address(0), "No Bids Found on Auction");

        if(product.creator != ownerOf(product.tokenId)){
            console.log("Transfer %s Ether to %s", price - royaltyAmount, auction.seller);
            payable(auction.seller).transfer(price  - royaltyAmount);
            console.log("Transfer Complete");
            console.log("Royalty Transfer %s Ether to %s", royaltyAmount, product.creator);
            payable(product.creator).transfer(royaltyAmount); 
            console.log("Transfer Complete");
        }else{
            console.log('Transfer %s Ether to %s', price, auction.seller);
            payable(auction.seller).transfer(price);
            console.log('Ether Transfer Complete');  
        }
        console.log("Transfer %s to %s", auction.tokenId, auction.highestBidder);
        _transfer(ownerOf(auction.tokenId), auction.highestBidder, auction.tokenId);
        console.log('NFT Transfer Complete');
        
        emit AuctionEnded(_auctionId, auction.highestBidder);
    }

    function cancelAuction(uint256 tokenId, uint256 _auctionId) external {
        Auction storage auction = idToAuction[_auctionId];
        require(auction.auctionId != 0, "Auction does not exist");
        require(block.timestamp < auction.auctionEnd, "Auction has already ended");

        require(msg.sender == ownerOf(tokenId), "Only owner of the asset can cancel the auction.");

        if(auction.highestBidder != address(0)){
            // Refund the previous highest bidder
            auction.highestBidder.transfer(auction.highestBid);
            emit AuctionCanceled(_auctionId, auction.highestBidder, auction.highestBid);
            }  
        }
    }




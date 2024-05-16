
// SPDX-License-Identifier: MIT
pragma solidity  ^0.8.20;

import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "hardhat/console.sol";

contract MyNFT is ERC721URIStorage, Ownable {
    using Counters for Counters.Counter;

    Counters.Counter private _tokenIds;
    Counters.Counter private auctionIds;
    Counters.Counter private _productsSold;

    uint256 _listingPrice = 0.00000000025 ether;

    mapping(uint256 => Product) public idToProduct;
    mapping(uint256 => Auction) public idToAuction;


    Product[] public Products;

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

    event ProductCreated(
        uint256 tokenId,
        address payable creator,
        uint256 price,
        uint256 auctionId,
        uint256 royalty
    );
    event AuctionCreated(
        uint256 indexed auctionId,
        uint256 tokenId,
        address seller,
        uint256 floorPrice,
        uint256 auctionEnd
    );

    event BidPlaced(uint256 indexed auctionId, address bidder, uint256 amount);
    event AuctionEnded(uint256 indexed auctionId, address winner, uint256 amount);
    event TransferAsset(uint256 indexed tokenId, address newOwner);
    event DeleteAsset(uint256 tokenid);

    constructor() ERC721("NFT Token", "MTK")
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
        ) public payable returns(uint256) {

        require(price > 0, 'Price must greater than 0');
        require(msg.value == _listingPrice, 'Listing Payment must be equal to listing price');
        require(royalty <= 10, 'Royalty percentage must be between 0 and 10');

        _tokenIds.increment();
        
        uint256 nextTokenId =_tokenIds.current();
        _safeMint(msg.sender, nextTokenId);
        _setTokenURI(nextTokenId, uri);

        createProduct(
            nextTokenId,
            price,
            auction,
            auctionEnd,
            royalty
        );


        return nextTokenId;
    }

    function createProduct(
        uint256 tokenId, 
        uint256 price,
        bool auction,
        uint256 auctionEnd,
        uint256 royalty
        ) private {
        uint256 auctionId = 0;
        if (auction) {
            auctionId = createAuction(tokenId, price, auctionEnd);
        }
        idToProduct[tokenId] = Product(
             tokenId,
             payable(msg.sender),
             price,
             auctionId,
             royalty
        );
        Products.push(idToProduct[tokenId]);

        emit ProductCreated(
            tokenId,
            payable(msg.sender),
            price,
            auctionId,
            royalty
        );
    }

    function resellProduct(uint256 tokenId, uint256 price, bool auction, uint256 auctionEnd) public payable{

        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can resale this Product");
        require(msg.value == _listingPrice, "Listing Payment must be equal to listing price");

        idToProduct[tokenId].price = price;

        if(auction){
            restartAuction(idToProduct[tokenId].auctionId, price, auctionEnd);
        }
        _productsSold.decrement();
    }

    function buyAsset( uint256 tokenId) public payable {
        uint256 price = idToProduct[tokenId].price;
        uint256 royaltyAmount = (price * idToProduct[tokenId].royalty) / 100;
        console.log("Price %s - Value %s - Equal %s", price, msg.value, msg.value == price);
        require(msg.value == price, "Please Top up the asking price to purchase the product");
        console.log("Price %s and Royalty %s", price, royaltyAmount);
        _productsSold.increment();

        if(idToProduct[tokenId].creator != ownerOf(tokenId)){
            console.log("Transfer %s Ether to %s", price - royaltyAmount, ownerOf(tokenId));
            payable(ownerOf(tokenId)).transfer(price - royaltyAmount);
            console.log("Transfer Complete");
            console.log("Royalty Transfer %s Ether to %s", royaltyAmount, idToProduct[tokenId].creator);
            payable(idToProduct[tokenId].creator).transfer(royaltyAmount); 
            console.log("Transfer Complete");
        }else{
            console.log("Transfer %s Ether to %s", price, ownerOf(tokenId));
            payable(ownerOf(tokenId)).transfer(price);
            console.log("Transfer Complete");  
        }

        console.log("NFT Transfer Begin - Owner %s", ownerOf(tokenId));
        _transfer(ownerOf(tokenId), msg.sender, tokenId);
        console.log("NFT Transfer Complete - Owner %s", ownerOf(tokenId));
    }
    function transferAsset(uint256 tokenId) public onlyOwner {
        console.log("NFT Transfer Begin - Owner %s", ownerOf(tokenId));
        _transfer(ownerOf(tokenId), msg.sender, tokenId);
        console.log("NFT Transfer Complete - Owner %s", ownerOf(tokenId));
        emit TransferAsset(tokenId, msg.sender);
    }
    function deleteAsset(uint256 tokenId) public onlyOwner {
        console.log("NFT Burn Begin - Owner %s", ownerOf(tokenId));
        _burn(tokenId);
        console.log("NFT Burn Complete - Owner %s", ownerOf(tokenId));   
        if(idToProduct[tokenId].auctionId != 0){
            delete idToAuction[idToProduct[tokenId].auctionId];
        }
        delete idToProduct[tokenId];
        emit DeleteAsset(tokenId);
    }

    function fetchMarketProducts() public view returns(Product[] memory){
        return Products;
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

    function restartAuction(
        uint256 _auctionId,
        uint256 _floorPrice,
        uint256 auctionEnd
    ) public onlyOwner {
        idToAuction[_auctionId].floorPrice = _floorPrice;
        idToAuction[_auctionId].auctionEnd = auctionEnd;
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
        
        emit AuctionEnded(_auctionId, auction.seller, auction.floorPrice);
    }

}




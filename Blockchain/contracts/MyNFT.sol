
// SPDX-License-Identifier: MIT
pragma solidity  ^0.8.20;

import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/token/ERC721/ERC721.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "./Auction.sol";

contract MyNFT is ERC721URIStorage, Ownable {
    using Counters for Counters.Counter;

    Counters.Counter private _tokenIds;
    Counters.Counter private _productsSold;

    uint256 _listingPrice = 0.00000000025 ether;

    mapping(uint256 => Product) public idToProduct;
    mapping(uint256 => NFTAuction) public idToAuction;


    Product[] public Products;

    struct Product {
        uint256 tokenId;
        address payable creator;
        uint256 price;
        address auctionAddress;
        uint256 royalty;
    }

    event productCreated(
        uint256 tokenId,
        address payable creator,
        uint256 price,
        address auctionAddress,
        uint256 royalty
    );


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
        address auctionAddress = address(0);
        if (auction) {
            NFTAuction auctionContract = new NFTAuction(msg.sender);
            auctionAddress = auctionContract.createAuction(address(this), tokenId, price, auctionEnd);

            idToAuction[tokenId] = auctionContract;
        }
        idToProduct[tokenId] = Product(
             tokenId,
             payable(msg.sender),
             price,
             auctionAddress,
             royalty
        );
        Products.push(idToProduct[tokenId]);

        _transfer(msg.sender, address(this), tokenId);

        emit productCreated(
            tokenId,
            payable(msg.sender),
            price,
            auctionAddress,
            royalty
        );
    }

    function resellProduct(uint256 tokenId, uint256 price, bool auction, uint256 auctionEnd) public payable{

        require(ownerOf(tokenId) == msg.sender, "Only Owner of the NFT can resale this Product");
        require(msg.value == _listingPrice, "Listing Payment must be equal to listing price");

        idToProduct[tokenId].price = price;

        if(auction){
            idToAuction[tokenId].restartAuction(tokenId, price, auctionEnd);
        }
        _productsSold.decrement();
        _transfer(msg.sender, address(this), tokenId);

    }

    function buyProduct( uint256 tokenId) public payable {
        uint256 price = idToProduct[tokenId].price;
        uint256 royaltyAmount = (price * idToProduct[tokenId].royalty) / 100;
        require(msg.value == price, "Please Top up the asking price to purchase the product");

        _productsSold.increment();
        _transfer(ownerOf(tokenId), msg.sender, tokenId);

        payable(owner()).transfer(_listingPrice);
        if(idToProduct[tokenId].creator == ownerOf(tokenId)){
            payable(ownerOf(tokenId)).transfer(price);
        }else{
            payable(ownerOf(tokenId)).transfer(price - royaltyAmount);
            payable(idToProduct[tokenId].creator).transfer(royaltyAmount); 
        }
    }


    function fetchMarketProducts() public view returns(Product[] memory){
        return Products;
    }

}




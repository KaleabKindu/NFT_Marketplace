
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

    uint256 _listingPrice = 0.0025 ether;

    mapping(uint256 => Product) public idToProduct;


    Product[] public Products;

    struct Product {
        uint256 tokenId;
        string name;
        string description;
        string image;
        string files;
        address payable seller;
        address payable owner;
        address payable creator;
        uint256 price;
        bool sold;
        uint256 auctionId;
        uint256 royalty;
    }

    event productCreated(
        uint256 tokenId,
        string name,
        string description,
        string image,
        string files,
        address payable seller,
        address payable owner,
        address payable creator,
        uint256 price,
        bool sold,
        uint256 auctionId,
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
        string memory name,
        string memory description,
        string memory image,
        string memory files,
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
        _setTokenURI(nextTokenId, files);

        createProduct(
            nextTokenId,
            name,
            description,
            image,
            files,
            price,
            auction,
            auctionEnd,
            royalty
        );


        return nextTokenId;
    }

    function createProduct(
        uint256 tokenId, 
        string memory name,
        string memory description,
        string memory image,
        string memory files,
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
             name,
             description,
             image,
             files,
             payable(msg.sender),
             payable(address(this)),  
             payable(msg.sender),
             price,
             false,
             auctionId,
             royalty
        );
        Products.push(idToProduct[tokenId]);

        _transfer(msg.sender, address(this), tokenId);

        emit productCreated(
            tokenId,
            name,
            description,
            image,
            files,
            payable(msg.sender),
            payable(address(this)),  
            payable(msg.sender),
            price,
            false,
            auctionId,
            royalty
        );
    }

    function createAuction(uint256 tokenId, uint256 floorPrice, uint256 auctionEnd) public returns (uint256){
        // Deploy the auction contract
        NFTAuction auctionContract = new NFTAuction();

        // Create the auction
        uint256 auctionId = auctionContract.createAuction(address(this), tokenId, floorPrice, auctionEnd);

        // Update the product with the auction details
        idToProduct[tokenId].auctionId = auctionId;
        return auctionId;
    }

    function resellProduct(uint256 tokenId, uint256 price) public payable{

        require(idToProduct[tokenId].owner == msg.sender, "Only Owner of the NFT can resale this Product");
        require(msg.value == _listingPrice, "Listing Payment must be equal to listing price");

        idToProduct[tokenId].sold = false;
        idToProduct[tokenId].price = price;
        idToProduct[tokenId].seller = payable(msg.sender);
        idToProduct[tokenId].owner = payable(address(this));

        _productsSold.decrement();
        _transfer(msg.sender, address(this), tokenId);

    }

    function buyProduct( uint256 tokenId) public payable {
        uint256 price = idToProduct[tokenId].price;
        uint256 royaltyAmount = (price * idToProduct[tokenId].royalty) / 100;
        require(msg.value == price, "Please Top up the asking price to purchase the product");

        idToProduct[tokenId].owner = payable(msg.sender);
        idToProduct[tokenId].sold = true;

        _productsSold.increment();
        _transfer(address(this), msg.sender, tokenId);

        payable(owner()).transfer(_listingPrice);
        if(idToProduct[tokenId].seller == idToProduct[tokenId].creator){
            payable(idToProduct[tokenId].seller).transfer(price);
        }else{
            payable(idToProduct[tokenId].seller).transfer(price - royaltyAmount);
            payable(idToProduct[tokenId].creator).transfer(royaltyAmount); 
        }
    }


    function fetchMarketProducts() public view returns(Product[] memory){
        uint256 itemsCount = _tokenIds.current();
        uint256 unsoldCount = _tokenIds.current() - _productsSold.current();

        Product[] memory items = new Product[](unsoldCount);

        uint256 currentIndex = 0;

        for(uint256 i = 0; i < itemsCount; i++){
            Product storage currentProduct = idToProduct[i + 1];
            if(currentProduct.owner == address(this)){
                items[currentIndex] = currentProduct;
                currentIndex++;
            }
        }

        return items;
    }

    function fetchMyProducts() public view returns(Product[] memory){
        uint256 itemsCount = _tokenIds.current();
        uint256 myProducts = 0;

        for(uint256 i = 0; i < itemsCount; i++){
            Product storage currentProduct = idToProduct[i + 1];
            if(currentProduct.owner == msg.sender){
                myProducts++;
            }
        }

        Product[] memory items = new Product[](myProducts);
        uint256 currentIndex = 0;

        for(uint256 i = 0; i < itemsCount; i++){
            Product storage currentProduct = Products[i];
            if(currentProduct.owner == msg.sender){
                items[currentIndex] = currentProduct;
                currentIndex++;
            }
        }

        return items;
    }



    function fetchProductsListed() public view returns(Product[] memory){
        uint256 itemsCount = _tokenIds.current();
        uint256 myProducts = 0;

        for(uint256 i = 0; i < itemsCount; i++){
            Product storage currentProduct = idToProduct[i + 1];
            if(currentProduct.seller == msg.sender){
                myProducts++;
            }
        }

        Product[] memory items = new Product[](myProducts);
        uint256 currentIndex = 0;

        for(uint256 i = 0; i < itemsCount; i++){
            Product storage currentProduct = idToProduct[i + 1];
            if(currentProduct.seller == msg.sender){
                items[currentIndex] = currentProduct;
                currentIndex++;
            }
        }

        return items;
    }























    function _baseURI() internal pure override returns (string memory) {
        return "ipfs://";
    }
    // The following functions are overrides required by Solidity.

    function tokenURI(uint256 tokenId)
        public
        view
        override(ERC721URIStorage)
        returns (string memory)
    {
        return super.tokenURI(tokenId);
    }

    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC721URIStorage)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }
}




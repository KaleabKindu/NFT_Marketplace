using Domain.Auctions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class AuctionEntityConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        // builder.HasData(
        //     new Auction
        //     {
        //         Id = 1,
        //         AuctionId = 1,
        //         TokenId = 1,
        //         SellerId = "123e4567-e89b-12d3-a456-426614174000",
        //         FloorPrice = 1.0,
        //         HighestBid = 1.6,
        //         HighestBidderId = "111e2222-e89b-12d3-a456-426614174001",
        //         AuctionEnd = 1633027200 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 2,
        //         AuctionId = 2,
        //         TokenId = 2,
        //         SellerId = "333e4444-e89b-12d3-a456-426614174002",
        //         FloorPrice = 2.0,
        //         HighestBid = 2.6,
        //         HighestBidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AuctionEnd = 1633113600 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 3,
        //         AuctionId = 3,
        //         TokenId = 3,
        //         SellerId = "555e6666-e89b-12d3-a456-426614174004",
        //         FloorPrice = 3.0,
        //         HighestBid = 3.6,
        //         HighestBidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AuctionEnd = 1633200000 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 4,
        //         AuctionId = 4,
        //         TokenId = 4,
        //         SellerId = "777e8888-e89b-12d3-a456-426614174006",
        //         FloorPrice = 4.0,
        //         HighestBid = 4.6,
        //         HighestBidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AuctionEnd = 1633286400 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 5,
        //         AuctionId = 5,
        //         TokenId = 5,
        //         SellerId = "999e0000-e89b-12d3-a456-426614174008",
        //         FloorPrice = 5.0,
        //         HighestBid = 5.6,
        //         HighestBidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AuctionEnd = 1633372800 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 6,
        //         AuctionId = 6,
        //         TokenId = 6,
        //         SellerId = "000e1111-e89b-12d3-a456-426614174009",
        //         FloorPrice = 6.0,
        //         HighestBid = 6.6,
        //         HighestBidderId = "111e2222-e89b-12d3-a456-426614174001",
        //         AuctionEnd = 1633459200 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 7,
        //         AuctionId = 7,
        //         TokenId = 7,
        //         SellerId = "111e2222-e89b-12d3-a456-426614174010",
        //         FloorPrice = 7.0,
        //         HighestBid = 7.6,
        //         HighestBidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AuctionEnd = 1633545600 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 8,
        //         AuctionId = 8,
        //         TokenId = 8,
        //         SellerId = "222e3333-e89b-12d3-a456-426614174011",
        //         FloorPrice = 8.0,
        //         HighestBid = 8.6,
        //         HighestBidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AuctionEnd = 1633632000 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 9,
        //         AuctionId = 9,
        //         TokenId = 9,
        //         SellerId = "333e4444-e89b-12d3-a456-426614174012",
        //         FloorPrice = 9.0,
        //         HighestBid = 9.6,
        //         HighestBidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AuctionEnd = 1633718400 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 10,
        //         AuctionId = 10,
        //         TokenId = 10,
        //         SellerId = "444e5555-e89b-12d3-a456-426614174013",
        //         FloorPrice = 10.0,
        //         HighestBid = 10.6,
        //         HighestBidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AuctionEnd = 1633804800 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 11,
        //         AuctionId = 11,
        //         TokenId = 11,
        //         SellerId = "555e6666-e89b-12d3-a456-426614174014",
        //         FloorPrice = 11.0,
        //         HighestBid = 11.6,
        //         HighestBidderId = "666e7777-e89b-12d3-a456-426614174005",
        //         AuctionEnd = 1633891200 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 12,
        //         AuctionId = 12,
        //         TokenId = 12,
        //         SellerId = "666e7777-e89b-12d3-a456-426614174005",
        //         FloorPrice = 12.0,
        //         HighestBid = 12.6,
        //         HighestBidderId = "777e8888-e89b-12d3-a456-426614174006",
        //         AuctionEnd = 1633977600 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 13,
        //         AuctionId = 13,
        //         TokenId = 13,
        //         SellerId = "777e8888-e89b-12d3-a456-426614174006",
        //         FloorPrice = 13.0,
        //         HighestBid = 13.6,
        //         HighestBidderId = "888e9999-e89b-12d3-a456-426614174007",
        //         AuctionEnd = 1634064000 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 14,
        //         AuctionId = 14,
        //         TokenId = 14,
        //         SellerId = "888e9999-e89b-12d3-a456-426614174007",
        //         FloorPrice = 14.0,
        //         HighestBid = 14.6,
        //         HighestBidderId = "999e0000-e89b-12d3-a456-426614174008",
        //         AuctionEnd = 1634150400 // represents a Unix timestamp for a future date
        //     },
        //     new Auction
        //     {
        //         Id = 15,
        //         AuctionId = 15,
        //         TokenId = 15,
        //         SellerId = "999e0000-e89b-12d3-a456-426614174008",
        //         FloorPrice = 15.0,
        //         HighestBid = 15.6,
        //         HighestBidderId = "000e1111-e89b-12d3-a456-426614174009",
        //         AuctionEnd = 1634236800 // represents a Unix timestamp for a future date
        //     }
        // );		
    }
}
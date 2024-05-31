using Domain.Bids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class BidEntityConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        // builder.HasData(
        //     new Bid
        //     {
        //         Id = 1,
        //         BidderId = "123e4567-e89b-12d3-a456-426614174000",
        //         AssetId = 1,
        //         Amount = 1.2,
        //         TransactionHash = "hash1"
        //     },
        //     new Bid
        //     {
        //         Id = 2,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174001",
        //         AssetId = 1,
        //         Amount = 1.4,
        //         TransactionHash = "hash2"
        //     },
        //     new Bid
        //     {
        //         Id = 3,
        //         BidderId = "333e4444-e89b-12d3-a456-426614174002",
        //         AssetId = 1,
        //         Amount = 1.6,
        //         TransactionHash = "hash3"
        //     },
        //     new Bid
        //     {
        //         Id = 4,
        //         BidderId = "123e4567-e89b-12d3-a456-426614174000",
        //         AssetId = 2,
        //         Amount = 2.2,
        //         TransactionHash = "hash4"
        //     },
        //     new Bid
        //     {
        //         Id = 5,
        //         BidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AssetId = 2,
        //         Amount = 2.4,
        //         TransactionHash = "hash5"
        //     },
        //     new Bid
        //     {
        //         Id = 6,
        //         BidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AssetId = 2,
        //         Amount = 2.6,
        //         TransactionHash = "hash6"
        //     },
        //     new Bid
        //     {
        //         Id = 7,
        //         BidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AssetId = 3,
        //         Amount = 3.2,
        //         TransactionHash = "hash7"
        //     },
        //     new Bid
        //     {
        //         Id = 8,
        //         BidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AssetId = 3,
        //         Amount = 3.4,
        //         TransactionHash = "hash8"
        //     },
        //     new Bid
        //     {
        //         Id = 9,
        //         BidderId = "666e7777-e89b-12d3-a456-426614174005",
        //         AssetId = 3,
        //         Amount = 3.6,
        //         TransactionHash = "hash9"
        //     },
        //     new Bid
        //     {
        //         Id = 10,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 4,
        //         Amount = 4.2,
        //         TransactionHash = "hash10"
        //     },
        //     new Bid
        //     {
        //         Id = 11,
        //         BidderId = "777e8888-e89b-12d3-a456-426614174006",
        //         AssetId = 4,
        //         Amount = 4.4,
        //         TransactionHash = "hash11"
        //     },
        //     new Bid
        //     {
        //         Id = 12,
        //         BidderId = "888e9999-e89b-12d3-a456-426614174007",
        //         AssetId = 4,
        //         Amount = 4.6,
        //         TransactionHash = "hash12"
        //     },
        //     new Bid
        //     {
        //         Id = 13,
        //         BidderId = "999e0000-e89b-12d3-a456-426614174008",
        //         AssetId = 5,
        //         Amount = 5.2,
        //         TransactionHash = "hash13"
        //     },
        //     new Bid
        //     {
        //         Id = 14,
        //         BidderId = "000e1111-e89b-12d3-a456-426614174009",
        //         AssetId = 5,
        //         Amount = 5.4,
        //         TransactionHash = "hash14"
        //     },
        //     new Bid
        //     {
        //         Id = 15,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 5,
        //         Amount = 5.6,
        //         TransactionHash = "hash15"
        //     },
        //     new Bid
        //     {
        //         Id = 16,
        //         BidderId = "123e4567-e89b-12d3-a456-426614174000",
        //         AssetId = 6,
        //         Amount = 6.2,
        //         TransactionHash = "hash16"
        //     },
        //     new Bid
        //     {
        //         Id = 17,
        //         BidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AssetId = 6,
        //         Amount = 6.4,
        //         TransactionHash = "hash17"
        //     },
        //     new Bid
        //     {
        //         Id = 18,
        //         BidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AssetId = 6,
        //         Amount = 6.6,
        //         TransactionHash = "hash18"
        //     },
        //     new Bid
        //     {
        //         Id = 19,
        //         BidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AssetId = 7,
        //         Amount = 7.2,
        //         TransactionHash = "hash19"
        //     },
        //     new Bid
        //     {
        //         Id = 20,
        //         BidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AssetId = 7,
        //         Amount = 7.4,
        //         TransactionHash = "hash20"
        //     },
        //     new Bid
        //     {
        //         Id = 21,
        //         BidderId = "666e7777-e89b-12d3-a456-426614174005",
        //         AssetId = 7,
        //         Amount = 7.6,
        //         TransactionHash = "hash21"
        //     },
        //     new Bid
        //     {
        //         Id = 22,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 8,
        //         Amount = 8.2,
        //         TransactionHash = "hash22"
        //     },
        //     new Bid
        //     {
        //         Id = 23,
        //         BidderId = "777e8888-e89b-12d3-a456-426614174006",
        //         AssetId = 8,
        //         Amount = 8.4,
        //         TransactionHash = "hash23"
        //     },
        //     new Bid
        //     {
        //         Id = 24,
        //         BidderId = "888e9999-e89b-12d3-a456-426614174007",
        //         AssetId = 8,
        //         Amount = 8.6,
        //         TransactionHash = "hash24"
        //     },
        //     new Bid
        //     {
        //         Id = 25,
        //         BidderId = "999e0000-e89b-12d3-a456-426614174008",
        //         AssetId = 9,
        //         Amount = 9.2,
        //         TransactionHash = "hash25"
        //     },
        //     new Bid
        //     {
        //         Id = 26,
        //         BidderId = "000e1111-e89b-12d3-a456-426614174009",
        //         AssetId = 9,
        //         Amount = 9.4,
        //         TransactionHash = "hash26"
        //     },
        //     new Bid
        //     {
        //         Id = 27,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 9,
        //         Amount = 9.6,
        //         TransactionHash = "hash27"
        //     },
        //     new Bid
        //     {
        //         Id = 28,
        //         BidderId = "123e4567-e89b-12d3-a456-426614174000",
        //         AssetId = 10,
        //         Amount = 10.2,
        //         TransactionHash = "hash28"
        //     },
        //     new Bid
        //     {
        //         Id = 29,
        //         BidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AssetId = 10,
        //         Amount = 10.4,
        //         TransactionHash = "hash29"
        //     },
        //     new Bid
        //     {
        //         Id = 30,
        //         BidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AssetId = 10,
        //         Amount = 10.6,
        //         TransactionHash = "hash30"
        //     },
        //     new Bid
        //     {
        //         Id = 31,
        //         BidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AssetId = 11,
        //         Amount = 11.2,
        //         TransactionHash = "hash31"
        //     },
        //     new Bid
        //     {
        //         Id = 32,
        //         BidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AssetId = 11,
        //         Amount = 11.4,
        //         TransactionHash = "hash32"
        //     },
        //     new Bid
        //     {
        //         Id = 33,
        //         BidderId = "666e7777-e89b-12d3-a456-426614174005",
        //         AssetId = 11,
        //         Amount = 11.6,
        //         TransactionHash = "hash33"
        //     },
        //     new Bid
        //     {
        //         Id = 34,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 12,
        //         Amount = 12.2,
        //         TransactionHash = "hash34"
        //     },
        //     new Bid
        //     {
        //         Id = 35,
        //         BidderId = "777e8888-e89b-12d3-a456-426614174006",
        //         AssetId = 12,
        //         Amount = 12.4,
        //         TransactionHash = "hash35"
        //     },
        //     new Bid
        //     {
        //         Id = 36,
        //         BidderId = "888e9999-e89b-12d3-a456-426614174007",
        //         AssetId = 12,
        //         Amount = 12.6,
        //         TransactionHash = "hash36"
        //     },
        //     new Bid
        //     {
        //         Id = 37,
        //         BidderId = "999e0000-e89b-12d3-a456-426614174008",
        //         AssetId = 13,
        //         Amount = 13.2,
        //         TransactionHash = "hash37"
        //     },
        //     new Bid
        //     {
        //         Id = 38,
        //         BidderId = "000e1111-e89b-12d3-a456-426614174009",
        //         AssetId = 13,
        //         Amount = 13.4,
        //         TransactionHash = "hash38"
        //     },
        //     new Bid
        //     {
        //         Id = 39,
        //         BidderId = "111e2222-e89b-12d3-a456-426614174010",
        //         AssetId = 13,
        //         Amount = 13.6,
        //         TransactionHash = "hash39"
        //     },
        //     new Bid
        //     {
        //         Id = 40,
        //         BidderId = "123e4567-e89b-12d3-a456-426614174000",
        //         AssetId = 14,
        //         Amount = 14.2,
        //         TransactionHash = "hash40"
        //     },
        //     new Bid
        //     {
        //         Id = 41,
        //         BidderId = "222e3333-e89b-12d3-a456-426614174011",
        //         AssetId = 14,
        //         Amount = 14.4,
        //         TransactionHash = "hash41"
        //     },
        //     new Bid
        //     {
        //         Id = 42,
        //         BidderId = "333e4444-e89b-12d3-a456-426614174012",
        //         AssetId = 14,
        //         Amount = 14.6,
        //         TransactionHash = "hash42"
        //     },
        //     new Bid
        //     {
        //         Id = 43,
        //         BidderId = "444e5555-e89b-12d3-a456-426614174013",
        //         AssetId = 15,
        //         Amount = 15.2,
        //         TransactionHash = "hash43"
        //     },
        //     new Bid
        //     {
        //         Id = 44,
        //         BidderId = "555e6666-e89b-12d3-a456-426614174014",
        //         AssetId = 15,
        //         Amount = 15.4,
        //         TransactionHash = "hash44"
        //     },
        //     new Bid
        //     {
        //         Id = 45,
        //         BidderId = "666e7777-e89b-12d3-a456-426614174005",
        //         AssetId = 15,
        //         Amount = 15.6,
        //         TransactionHash = "hash45"
        //     }
        // );
    }
}
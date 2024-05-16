using Domain.Provenances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class ProvenanceEntityConfiguration : IEntityTypeConfiguration<Provenance>
{
	public void Configure(EntityTypeBuilder<Provenance> builder)
	{
        builder.HasData(
            new Provenance
            {
                Id = 11,
                AssetId = 6,
                Event = Event.Mint,
                FromId = "666e7777-e89b-12d3-a456-426614174005",
                ToId = "666e7777-e89b-12d3-a456-426614174005",
                Price = 0.0,
                TransactionHash = "hash11",
            },
            new Provenance
            {
                Id = 12,
                AssetId = 7,
                Event = Event.Mint,
                FromId = "777e8888-e89b-12d3-a456-426614174006",
                ToId = "777e8888-e89b-12d3-a456-426614174006",
                Price = 0.0,
                TransactionHash = "hash12",
            },
            new Provenance
            {
                Id = 13,
                AssetId = 8,
                Event = Event.Mint,
                FromId = "888e9999-e89b-12d3-a456-426614174007",
                ToId = "888e9999-e89b-12d3-a456-426614174007",
                Price = 0.0,
                TransactionHash = "hash13",
            },
            new Provenance
            {
                Id = 14,
                AssetId = 9,
                Event = Event.Mint,
                FromId = "999e0000-e89b-12d3-a456-426614174008",
                ToId = "999e0000-e89b-12d3-a456-426614174008",
                Price = 0.0,
                TransactionHash = "hash14",
            },
            new Provenance
            {
                Id = 15,
                AssetId = 10,
                Event = Event.Mint,
                FromId = "000e1111-e89b-12d3-a456-426614174009",
                ToId = "000e1111-e89b-12d3-a456-426614174009",
                Price = 0.0,
                TransactionHash = "hash15",
            },
            new Provenance
            {
                Id = 16,
                AssetId = 6,
                Event = Event.Sale,
                FromId = "666e7777-e89b-12d3-a456-426614174005",
                ToId = "777e8888-e89b-12d3-a456-426614174006",
                Price = 6.0,
                TransactionHash = "hash16",
            },
            new Provenance
            {
                Id = 17,
                AssetId = 7,
                Event = Event.Sale,
                FromId = "777e8888-e89b-12d3-a456-426614174006",
                ToId = "888e9999-e89b-12d3-a456-426614174007",
                Price = 7.0,
                TransactionHash = "hash17",
            },
            new Provenance
            {
                Id = 18,
                AssetId = 8,
                Event = Event.Sale,
                FromId = "888e9999-e89b-12d3-a456-426614174007",
                ToId = "999e0000-e89b-12d3-a456-426614174008",
                Price = 8.0,
                TransactionHash = "hash18",
            },
            new Provenance
            {
                Id = 19,
                AssetId = 9,
                Event = Event.Sale,
                FromId = "999e0000-e89b-12d3-a456-426614174008",
                ToId = "000e1111-e89b-12d3-a456-426614174009",
                Price = 9.0,
                TransactionHash = "hash19",
            },
            new Provenance
            {
                Id = 20,
                AssetId = 10,
                Event = Event.Sale,
                FromId = "000e1111-e89b-12d3-a456-426614174009",
                ToId = "111e2222-e89b-12d3-a456-426614174010",
                Price = 10.0,
                TransactionHash = "hash20",
            },
            new Provenance
            {
                Id = 21,
                AssetId = 11,
                Event = Event.Mint,
                FromId = "111e2222-e89b-12d3-a456-426614174010",
                ToId = "111e2222-e89b-12d3-a456-426614174010",
                Price = 0.0,
                TransactionHash = "hash21",
            },
            new Provenance
            {
                Id = 22,
                AssetId = 12,
                Event = Event.Mint,
                FromId = "222e3333-e89b-12d3-a456-426614174011",
                ToId = "222e3333-e89b-12d3-a456-426614174011",
                Price = 0.0,
                TransactionHash = "hash22",
            },
            new Provenance
            {
                Id = 23,
                AssetId = 13,
                Event = Event.Mint,
                FromId = "333e4444-e89b-12d3-a456-426614174012",
                ToId = "333e4444-e89b-12d3-a456-426614174012",
                Price = 0.0,
                TransactionHash = "hash23",
            },
            new Provenance
            {
                Id = 24,
                AssetId = 14,
                Event = Event.Mint,
                FromId = "444e5555-e89b-12d3-a456-426614174013",
                ToId = "444e5555-e89b-12d3-a456-426614174013",
                Price = 0.0,
                TransactionHash = "hash24",
            },
            new Provenance
            {
                Id = 25,
                AssetId = 15,
                Event = Event.Mint,
                FromId = "555e6666-e89b-12d3-a456-426614174014",
                ToId = "555e6666-e89b-12d3-a456-426614174014",
                Price = 0.0,
                TransactionHash = "hash25",
            },
            new Provenance
            {
                Id = 26,
                AssetId = 11,
                Event = Event.Sale,
                FromId = "111e2222-e89b-12d3-a456-426614174010",
                ToId = "222e3333-e89b-12d3-a456-426614174011",
                Price = 11.0,
                TransactionHash = "hash26",
            },
            new Provenance
            {
                Id = 27,
                AssetId = 12,
                Event = Event.Sale,
                FromId = "222e3333-e89b-12d3-a456-426614174011",
                ToId = "333e4444-e89b-12d3-a456-426614174012",
                Price = 12.0,
                TransactionHash = "hash27",
            },
            new Provenance
            {
                Id = 28,
                AssetId = 13,
                Event = Event.Sale,
                FromId = "333e4444-e89b-12d3-a456-426614174012",
                ToId = "444e5555-e89b-12d3-a456-426614174013",
                Price = 13.0,
                TransactionHash = "hash28",
            },
            new Provenance
            {
                Id = 29,
                AssetId = 14,
                Event = Event.Sale,
                FromId = "444e5555-e89b-12d3-a456-426614174013",
                ToId = "555e6666-e89b-12d3-a456-426614174014",
                Price = 14.0,
                TransactionHash = "hash29",
            },
            new Provenance
            {
                Id = 30,
                AssetId = 15,
                Event = Event.Sale,
                FromId = "555e6666-e89b-12d3-a456-426614174014",
                ToId = "666e7777-e89b-12d3-a456-426614174005",
                Price = 15.0,
                TransactionHash = "hash30",
            },
            new Provenance
            {
                Id = 31,
                AssetId = 16,
                Event = Event.Mint,
                FromId = "666e7777-e89b-12d3-a456-426614174005",
                ToId = "666e7777-e89b-12d3-a456-426614174005",
                Price = 0.0,
                TransactionHash = "hash31",
            },
            new Provenance
            {
                Id = 32,
                AssetId = 17,
                Event = Event.Mint,
                FromId = "777e8888-e89b-12d3-a456-426614174006",
                ToId = "777e8888-e89b-12d3-a456-426614174006",
                Price = 0.0,
                TransactionHash = "hash32",
            },
            new Provenance
            {
                Id = 33,
                AssetId = 18,
                Event = Event.Mint,
                FromId = "888e9999-e89b-12d3-a456-426614174007",
                ToId = "888e9999-e89b-12d3-a456-426614174007",
                Price = 0.0,
                TransactionHash = "hash33",
            },
            new Provenance
            {
                Id = 34,
                AssetId = 19,
                Event = Event.Mint,
                FromId = "999e0000-e89b-12d3-a456-426614174008",
                ToId = "999e0000-e89b-12d3-a456-426614174008",
                Price = 0.0,
                TransactionHash = "hash34",
            },
            new Provenance
            {
                Id = 35,
                AssetId = 20,
                Event = Event.Mint,
                FromId = "000e1111-e89b-12d3-a456-426614174009",
                ToId = "000e1111-e89b-12d3-a456-426614174009",
                Price = 0.0,
                TransactionHash = "hash35",
            },
            new Provenance
            {
                Id = 36,
                AssetId = 16,
                Event = Event.Sale,
                FromId = "666e7777-e89b-12d3-a456-426614174005",
                ToId = "777e8888-e89b-12d3-a456-426614174006",
                Price = 16.0,
                TransactionHash = "hash36",
            },
            new Provenance
            {
                Id = 37,
                AssetId = 17,
                Event = Event.Sale,
                FromId = "777e8888-e89b-12d3-a456-426614174006",
                ToId = "888e9999-e89b-12d3-a456-426614174007",
                Price = 17.0,
                TransactionHash = "hash37",
            },
            new Provenance
            {
                Id = 38,
                AssetId = 18,
                Event = Event.Sale,
                FromId = "888e9999-e89b-12d3-a456-426614174007",
                ToId = "999e0000-e89b-12d3-a456-426614174008",
                Price = 18.0,
                TransactionHash = "hash38",
            },
            new Provenance
            {
                Id = 39,
                AssetId = 19,
                Event = Event.Sale,
                FromId = "999e0000-e89b-12d3-a456-426614174008",
                ToId = "000e1111-e89b-12d3-a456-426614174009",
                Price = 19.0,
                TransactionHash = "hash39",
            },
            new Provenance
            {
                Id = 40,
                AssetId = 20,
                Event = Event.Sale,
                FromId = "000e1111-e89b-12d3-a456-426614174009",
                ToId = "111e2222-e89b-12d3-a456-426614174010",
                Price = 20.0,
                TransactionHash = "hash40",
            },
            new Provenance
            {
                Id = 41,
                AssetId = 21,
                Event = Event.Mint,
                FromId = "111e2222-e89b-12d3-a456-426614174010",
                ToId = "111e2222-e89b-12d3-a456-426614174010",
                Price = 0.0,
                TransactionHash = "hash41",
            },
            new Provenance
            {
                Id = 42,
                AssetId = 22,
                Event = Event.Mint,
                FromId = "222e3333-e89b-12d3-a456-426614174011",
                ToId = "222e3333-e89b-12d3-a456-426614174011",
                Price = 0.0,
                TransactionHash = "hash42",
            },
            new Provenance
            {
                Id = 43,
                AssetId = 23,
                Event = Event.Mint,
                FromId = "333e4444-e89b-12d3-a456-426614174012",
                ToId = "333e4444-e89b-12d3-a456-426614174012",
                Price = 0.0,
                TransactionHash = "hash43",
            },
            new Provenance
            {
                Id = 44,
                AssetId = 24,
                Event = Event.Mint,
                FromId = "444e5555-e89b-12d3-a456-426614174013",
                ToId = "444e5555-e89b-12d3-a456-426614174013",
                Price = 0.0,
                TransactionHash = "hash44",
            },
            new Provenance
            {
                Id = 45,
                AssetId = 25,
                Event = Event.Mint,
                FromId = "555e6666-e89b-12d3-a456-426614174014",
                ToId = "555e6666-e89b-12d3-a456-426614174014",
                Price = 0.0,
                TransactionHash = "hash45",
            },
            new Provenance
            {
                Id = 46,
                AssetId = 21,
                Event = Event.Sale,
                FromId = "111e2222-e89b-12d3-a456-426614174010",
                ToId = "222e3333-e89b-12d3-a456-426614174011",
                Price = 21.0,
                TransactionHash = "hash46",
            },
            new Provenance
            {
                Id = 47,
                AssetId = 22,
                Event = Event.Sale,
                FromId = "222e3333-e89b-12d3-a456-426614174011",
                ToId = "333e4444-e89b-12d3-a456-426614174012",
                Price = 22.0,
                TransactionHash = "hash47",
            },
            new Provenance
            {
                Id = 48,
                AssetId = 23,
                Event = Event.Sale,
                FromId = "333e4444-e89b-12d3-a456-426614174012",
                ToId = "444e5555-e89b-12d3-a456-426614174013",
                Price = 23.0,
                TransactionHash = "hash48",
            },
            new Provenance
            {
                Id = 49,
                AssetId = 24,
                Event = Event.Sale,
                FromId = "444e5555-e89b-12d3-a456-426614174013",
                ToId = "555e6666-e89b-12d3-a456-426614174014",
                Price = 24.0,
                TransactionHash = "hash49",
            },
            new Provenance
            {
                Id = 50,
                AssetId = 25,
                Event = Event.Sale,
                FromId = "555e6666-e89b-12d3-a456-426614174014",
                ToId = "666e7777-e89b-12d3-a456-426614174005",
                Price = 25.0,
                TransactionHash = "hash50",
            },
            new Provenance
            {
                Id = 51,
                AssetId = 26,
                Event = Event.Mint,
                FromId = "666e7777-e89b-12d3-a456-426614174005",
                ToId = "666e7777-e89b-12d3-a456-426614174005",
                Price = 0.0,
                TransactionHash = "hash51",
            },
            new Provenance
            {
                Id = 52,
                AssetId = 27,
                Event = Event.Mint,
                FromId = "777e8888-e89b-12d3-a456-426614174006",
                ToId = "777e8888-e89b-12d3-a456-426614174006",
                Price = 0.0,
                TransactionHash = "hash52",
            },
            new Provenance
            {
                Id = 53,
                AssetId = 28,
                Event = Event.Mint,
                FromId = "888e9999-e89b-12d3-a456-426614174007",
                ToId = "888e9999-e89b-12d3-a456-426614174007",
                Price = 0.0,
                TransactionHash = "hash53",
            },
            new Provenance
            {
                Id = 54,
                AssetId = 29,
                Event = Event.Mint,
                FromId = "999e0000-e89b-12d3-a456-426614174008",
                ToId = "999e0000-e89b-12d3-a456-426614174008",
                Price = 0.0,
                TransactionHash = "hash54",
            }
        );		
    }	
}
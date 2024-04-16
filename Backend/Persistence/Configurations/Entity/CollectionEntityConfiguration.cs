using Domain.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class CollectionEntityConfiguration : IEntityTypeConfiguration<Collection>
{
	public void Configure(EntityTypeBuilder<Collection> builder)
	{
        builder.HasData(
            new Collection
            {
                Id = 1,
                Name = "Art by John Doe",
                Description = "A collection of art pieces by John Doe",
                Background = "https://example.com/background1.jpg",
                Avatar = "https://example.com/avatar1.jpg",
                Category = "art",
                CreatorId = "123e4567-e89b-12d3-a456-426614174000",
                Volume = 1000.0,
                Items = 10,
                FloorPrice = 1.0,
                LatestPrice = 1.5
            },
            new Collection
            {
                Id = 2,
                Name = "Photography by Jane Doe",
                Description = "A collection of photography pieces by Jane Doe",
                Background = "https://example.com/background2.jpg",
                Avatar = "https://example.com/avatar2.jpg",
                Category = "photography",
                CreatorId = "111e2222-e89b-12d3-a456-426614174001",
                Volume = 2000.0,
                Items = 20,
                FloorPrice = 2.0,
                LatestPrice = 2.5
            },
            new Collection
            {
                Id = 3,
                Name = "Designs by Bob Smith",
                Description = "A collection of design pieces by Bob Smith",
                Background = "https://example.com/background3.jpg",
                Avatar = "https://example.com/avatar3.jpg",
                Category = "design",
                CreatorId = "333e4444-e89b-12d3-a456-426614174002",
                Volume = 3000.0,
                Items = 30,
                FloorPrice = 3.0,
                LatestPrice = 3.5
            },
            new Collection
            {
                Id = 4,
                Name = "Music by Alice Jones",
                Description = "A collection of music pieces by Alice Jones",
                Background = "https://example.com/background4.jpg",
                Avatar = "https://example.com/avatar4.jpg",
                Category = "audio",
                CreatorId = "444e5555-e89b-12d3-a456-426614174003",
                Volume = 4000.0,
                Items = 40,
                FloorPrice = 4.0,
                LatestPrice = 4.5
            },
            new Collection
            {
                Id = 5,
                Name = "Videos by Charlie Brown",
                Description = "A collection of video pieces by Charlie Brown",
                Background = "https://example.com/background5.jpg",
                Avatar = "https://example.com/avatar5.jpg",
                Category = "video",
                CreatorId = "555e6666-e89b-12d3-a456-426614174004",
                Volume = 5000.0,
                Items = 50,
                FloorPrice = 5.0,
                LatestPrice = 5.5
            },
            new Collection
            {
                Id = 6,
                Name = "3D Art by David Davis",
                Description = "A collection of 3D art pieces by David Davis",
                Background = "https://example.com/background6.jpg",
                Avatar = "https://example.com/avatar6.jpg",
                Category = "three_d",
                CreatorId = "666e7777-e89b-12d3-a456-426614174005",
                Volume = 6000.0,
                Items = 60,
                FloorPrice = 6.0,
                LatestPrice = 6.5
            },
            new Collection
            {
                Id = 7,
                Name = "eBooks by Evelyn Green",
                Description = "A collection of eBooks by Evelyn Green",
                Background = "https://example.com/background7.jpg",
                Avatar = "https://example.com/avatar7.jpg",
                Category = "ebook",
                CreatorId = "777e8888-e89b-12d3-a456-426614174006",
                Volume = 7000.0,
                Items = 70,
                FloorPrice = 7.0,
                LatestPrice = 7.5
            },
            new Collection
            {
                Id = 8,
                Name = "Tickets by Frank Franklin",
                Description = "A collection of event tickets by Frank Franklin",
                Background = "https://example.com/background8.jpg",
                Avatar = "https://example.com/avatar8.jpg",
                Category = "ticket",
                CreatorId = "888e9999-e89b-12d3-a456-426614174007",
                Volume = 8000.0,
                Items = 80,
                FloorPrice = 8.0,
                LatestPrice = 8.5
            },
            new Collection
            {
                Id = 9,
                Name = "Art by Grace Gray",
                Description = "A collection of art pieces by Grace Gray",
                Background = "https://example.com/background9.jpg",
                Avatar = "https://example.com/avatar9.jpg",
                Category = "art",
                CreatorId = "999e0000-e89b-12d3-a456-426614174008",
                Volume = 9000.0,
                Items = 90,
                FloorPrice = 9.0,
                LatestPrice = 9.5
            },
            new Collection
            {
                Id = 10,
                Name = "Photography by Henry Hall",
                Description = "A collection of photography pieces by Henry Hall",
                Background = "https://example.com/background10.jpg",
                Avatar = "https://example.com/avatar10.jpg",
                Category = "photography",
                CreatorId = "000e1111-e89b-12d3-a456-426614174009",
                Volume = 10000.0,
                Items = 100,
                FloorPrice = 10.0,
                LatestPrice = 10.5
            },
            new Collection
            {
                Id = 11,
                Name = "Designs by Iggy Iglesias",
                Description = "A collection of design pieces by Iggy Iglesias",
                Background = "https://example.com/background11.jpg",
                Avatar = "https://example.com/avatar11.jpg",
                Category = "design",
                CreatorId = "111e2222-e89b-12d3-a456-426614174010",
                Volume = 11000.0,
                Items = 110,
                FloorPrice = 11.0,
                LatestPrice = 11.5
            },
            new Collection
            {
                Id = 12,
                Name = "Music by Jessie James",
                Description = "A collection of music pieces by Jessie James",
                Background = "https://example.com/background12.jpg",
                Avatar = "https://example.com/avatar12.jpg",
                Category = "audio",
                CreatorId = "222e3333-e89b-12d3-a456-426614174011",
                Volume = 12000.0,
                Items = 120,
                FloorPrice = 12.0,
                LatestPrice = 12.5
            },
            new Collection
            {
                Id = 13,
                Name = "Videos by Karl Kennedy",
                Description = "A collection of video pieces by Karl Kennedy",
                Background = "https://example.com/background13.jpg",
                Avatar = "https://example.com/avatar13.jpg",
                Category = "video",
                CreatorId = "333e4444-e89b-12d3-a456-426614174012",
                Volume = 13000.0,
                Items = 130,
                FloorPrice = 13.0,
                LatestPrice = 13.5
            },
            new Collection
            {
                Id = 14,
                Name = "3D Art by Lucy Lu",
                Description = "A collection of 3D art pieces by Lucy Lu",
                Background = "https://example.com/background14.jpg",
                Avatar = "https://example.com/avatar14.jpg",
                Category = "three_d",
                CreatorId = "444e5555-e89b-12d3-a456-426614174013",
                Volume = 14000.0,
                Items = 140,
                FloorPrice = 14.0,
                LatestPrice = 14.5
            },
            new Collection
            {
                Id = 15,
                Name = "eBooks by Mike Miller",
                Description = "A collection of eBooks by Mike Miller",
                Background = "https://example.com/background15.jpg",
                Avatar = "https://example.com/avatar15.jpg",
                Category = "ebook",
                CreatorId = "555e6666-e89b-12d3-a456-426614174014",
                Volume = 15000.0,
                Items = 150,
                FloorPrice = 15.0,
                LatestPrice = 15.5
            }
        );		
    }	
}
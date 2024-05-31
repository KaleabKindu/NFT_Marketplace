using Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{

    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(x => new { x.UserId, x.AssetId });

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Asset)
            .WithMany()
            .HasForeignKey(x => x.AssetId);

        // builder.HasData(
        //     // Asset 1 Likes
        //     new Like { Id = 1, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 1 },
        //     new Like { Id = 2, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 1 },
        //     new Like { Id = 3, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 1 },
        //     new Like { Id = 4, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 1 },
        //     new Like { Id = 5, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 1 },
        //     new Like { Id = 6, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 1 },
        //     new Like { Id = 7, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 1 },
        //     new Like { Id = 8, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 1 },
        //     new Like { Id = 9, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 1 },
        //     new Like { Id = 10, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 1 },

        //     // Asset 2 Likes
        //     new Like { Id = 11, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 2 },
        //     new Like { Id = 12, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 2 },
        //     new Like { Id = 13, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 2 },
        //     new Like { Id = 14, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 2 },
        //     new Like { Id = 15, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 2 },
        //     new Like { Id = 16, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 2 },
        //     new Like { Id = 17, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 2 },
        //     new Like { Id = 18, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 2 },
        //     new Like { Id = 19, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 2 },
        //     new Like { Id = 20, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 2 },

        //     // Asset 3 Likes
        //     new Like { Id = 21, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 3 },
        //     new Like { Id = 22, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 3 },
        //     new Like { Id = 23, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 3 },
        //     new Like { Id = 24, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 3 },
        //     new Like { Id = 25, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 3 },
        //     new Like { Id = 26, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 3 },
        //     new Like { Id = 27, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 3 },
        //     new Like { Id = 28, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 3 },
        //     new Like { Id = 29, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 3 },
        //     new Like { Id = 30, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 3 },

        //     // Asset 4 Likes
        //     new Like { Id = 31, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 4 },
        //     new Like { Id = 32, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 4 },
        //     new Like { Id = 33, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 4 },
        //     new Like { Id = 34, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 4 },
        //     new Like { Id = 35, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 4 },
        //     new Like { Id = 36, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 4 },
        //     new Like { Id = 37, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 4 },
        //     new Like { Id = 38, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 4 },
        //     new Like { Id = 39, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 4 },
        //     new Like { Id = 40, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 4 },

        //     // Asset 5 Likes
        //     new Like { Id = 41, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 5 },
        //     new Like { Id = 42, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 5 },
        //     new Like { Id = 43, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 5 },
        //     new Like { Id = 44, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 5 },
        //     new Like { Id = 45, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 5 },
        //     new Like { Id = 46, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 5 },
        //     new Like { Id = 47, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 5 },
        //     new Like { Id = 48, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 5 },
        //     new Like { Id = 49, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 5 },
        //     new Like { Id = 50, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 5 },

        //     // Asset 6 Likes
        //     new Like { Id = 51, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 6 },
        //     new Like { Id = 52, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 6 },
        //     new Like { Id = 53, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 6 },
        //     new Like { Id = 54, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 6 },
        //     new Like { Id = 55, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 6 },
        //     new Like { Id = 56, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 6 },
        //     new Like { Id = 57, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 6 },
        //     new Like { Id = 58, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 6 },
        //     new Like { Id = 59, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 6 },
        //     new Like { Id = 60, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 6 },

        //     // Asset 7 Likes
        //     new Like { Id = 61, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 7 },
        //     new Like { Id = 62, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 7 },
        //     new Like { Id = 63, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 7 },
        //     new Like { Id = 64, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 7 },
        //     new Like { Id = 65, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 7 },
        //     new Like { Id = 66, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 7 },
        //     new Like { Id = 67, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 7 },
        //     new Like { Id = 68, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 7 },
        //     new Like { Id = 69, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 7 },
        //     new Like { Id = 70, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 7 },

        //     // Asset 8 Likes
        //     new Like { Id = 71, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 8 },
        //     new Like { Id = 72, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 8 },
        //     new Like { Id = 73, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 8 },
        //     new Like { Id = 74, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 8 },
        //     new Like { Id = 75, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 8 },
        //     new Like { Id = 76, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 8 },
        //     new Like { Id = 77, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 8 },
        //     new Like { Id = 78, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 8 },
        //     new Like { Id = 79, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 8 },
        //     new Like { Id = 80, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 8 },

        //     // Asset 9 Likes
        //     new Like { Id = 81, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 9 },
        //     new Like { Id = 82, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 9 },
        //     new Like { Id = 83, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 9 },
        //     new Like { Id = 84, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 9 },
        //     new Like { Id = 85, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 9 },
        //     new Like { Id = 86, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 9 },
        //     new Like { Id = 87, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 9 },
        //     new Like { Id = 88, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 9 },
        //     new Like { Id = 89, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 9 },
        //     new Like { Id = 90, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 9 },

        //     // Asset 10 Likes
        //     new Like { Id = 91, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 10 },
        //     new Like { Id = 92, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 10 },
        //     new Like { Id = 93, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 10 },
        //     new Like { Id = 94, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 10 },
        //     new Like { Id = 95, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 10 },
        //     new Like { Id = 96, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 10 },
        //     new Like { Id = 97, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 10 },
        //     new Like { Id = 98, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 10 },
        //     new Like { Id = 99, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 10 },
        //     new Like { Id = 100, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 10 },

        //     // Asset 11 Likes
        //     new Like { Id = 101, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 11 },
        //     new Like { Id = 102, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 11 },
        //     new Like { Id = 103, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 11 },
        //     new Like { Id = 104, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 11 },
        //     new Like { Id = 105, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 11 },
        //     new Like { Id = 106, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 11 },

        //     // Asset 12 Likes
        //     new Like { Id = 107, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 12 },
        //     new Like { Id = 108, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 12 },
        //     new Like { Id = 109, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 12 },
        //     new Like { Id = 110, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 12 },
        //     new Like { Id = 111, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 12 },
        //     new Like { Id = 112, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 12 },
        //     new Like { Id = 113, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 12 },
        //     new Like { Id = 114, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 12 },

        //     // Asset 13 Likes
        //     new Like { Id = 115, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 13 },
        //     new Like { Id = 116, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 13 },
        //     new Like { Id = 117, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 13 },
        //     new Like { Id = 118, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 13 },
        //     new Like { Id = 119, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 13 },
        //     new Like { Id = 120, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 13 },
        //     new Like { Id = 121, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 13 },

        //     // Asset 14 Likes
        //     new Like { Id = 122, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 14 },
        //     new Like { Id = 123, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 14 },
        //     new Like { Id = 124, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 14 },
        //     new Like { Id = 125, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 14 },
        //     new Like { Id = 126, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 14 },
        //     new Like { Id = 127, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 14 },
        //     new Like { Id = 128, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 14 },
        //     new Like { Id = 129, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 14 },
        //     new Like { Id = 130, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 14 },
        //     new Like { Id = 131, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 14 },

        //     // Asset 15 Likes
        //     new Like { Id = 131, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 15 },
        //     new Like { Id = 132, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 15 },
        //     new Like { Id = 133, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 15 },
        //     new Like { Id = 134, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 15 },
        //     new Like { Id = 135, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 15 },
        //     new Like { Id = 136, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 15 },
        //     new Like { Id = 137, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 15 },
        //     new Like { Id = 138, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 15 },
        //     new Like { Id = 139, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 15 },
        //     new Like { Id = 140, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 15 },

        //     // Asset 16 Likes
        //     new Like { Id = 141, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 16 },
        //     new Like { Id = 142, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 16 },
        //     new Like { Id = 143, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 16 },
        //     new Like { Id = 144, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 16 },
        //     new Like { Id = 145, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 16 },
        //     new Like { Id = 146, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 16 },
        //     new Like { Id = 147, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 16 },
        //     new Like { Id = 148, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 16 },
        //     new Like { Id = 149, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 16 },
        //     new Like { Id = 150, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 16 },

        //     // Asset 17 Likes
        //     new Like { Id = 151, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 17 },
        //     new Like { Id = 152, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 17 },
        //     new Like { Id = 153, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 17 },
        //     new Like { Id = 154, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 17 },
        //     new Like { Id = 155, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 17 },
        //     new Like { Id = 156, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 17 },
        //     new Like { Id = 157, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 17 },
        //     new Like { Id = 158, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 17 },
        //     new Like { Id = 159, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 17 },
        //     new Like { Id = 160, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 17 },

        //     // Asset 18 Likes
        //     new Like { Id = 161, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 18 },
        //     new Like { Id = 162, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 18 },
        //     new Like { Id = 163, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 18 },
        //     new Like { Id = 164, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 18 },
        //     new Like { Id = 165, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 18 },
        //     new Like { Id = 166, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 18 },
        //     new Like { Id = 167, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 18 },
        //     new Like { Id = 168, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 18 },
        //     new Like { Id = 169, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 18 },
        //     new Like { Id = 170, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 18 },

        //     // Asset 19 Likes
        //     new Like { Id = 171, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 19 },
        //     new Like { Id = 172, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 19 },
        //     new Like { Id = 173, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 19 },
        //     new Like { Id = 174, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 19 },
        //     new Like { Id = 175, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 19 },
        //     new Like { Id = 176, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 19 },

        //     // Asset 20 Likes
        //     new Like { Id = 177, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 20 },
        //     new Like { Id = 178, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 20 },
        //     new Like { Id = 179, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 20 },
        //     new Like { Id = 180, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 20 },
        //     new Like { Id = 181, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 20 },
        //     new Like { Id = 182, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 20 },
        //     new Like { Id = 183, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 20 }, 

        //     // Asset 21 Likes
        //     new Like { Id = 184, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 21 },
        //     new Like { Id = 185, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 21 },
        //     new Like { Id = 186, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 21 },
        //     new Like { Id = 187, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 21 },
        //     new Like { Id = 188, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 21 },
        //     new Like { Id = 189, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 21 },
        //     new Like { Id = 190, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 21 },
        //     new Like { Id = 191, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 21 },
        //     new Like { Id = 192, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 21 },
        //     new Like { Id = 193, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 21 },

        //     // Asset 22 Likes
        //     new Like { Id = 194, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 22 },
        //     new Like { Id = 195, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 22 },
        //     new Like { Id = 196, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 22 },
        //     new Like { Id = 197, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 22 },
        //     new Like { Id = 198, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 22 },
        //     new Like { Id = 200, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 22 },
        //     new Like { Id = 201, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 22 },
        //     new Like { Id = 202, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 22 },

        //     // Asset 23 Likes
        //     new Like { Id = 203, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 23 },
        //     new Like { Id = 204, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 23 },
        //     new Like { Id = 205, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 23 },
        //     new Like { Id = 206, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 23 },
        //     new Like { Id = 207, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 23 },
        //     new Like { Id = 208, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 23 },
        //     new Like { Id = 209, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 23 },

        //     // Asset 24 Likes
        //     new Like { Id = 210, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 24 },
        //     new Like { Id = 211, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 24 },
        //     new Like { Id = 212, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 24 },
        //     new Like { Id = 213, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 24 },
        //     new Like { Id = 214, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 24 },

        //     // Asset 25 Likes
        //     new Like { Id = 215, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 25 },
        //     new Like { Id = 216, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 25 },
        //     new Like { Id = 217, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 25 },
        //     new Like { Id = 218, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 25 },
        //     new Like { Id = 219, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 25 },
        //     new Like { Id = 220, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 25 },
        //     new Like { Id = 221, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 25 },

        //     // Asset 26 Likes
        //     new Like { Id = 222, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 26 },
        //     new Like { Id = 223, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 26 },
        //     new Like { Id = 224, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 26 },
        //     new Like { Id = 225, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 26 },
        //     new Like { Id = 226, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 26 },
        //     new Like { Id = 227, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 26 },
        //     new Like { Id = 228, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 26 },
        //     new Like { Id = 229, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 26 },
        //     new Like { Id = 230, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 26 },
        //     new Like { Id = 231, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 26 },

        //     // Asset 27 Likes
        //     new Like { Id = 232, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 27 },
        //     new Like { Id = 233, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 27 },
        //     new Like { Id = 234, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 27 },
        //     new Like { Id = 235, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 27 },
        //     new Like { Id = 236, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 27 },
        //     new Like { Id = 237, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 27 },
        //     new Like { Id = 238, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 27 },
        //     new Like { Id = 239, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 27 },
        //     new Like { Id = 240, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 27 },
        //     new Like { Id = 241, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 27 },

        //     // Asset 28 Likes
        //     new Like { Id = 242, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 28 },
        //     new Like { Id = 243, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 28 },
        //     new Like { Id = 244, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 28 },
        //     new Like { Id = 245, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 28 },
        //     new Like { Id = 246, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 28 },
        //     new Like { Id = 247, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 28 },
        //     new Like { Id = 248, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 28 },

        //     // Asset 29 Likes
        //     new Like { Id = 249, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 29 },
        //     new Like { Id = 250, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 29 },
        //     new Like { Id = 251, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 29 },
        //     new Like { Id = 252, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 29 },
        //     new Like { Id = 253, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 29 },
        //     new Like { Id = 254, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 29 },
        //     new Like { Id = 255, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 29 },
        //     new Like { Id = 256, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 29 },
        //     new Like { Id = 257, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 29 },
        //     new Like { Id = 258, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 29 },   

        //     // Asset 30 Likes
        //     new Like { Id = 259, UserId = "123e4567-e89b-12d3-a456-426614174000", AssetId = 30 },
        //     new Like { Id = 260, UserId = "111e2222-e89b-12d3-a456-426614174001", AssetId = 30 },
        //     new Like { Id = 261, UserId = "333e4444-e89b-12d3-a456-426614174002", AssetId = 30 },
        //     new Like { Id = 262, UserId = "444e5555-e89b-12d3-a456-426614174003", AssetId = 30 },
        //     new Like { Id = 263, UserId = "555e6666-e89b-12d3-a456-426614174004", AssetId = 30 },
        //     new Like { Id = 264, UserId = "666e7777-e89b-12d3-a456-426614174005", AssetId = 30 },
        //     new Like { Id = 265, UserId = "777e8888-e89b-12d3-a456-426614174006", AssetId = 30 },
        //     new Like { Id = 266, UserId = "888e9999-e89b-12d3-a456-426614174007", AssetId = 30 },
        //     new Like { Id = 267, UserId = "999e0000-e89b-12d3-a456-426614174008", AssetId = 30 },
        //     new Like { Id = 268, UserId = "000e1111-e89b-12d3-a456-426614174009", AssetId = 30 }          
        // );
    }
}
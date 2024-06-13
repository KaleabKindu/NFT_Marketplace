using Bogus;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    private static readonly Faker _faker = new();

    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        var admin = new UserProfile
        {
            Id = 3,
            UserId = "123e4567-e89b-12d3-a456-426614174000",
            UserName = _faker.Internet.UserName(),
        };

        builder.HasData(
            admin,
            new UserProfile
            {
                Id = 4,
                UserId = "111e2222-e89b-12d3-a456-426614174001",
                UserName = _faker.Internet.UserName(),
                Email = "janedoe@example.com",
                Avatar = "https://example.com/avatar2.jpg",
                Bio = "I am Jane Doe, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background2.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/janedoe",
                Twitter = "https://twitter.com/janedoe",
                YouTube = "https://youtube.com/janedoe",
                Telegram = "https://telegram.com/janedoe"
            },
            new UserProfile
            {
                Id = 5,
                UserId = "333e4444-e89b-12d3-a456-426614174002",
                UserName = _faker.Internet.UserName(),
                Email = "bobsmith@example.com",
                Avatar = "https://example.com/avatar3.jpg",
                Bio = "I am Bob Smith, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background3.jpg",
                TotalSalesCount = 20,
                Facebook = "https://facebook.com/bobsmith",
                Twitter = "https://twitter.com/bobsmith",
                YouTube = "https://youtube.com/bobsmith",
                Telegram = "https://telegram.com/bobsmith"
            },
            new UserProfile
            {
                Id = 6,
                UserId = "444e5555-e89b-12d3-a456-426614174003",
                UserName = _faker.Internet.UserName(),
                Email = "alicejones@example.com",
                Avatar = "https://example.com/avatar4.jpg",
                Bio = "I am Alice Jones, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background4.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/alicejones",
                Twitter = "https://twitter.com/alicejones",
                YouTube = "https://youtube.com/alicejones",
                Telegram = "https://telegram.com/alicejones"
            },
            new UserProfile
            {
                Id = 7,
                UserId = "555e6666-e89b-12d3-a456-426614174004",
                UserName = _faker.Internet.UserName(),
                Email = "charliebrown@example.com",
                Avatar = "https://example.com/avatar5.jpg",
                Bio = "I am Charlie Brown, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background5.jpg",
                TotalSalesCount = 30,
                Facebook = "https://facebook.com/charliebrown",
                Twitter = "https://twitter.com/charliebrown",
                YouTube = "https://youtube.com/charliebrown",
                Telegram = "https://telegram.com/charliebrown"
            },
            new UserProfile
            {
                Id = 8,
                UserId = "666e7777-e89b-12d3-a456-426614174005",
                UserName = _faker.Internet.UserName(),
                Email = "daviddavis@example.com",
                Avatar = "https://example.com/avatar6.jpg",
                Bio = "I am David Davis, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background6.jpg",
                TotalSalesCount = 40,
                Facebook = "https://facebook.com/daviddavis",
                Twitter = "https://twitter.com/daviddavis",
                YouTube = "https://youtube.com/daviddavis",
                Telegram = "https://telegram.com/daviddavis"
            },
            new UserProfile
            {
                Id = 9,
                UserId = "777e8888-e89b-12d3-a456-426614174006",
                UserName = _faker.Internet.UserName(),
                Email = "evelyngreen@example.com",
                Avatar = "https://example.com/avatar7.jpg",
                Bio = "I am Evelyn Green, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background7.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/evelyngreen",
                Twitter = "https://twitter.com/evelyngreen",
                YouTube = "https://youtube.com/evelyngreen",
                Telegram = "https://telegram.com/evelyngreen"
            },
            new UserProfile
            {
                Id = 10,
                UserId = "888e9999-e89b-12d3-a456-426614174007",
                UserName = _faker.Internet.UserName(),
                Email = "frankfranklin@example.com",
                Avatar = "https://example.com/avatar8.jpg",
                Bio = "I am Frank Franklin, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background8.jpg",
                TotalSalesCount = 50,
                Facebook = "https://facebook.com/frankfranklin",
                Twitter = "https://twitter.com/frankfranklin",
                YouTube = "https://youtube.com/frankfranklin",
                Telegram = "https://telegram.com/frankfranklin"
            },
            new UserProfile
            {
                Id = 11,
                UserId = "999e0000-e89b-12d3-a456-426614174008",
                UserName = _faker.Internet.UserName(),
                Email = "gracegray@example.com",
                Avatar = "https://example.com/avatar9.jpg",
                Bio = "I am Grace Gray, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background9.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/gracegray",
                Twitter = "https://twitter.com/gracegray",
                YouTube = "https://youtube.com/gracegray",
                Telegram = "https://telegram.com/gracegray"
            },
            new UserProfile
            {
                Id = 12,
                UserId = "000e1111-e89b-12d3-a456-426614174009",
                UserName = _faker.Internet.UserName(),
                Email = "henryhall@example.com",
                Avatar = "https://example.com/avatar10.jpg",
                Bio = "I am Henry Hall, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background10.jpg",
                TotalSalesCount = 60,
                Facebook = "https://facebook.com/henryhall",
                Twitter = "https://twitter.com/henryhall",
                YouTube = "https://youtube.com/henryhall",
                Telegram = "https://telegram.com/henryhall"
            },
            new UserProfile
            {
                Id = 13,
                UserId = "111e2222-e89b-12d3-a456-426614174010",
                UserName = _faker.Internet.UserName(),
                Email = "iggyiglesias@example.com",
                Avatar = "https://example.com/avatar11.jpg",
                Bio = "I am Iggy Iglesias, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background11.jpg",
                TotalSalesCount = 70,
                Facebook = "https://facebook.com/iggyiglesias",
                Twitter = "https://twitter.com/iggyiglesias",
                YouTube = "https://youtube.com/iggyiglesias",
                Telegram = "https://telegram.com/iggyiglesias"
            },
            new UserProfile
            {
                Id = 14,
                UserId = "222e3333-e89b-12d3-a456-426614174011",
                UserName = _faker.Internet.UserName(),
                Email = "jessiejames@example.com",
                Avatar = "https://example.com/avatar12.jpg",
                Bio = "I am Jessie James, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background12.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/jessiejames",
                Twitter = "https://twitter.com/jessiejames",
                YouTube = "https://youtube.com/jessiejames",
                Telegram = "https://telegram.com/jessiejames"
            },
            new UserProfile
            {
                Id = 15,
                UserId = "333e4444-e89b-12d3-a456-426614174012",
                UserName = _faker.Internet.UserName(),
                Email = "karlkennedy@example.com",
                Avatar = "https://example.com/avatar13.jpg",
                Bio = "I am Karl Kennedy, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background13.jpg",
                TotalSalesCount = 80,
                Facebook = "https://facebook.com/karlkennedy",
                Twitter = "https://twitter.com/karlkennedy",
                YouTube = "https://youtube.com/karlkennedy",
                Telegram = "https://telegram.com/karlkennedy"
            },
            new UserProfile
            {
                Id = 16,
                UserId = "444e5555-e89b-12d3-a456-426614174013",
                UserName = _faker.Internet.UserName(),
                Email = "lucylu@example.com",
                Avatar = "https://example.com/avatar14.jpg",
                Bio = "I am Lucy Lu, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background14.jpg",
                TotalSalesCount = 0,
                Facebook = "https://facebook.com/lucylu",
                Twitter = "https://twitter.com/lucylu",
                YouTube = "https://youtube.com/lucylu",
                Telegram = "https://telegram.com/lucylu"
            },
            new UserProfile
            {
                Id = 17,
                UserId = "555e6666-e89b-12d3-a456-426614174014",
                UserName = _faker.Internet.UserName(),
                Email = "mikemiller@example.com",
                Avatar = "https://example.com/avatar15.jpg",
                Bio = "I am Mike Miller, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background15.jpg",
                TotalSalesCount = 90,
                Facebook = "https://facebook.com/mikemiller",
                Twitter = "https://twitter.com/mikemiller",
                YouTube = "https://youtube.com/mikemiller",
                Telegram = "https://telegram.com/mikemiller"
            }
        );
    }
}
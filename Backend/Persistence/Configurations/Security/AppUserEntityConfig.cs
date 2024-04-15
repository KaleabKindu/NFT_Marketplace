using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
	{
		var admin = new AppUser
		{
			Id ="6f09dad5-2268-4410-b755-cf7859927e5f",
			UserName="starlord",
			Address = "0x68d54e37D6221D1DA835489099D1b69Ce5c8b90d",
            Nonce="ee528136-2f06-41e6-b417-fdd116d36446"
		};

		var trader1 = new AppUser
		{
			Id ="6f09dad5-2268-4410-b755-cf7859927f6g",
			UserName="loki",
			Address = "0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
            Nonce="ff528136-2f06-41e6-b417-fdd116d36446"
		};

		builder.HasData(
			admin,
			trader1,
			new AppUser
            {
                Id = "123e4567-e89b-12d3-a456-426614174000",
                UserName = "johndoe@example.com",
                Email = "johndoe@example.com",
                Avatar = "https://example.com/avatar1.jpg",
                Bio = "I am John Doe, a digital artist.",
                Address = "0x1234567890123456789012345678901234567890",
                Nonce = "abcdefghijklmnopqrstuvwxyz",
                ProfileBackgroundImage = "https://example.com/background1.jpg",
                TotalSalesCount = 10,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/johndoe",
                Twitter = "https://twitter.com/johndoe",
                YouTube = "https://youtube.com/johndoe",
                Telegram = "https://telegram.com/johndoe"
            },


            new AppUser
            {
                Id = "111e2222-e89b-12d3-a456-426614174001",
                UserName = "janedoe@example.com",
                Email = "janedoe@example.com",
                Avatar = "https://example.com/avatar2.jpg",
                Bio = "I am Jane Doe, a digital collector.",
                Address = "0x2345678901234567890123456789012345678901",
                Nonce = "bcdefghijklmnopqrstuvwxyza",
                ProfileBackgroundImage = "https://example.com/background2.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/janedoe",
                Twitter = "https://twitter.com/janedoe",
                YouTube = "https://youtube.com/janedoe",
                Telegram = "https://telegram.com/janedoe"
            },
            new AppUser
            {
                Id = "333e4444-e89b-12d3-a456-426614174002",
                UserName = "bobsmith@example.com",
                Email = "bobsmith@example.com",
                Avatar = "https://example.com/avatar3.jpg",
                Bio = "I am Bob Smith, a digital artist.",
                Address = "0x3456789012345678901234567890123456789012",
                Nonce = "cdefghijklmnopqrstuvwxyzab",
                ProfileBackgroundImage = "https://example.com/background3.jpg",
                TotalSalesCount = 20,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/bobsmith",
                Twitter = "https://twitter.com/bobsmith",
                YouTube = "https://youtube.com/bobsmith",
                Telegram = "https://telegram.com/bobsmith"
            },
            new AppUser
            {
                Id = "444e5555-e89b-12d3-a456-426614174003",
                UserName = "alicejones@example.com",
                Email = "alicejones@example.com",
                Avatar = "https://example.com/avatar4.jpg",
                Bio = "I am Alice Jones, a digital collector.",
                Address = "0x4567890123456789012345678901234567890123",
                Nonce = "defghijklmnopqrstuvwxyzabc",
                ProfileBackgroundImage = "https://example.com/background4.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/alicejones",
                Twitter = "https://twitter.com/alicejones",
                YouTube = "https://youtube.com/alicejones",
                Telegram = "https://telegram.com/alicejones"
            },
            new AppUser
            {
                Id = "555e6666-e89b-12d3-a456-426614174004",
                UserName = "charliebrown@example.com",
                Email = "charliebrown@example.com",
                Avatar = "https://example.com/avatar5.jpg",
                Bio = "I am Charlie Brown, a digital artist.",
                Address = "0x5678901234567890123456789012345678901234",
                Nonce = "efghijklmnopqrstuvwxyzabcd",
                ProfileBackgroundImage = "https://example.com/background5.jpg",
                TotalSalesCount = 30,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/charliebrown",
                Twitter = "https://twitter.com/charliebrown",
                YouTube = "https://youtube.com/charliebrown",
                Telegram = "https://telegram.com/charliebrown"
            },
            new AppUser
            {
                Id = "666e7777-e89b-12d3-a456-426614174005",
                UserName = "daviddavis@example.com",
                Email = "daviddavis@example.com",
                Avatar = "https://example.com/avatar6.jpg",
                Bio = "I am David Davis, a digital artist.",
                Address = "0x6789012345678901234567890123456789012345",
                Nonce = "fghijklmnopqrstuvwxyzabcde",
                ProfileBackgroundImage = "https://example.com/background6.jpg",
                TotalSalesCount = 40,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/daviddavis",
                Twitter = "https://twitter.com/daviddavis",
                YouTube = "https://youtube.com/daviddavis",
                Telegram = "https://telegram.com/daviddavis"
            },
            new AppUser
            {
                Id = "777e8888-e89b-12d3-a456-426614174006",
                UserName = "evelyngreen@example.com",
                Email = "evelyngreen@example.com",
                Avatar = "https://example.com/avatar7.jpg",
                Bio = "I am Evelyn Green, a digital collector.",
                Address = "0x7890123456789012345678901234567890123456",
                Nonce = "ghijklmnopqrstuvwxyzabcdef",
                ProfileBackgroundImage = "https://example.com/background7.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/evelyngreen",
                Twitter = "https://twitter.com/evelyngreen",
                YouTube = "https://youtube.com/evelyngreen",
                Telegram = "https://telegram.com/evelyngreen"
            },
            new AppUser
            {
                Id = "888e9999-e89b-12d3-a456-426614174007",
                UserName = "frankfranklin@example.com",
                Email = "frankfranklin@example.com",
                Avatar = "https://example.com/avatar8.jpg",
                Bio = "I am Frank Franklin, a digital artist.",
                Address = "0x8901234567890123456789012345678901234567",
                Nonce = "hijklmnopqrstuvwxyzabcdefg",
                ProfileBackgroundImage = "https://example.com/background8.jpg",
                TotalSalesCount = 50,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/frankfranklin",
                Twitter = "https://twitter.com/frankfranklin",
                YouTube = "https://youtube.com/frankfranklin",
                Telegram = "https://telegram.com/frankfranklin"
            },
            new AppUser
            {
                Id = "999e0000-e89b-12d3-a456-426614174008",
                UserName = "gracegray@example.com",
                Email = "gracegray@example.com",
                Avatar = "https://example.com/avatar9.jpg",
                Bio = "I am Grace Gray, a digital collector.",
                Address = "0x9012345678901234567890123456789012345678",
                Nonce = "ijklmnopqrstuvwxyzabcdefgh",
                ProfileBackgroundImage = "https://example.com/background9.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/gracegray",
                Twitter = "https://twitter.com/gracegray",
                YouTube = "https://youtube.com/gracegray",
                Telegram = "https://telegram.com/gracegray"
            },
            new AppUser
            {
                Id = "000e1111-e89b-12d3-a456-426614174009",
                UserName = "henryhall@example.com",
                Email = "henryhall@example.com",
                Avatar = "https://example.com/avatar10.jpg",
                Bio = "I am Henry Hall, a digital artist.",
                Address = "0x0123456789012345678901234567890123456789",
                Nonce = "jklmnopqrstuvwxyzabcdefghi",
                ProfileBackgroundImage = "https://example.com/background10.jpg",
                TotalSalesCount = 60,
                Followers = new List<string> { 
					"0x2345678901234567890123456789012345678902",
					"0x3456789012345678901234567890123456789013",
					"0x4567890123456789012345678901234567890124",
					"0x5678901234567890123456789012345678901235"
				},
                Facebook = "https://facebook.com/henryhall",
                Twitter = "https://twitter.com/henryhall",
                YouTube = "https://youtube.com/henryhall",
                Telegram = "https://telegram.com/henryhall"
            },
            new AppUser
            {
                Id = "111e2222-e89b-12d3-a456-426614174010",
                UserName = "iggyiglesias@example.com",
                Email = "iggyiglesias@example.com",
                Avatar = "https://example.com/avatar11.jpg",
                Bio = "I am Iggy Iglesias, a digital artist.",
                Address = "0x1234567890123456789012345678901234567891",
                Nonce = "klmnopqrstuvwxyzabcdefghij",
                ProfileBackgroundImage = "https://example.com/background11.jpg",
                TotalSalesCount = 70,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x5678901234567890123456789012345678901234",
					"0x8901234567890123456789012345678901234567",
					"0x2345678901234567890123456789012345678902"
                },
                Facebook = "https://facebook.com/iggyiglesias",
                Twitter = "https://twitter.com/iggyiglesias",
                YouTube = "https://youtube.com/iggyiglesias",
                Telegram = "https://telegram.com/iggyiglesias"
            },
            new AppUser
            {
                Id = "222e3333-e89b-12d3-a456-426614174011",
                UserName = "jessiejames@example.com",
                Email = "jessiejames@example.com",
                Avatar = "https://example.com/avatar12.jpg",
                Bio = "I am Jessie James, a digital collector.",
                Address = "0x2345678901234567890123456789012345678902",
                Nonce = "lmnopqrstuvwxyzabcdefghijk",
                ProfileBackgroundImage = "https://example.com/background12.jpg",
                TotalSalesCount = 0,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x5678901234567890123456789012345678901234",
					"0x8901234567890123456789012345678901234567",
					"0x2345678901234567890123456789012345678902"
                },
                Facebook = "https://facebook.com/jessiejames",
                Twitter = "https://twitter.com/jessiejames",
                YouTube = "https://youtube.com/jessiejames",
                Telegram = "https://telegram.com/jessiejames"
            },
            new AppUser
            {
                Id = "333e4444-e89b-12d3-a456-426614174012",
                UserName = "karlkennedy@example.com",
                Email = "karlkennedy@example.com",
                Avatar = "https://example.com/avatar13.jpg",
                Bio = "I am Karl Kennedy, a digital artist.",
                Address = "0x3456789012345678901234567890123456789013",
                Nonce = "mnopqrstuvwxyzabcdefghijkl",
                ProfileBackgroundImage = "https://example.com/background13.jpg",
                TotalSalesCount = 80,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x1234567890123456789012345678901234567890",
					"0x2345678901234567890123456789012345678901",
					"0x3456789012345678901234567890123456789012"
                },
                Facebook = "https://facebook.com/karlkennedy",
                Twitter = "https://twitter.com/karlkennedy",
                YouTube = "https://youtube.com/karlkennedy",
                Telegram = "https://telegram.com/karlkennedy"
            },
            new AppUser
            {
                Id = "444e5555-e89b-12d3-a456-426614174013",
                UserName = "lucylu@example.com",
                Email = "lucylu@example.com",
                Avatar = "https://example.com/avatar14.jpg",
                Bio = "I am Lucy Lu, a digital collector.",
                Address = "0x4567890123456789012345678901234567890124",
                Nonce = "opqrstuvwxyzabcdefghijklm",
                ProfileBackgroundImage = "https://example.com/background14.jpg",
                TotalSalesCount = 0,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x1234567890123456789012345678901234567890",
					"0x2345678901234567890123456789012345678901",
					"0x3456789012345678901234567890123456789012"
                },
                Facebook = "https://facebook.com/lucylu",
                Twitter = "https://twitter.com/lucylu",
                YouTube = "https://youtube.com/lucylu",
                Telegram = "https://telegram.com/lucylu"
            },
            new AppUser
            {
                Id = "555e6666-e89b-12d3-a456-426614174014",
                UserName = "mikemiller@example.com",
                Email = "mikemiller@example.com",
                Avatar = "https://example.com/avatar15.jpg",
                Bio = "I am Mike Miller, a digital artist.",
                Address = "0x5678901234567890123456789012345678901235",
                Nonce = "pqrstuvwxyzabcdefghijklmn",
                ProfileBackgroundImage = "https://example.com/background15.jpg",
                TotalSalesCount = 90,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x1234567890123456789012345678901234567890",
					"0x2345678901234567890123456789012345678901",
					"0x3456789012345678901234567890123456789012"
                },
                Facebook = "https://facebook.com/mikemiller",
                Twitter = "https://twitter.com/mikemiller",
                YouTube = "https://youtube.com/mikemiller",
                Telegram = "https://telegram.com/mikemiller"
            }
		);		
    }	
}
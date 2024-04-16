using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
	public void Configure(EntityTypeBuilder<UserProfile> builder)
	{
		var admin = new UserProfile
		{
            Id = 1,
			UserId ="6f09dad5-2268-4410-b755-cf7859927e5f",
			UserName="starlord"
		};

		var trader1 = new UserProfile
		{
            Id = 2,
			UserId ="6f09dad5-2268-4410-b755-cf7859927f6g",
			UserName="loki"
		};

		builder.HasData(
			admin,
			trader1,
			new UserProfile
            {
                Id =  3,
                UserId = "123e4567-e89b-12d3-a456-426614174000",
                UserName = "johndoe@example.com",
                Email = "johndoe@example.com",
                Avatar = "https://example.com/avatar1.jpg",
                Bio = "I am John Doe, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background1.jpg",
                TotalSalesCount = 10,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/johndoe",
                Twitter = "https://twitter.com/johndoe",
                YouTube = "https://youtube.com/johndoe",
                Telegram = "https://telegram.com/johndoe"
            },
            new UserProfile
            {
                Id = 4,
                UserId = "111e2222-e89b-12d3-a456-426614174001",
                UserName = "janedoe@example.com",
                Email = "janedoe@example.com",
                Avatar = "https://example.com/avatar2.jpg",
                Bio = "I am Jane Doe, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background2.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/janedoe",
                Twitter = "https://twitter.com/janedoe",
                YouTube = "https://youtube.com/janedoe",
                Telegram = "https://telegram.com/janedoe"
            },
            new UserProfile
            {
                Id = 5,
                UserId = "333e4444-e89b-12d3-a456-426614174002",
                UserName = "bobsmith@example.com",
                Email = "bobsmith@example.com",
                Avatar = "https://example.com/avatar3.jpg",
                Bio = "I am Bob Smith, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background3.jpg",
                TotalSalesCount = 20,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/bobsmith",
                Twitter = "https://twitter.com/bobsmith",
                YouTube = "https://youtube.com/bobsmith",
                Telegram = "https://telegram.com/bobsmith"
            },
            new UserProfile
            {
                Id = 6,
                UserId = "444e5555-e89b-12d3-a456-426614174003",
                UserName = "alicejones@example.com",
                Email = "alicejones@example.com",
                Avatar = "https://example.com/avatar4.jpg",
                Bio = "I am Alice Jones, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background4.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/alicejones",
                Twitter = "https://twitter.com/alicejones",
                YouTube = "https://youtube.com/alicejones",
                Telegram = "https://telegram.com/alicejones"
            },
            new UserProfile
            {
                Id = 7,
                UserId = "555e6666-e89b-12d3-a456-426614174004",
                UserName = "charliebrown@example.com",
                Email = "charliebrown@example.com",
                Avatar = "https://example.com/avatar5.jpg",
                Bio = "I am Charlie Brown, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background5.jpg",
                TotalSalesCount = 30,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/charliebrown",
                Twitter = "https://twitter.com/charliebrown",
                YouTube = "https://youtube.com/charliebrown",
                Telegram = "https://telegram.com/charliebrown"
            },
            new UserProfile
            {
                Id = 8,
                UserId = "666e7777-e89b-12d3-a456-426614174005",
                UserName = "daviddavis@example.com",
                Email = "daviddavis@example.com",
                Avatar = "https://example.com/avatar6.jpg",
                Bio = "I am David Davis, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background6.jpg",
                TotalSalesCount = 40,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/daviddavis",
                Twitter = "https://twitter.com/daviddavis",
                YouTube = "https://youtube.com/daviddavis",
                Telegram = "https://telegram.com/daviddavis"
            },
            new UserProfile
            {
                Id = 9,
                UserId = "777e8888-e89b-12d3-a456-426614174006",
                UserName = "evelyngreen@example.com",
                Email = "evelyngreen@example.com",
                Avatar = "https://example.com/avatar7.jpg",
                Bio = "I am Evelyn Green, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background7.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/evelyngreen",
                Twitter = "https://twitter.com/evelyngreen",
                YouTube = "https://youtube.com/evelyngreen",
                Telegram = "https://telegram.com/evelyngreen"
            },
            new UserProfile
            {
                Id =  10,
                UserId = "888e9999-e89b-12d3-a456-426614174007",
                UserName = "frankfranklin@example.com",
                Email = "frankfranklin@example.com",
                Avatar = "https://example.com/avatar8.jpg",
                Bio = "I am Frank Franklin, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background8.jpg",
                TotalSalesCount = 50,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/frankfranklin",
                Twitter = "https://twitter.com/frankfranklin",
                YouTube = "https://youtube.com/frankfranklin",
                Telegram = "https://telegram.com/frankfranklin"
            },
            new UserProfile
            {
                Id = 11,
                UserId = "999e0000-e89b-12d3-a456-426614174008",
                UserName = "gracegray@example.com",
                Email = "gracegray@example.com",
                Avatar = "https://example.com/avatar9.jpg",
                Bio = "I am Grace Gray, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background9.jpg",
                TotalSalesCount = 0,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/gracegray",
                Twitter = "https://twitter.com/gracegray",
                YouTube = "https://youtube.com/gracegray",
                Telegram = "https://telegram.com/gracegray"
            },
            new UserProfile
            {
                Id = 12,
                UserId = "000e1111-e89b-12d3-a456-426614174009",
                UserName = "henryhall@example.com",
                Email = "henryhall@example.com",
                Avatar = "https://example.com/avatar10.jpg",
                Bio = "I am Henry Hall, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background10.jpg",
                TotalSalesCount = 60,
                Followers = new List<string> { 
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
					"0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
					"0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
					"0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097"
				},
                Facebook = "https://facebook.com/henryhall",
                Twitter = "https://twitter.com/henryhall",
                YouTube = "https://youtube.com/henryhall",
                Telegram = "https://telegram.com/henryhall"
            },
            new UserProfile
            {
                Id = 13,
                UserId = "111e2222-e89b-12d3-a456-426614174010",
                UserName = "iggyiglesias@example.com",
                Email = "iggyiglesias@example.com",
                Avatar = "https://example.com/avatar11.jpg",
                Bio = "I am Iggy Iglesias, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background11.jpg",
                TotalSalesCount = 70,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x15d34AAf54267DB7D7c367839AAf71A00a2C6A65",
					"0x14dC79964da2C08b23698B3D3cc7Ca32193d9955",
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788"
                },
                Facebook = "https://facebook.com/iggyiglesias",
                Twitter = "https://twitter.com/iggyiglesias",
                YouTube = "https://youtube.com/iggyiglesias",
                Telegram = "https://telegram.com/iggyiglesias"
            },
            new UserProfile
            {
                Id = 14,
                UserId = "222e3333-e89b-12d3-a456-426614174011",
                UserName = "jessiejames@example.com",
                Email = "jessiejames@example.com",
                Avatar = "https://example.com/avatar12.jpg",
                Bio = "I am Jessie James, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background12.jpg",
                TotalSalesCount = 0,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0x15d34AAf54267DB7D7c367839AAf71A00a2C6A65",
					"0x14dC79964da2C08b23698B3D3cc7Ca32193d9955",
					"0x71bE63f3384f5fb98995898A86B02Fb2426c5788"
                },
                Facebook = "https://facebook.com/jessiejames",
                Twitter = "https://twitter.com/jessiejames",
                YouTube = "https://youtube.com/jessiejames",
                Telegram = "https://telegram.com/jessiejames"
            },
            new UserProfile
            {
                Id = 15,
                UserId = "333e4444-e89b-12d3-a456-426614174012",
                UserName = "karlkennedy@example.com",
                Email = "karlkennedy@example.com",
                Avatar = "https://example.com/avatar13.jpg",
                Bio = "I am Karl Kennedy, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background13.jpg",
                TotalSalesCount = 80,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0xf39Fd6e51aad88F6F4ce6aB8827279cffFb92266",
					"0x70997970C51812dc3A010C7d01b50e0d17dc79C8",
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC"
                },
                Facebook = "https://facebook.com/karlkennedy",
                Twitter = "https://twitter.com/karlkennedy",
                YouTube = "https://youtube.com/karlkennedy",
                Telegram = "https://telegram.com/karlkennedy"
            },
            new UserProfile
            {
                Id = 16,
                UserId = "444e5555-e89b-12d3-a456-426614174013",
                UserName = "lucylu@example.com",
                Email = "lucylu@example.com",
                Avatar = "https://example.com/avatar14.jpg",
                Bio = "I am Lucy Lu, a digital collector.",
                ProfileBackgroundImage = "https://example.com/background14.jpg",
                TotalSalesCount = 0,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0xf39Fd6e51aad88F6F4ce6aB8827279cffFb92266",
					"0x70997970C51812dc3A010C7d01b50e0d17dc79C8",
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC"
                },
                Facebook = "https://facebook.com/lucylu",
                Twitter = "https://twitter.com/lucylu",
                YouTube = "https://youtube.com/lucylu",
                Telegram = "https://telegram.com/lucylu"
            },
            new UserProfile
            {
                Id = 17,
                UserId = "555e6666-e89b-12d3-a456-426614174014",
                UserName = "mikemiller@example.com",
                Email = "mikemiller@example.com",
                Avatar = "https://example.com/avatar15.jpg",
                Bio = "I am Mike Miller, a digital artist.",
                ProfileBackgroundImage = "https://example.com/background15.jpg",
                TotalSalesCount = 90,
                Followers = new List<string>{
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
					"0xf39Fd6e51aad88F6F4ce6aB8827279cffFb92266",
					"0x70997970C51812dc3A010C7d01b50e0d17dc79C8",
					"0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC"
                },
                Facebook = "https://facebook.com/mikemiller",
                Twitter = "https://twitter.com/mikemiller",
                YouTube = "https://youtube.com/mikemiller",
                Telegram = "https://telegram.com/mikemiller"
            }
		);		
    }	
}
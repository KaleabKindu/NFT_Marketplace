using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId);

        // var admin = new AppUser
        // {
        //     Id = "6f09dad5-2268-4410-b755-cf7859927e5f",
        //     Address = "0x68d54e37D6221D1DA835489099D1b69Ce5c8b90d",
        //     Nonce = "ee528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 1
        // };

        // var trader1 = new AppUser
        // {
        //     Id = "6f09dad5-2268-4410-b755-cf7859927f6g",
        //     Address = "0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 2
        // };

        // builder.HasData(
        //     admin,
        //     trader1
        // new AppUser
        // {
        //     Id = "123e4567-e89b-12d3-a456-426614174000",
        //     Address = "0xf39Fd6e51aad88F6F4ce6aB8827279cffFb92266",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 3
        // },
        // new AppUser
        // {
        //     Id = "111e2222-e89b-12d3-a456-426614174001",
        //     Address = "0x70997970C51812dc3A010C7d01b50e0d17dc79C8",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 4
        // },
        // new AppUser
        // {
        //     Id = "333e4444-e89b-12d3-a456-426614174002",
        //     Address = "0x3C44CdDdB6a900fa2b585dd299e03d12FA4293BC",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 5
        // },
        // new AppUser
        // {
        //     Id = "444e5555-e89b-12d3-a456-426614174003",
        //     Address = "0x90F79bf6EB2c4f870365E785982E1f101E93b906",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 6
        // },
        // new AppUser
        // {
        //     Id = "555e6666-e89b-12d3-a456-426614174004",
        //     Address = "0x15d34AAf54267DB7D7c367839AAf71A00a2C6A65",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 7
        // },
        // new AppUser
        // {
        //     Id = "666e7777-e89b-12d3-a456-426614174005",
        //     Address = "0x9965507D1a55bcC2695C58ba16FB37d819B0A4dc",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 8
        // },
        // new AppUser
        // {
        //     Id = "777e8888-e89b-12d3-a456-426614174006",
        //     Address = "0x976EA74026E726554dB657fA54763abd0C3a0aa9",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 9
        // },
        // new AppUser
        // {
        //     Id = "888e9999-e89b-12d3-a456-426614174007",
        //     Address = "0x14dC79964da2C08b23698B3D3cc7Ca32193d9955",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 10
        // },
        // new AppUser
        // {
        //     Id = "999e0000-e89b-12d3-a456-426614174008",
        //     Address = "0x23618e81E3f5cdF7f54C3d65f7FBc0aBf5B21E8f",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 11
        // },
        // new AppUser
        // {
        //     Id = "000e1111-e89b-12d3-a456-426614174009",
        //     Address = "0xa0Ee7A142d267C1f36714E4a8F75612F20a79720",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 12
        // },
        // new AppUser
        // {
        //     Id = "111e2222-e89b-12d3-a456-426614174010",
        //     Address = "0xBcd4042DE499D14e55001CcbB24a551F3b954096",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 13
        // },
        // new AppUser
        // {
        //     Id = "222e3333-e89b-12d3-a456-426614174011",
        //     Address = "0x71bE63f3384f5fb98995898A86B02Fb2426c5788",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 14
        // },
        // new AppUser
        // {
        //     Id = "333e4444-e89b-12d3-a456-426614174012",
        //     Address = "0xFABB0ac9d68B0B445fB7357272Ff202C5651694a",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 15
        // },
        // new AppUser
        // {
        //     Id = "444e5555-e89b-12d3-a456-426614174013",
        //     Address = "0x1CBd3b2770909D4e10f157cABC84C7264073C9Ec",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 16
        // },
        // new AppUser
        // {
        //     Id = "555e6666-e89b-12d3-a456-426614174014",
        //     Address = "0xdF3e18d64BC6A983f673Ab319CCaE4f1a57C7097",
        //     Nonce = "ff528136-2f06-41e6-b417-fdd116d36446",
        //     ProfileId = 17
        // }
        // );
    }
}
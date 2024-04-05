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
			trader1
		);
		
    }
	
}
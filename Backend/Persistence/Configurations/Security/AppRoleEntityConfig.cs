
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class AppRoleEntityConfiguration : IEntityTypeConfiguration<AppRole>
{
    private const string AdminRoleId = "aacb5c92-d9f8-4106-8150-91cb40139030";
    private const string TraderRoleId = "5970d313-8ead-434b-a1cb-cacbc6b5c0e9";
    private const string Admin = "Admin";
    private const string Trader = "Trader";
   
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        var admin = new AppRole
        {
            Id = AdminRoleId,
            Name = Admin,
            NormalizedName = Admin.ToUpperInvariant()
        };
        builder.HasData(admin);

        var trader = new AppRole
        {
            Id = TraderRoleId,
            Name = Trader,
            NormalizedName = Trader.ToUpperInvariant()
        };
        builder.HasData(trader);
    }
}
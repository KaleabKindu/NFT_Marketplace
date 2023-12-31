using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    private const string AdminRoleId = "aacb5c92-d9f8-4106-8150-91cb40139030";

    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = AdminRoleId,
                UserId = "6f09dad5-2268-4410-b755-cf7859927e5f"
            }
        );
    }
}
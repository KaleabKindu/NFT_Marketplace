using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Security;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    private const string AdminRoleId = "aacb5c92-d9f8-4106-8150-91cb40139030";
    private const string TraderRoleId = "5970d313-8ead-434b-a1cb-cacbc6b5c0e9";

    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = AdminRoleId,
                UserId = "6f09dad5-2268-4410-b755-cf7859927e5f"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "6f09dad5-2268-4410-b755-cf7859927f6g"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "123e4567-e89b-12d3-a456-426614174000"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "111e2222-e89b-12d3-a456-426614174001"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "333e4444-e89b-12d3-a456-426614174002"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "444e5555-e89b-12d3-a456-426614174003"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "555e6666-e89b-12d3-a456-426614174004"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "666e7777-e89b-12d3-a456-426614174005"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "777e8888-e89b-12d3-a456-426614174006"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "888e9999-e89b-12d3-a456-426614174007"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "999e0000-e89b-12d3-a456-426614174008"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "000e1111-e89b-12d3-a456-426614174009"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "111e2222-e89b-12d3-a456-426614174010"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "222e3333-e89b-12d3-a456-426614174011"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "333e4444-e89b-12d3-a456-426614174012"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "444e5555-e89b-12d3-a456-426614174013"
            },
            new IdentityUserRole<string>
            {
                RoleId = TraderRoleId,
                UserId = "555e6666-e89b-12d3-a456-426614174014"
            }     
        );
    }
}
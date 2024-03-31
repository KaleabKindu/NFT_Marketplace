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
    }
}
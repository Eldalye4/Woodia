using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class PortfolioItemConfiguration:IEntityTypeConfiguration<PortfolioItem>
{
    public void Configure(EntityTypeBuilder<PortfolioItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.CarpenterProfile)
            .WithMany(x => x.PortfolioItems)
            .HasForeignKey(x => x.CarpenterProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.Title).HasMaxLength(50);
    }
}
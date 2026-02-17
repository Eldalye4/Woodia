using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class PortfolioItemImageConfiguration:IEntityTypeConfiguration<PortfolioItemImage>
{
    public void Configure(EntityTypeBuilder<PortfolioItemImage> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.HasOne(x => x.PortfolioItem)
            .WithMany(x => x.PortfolioItemImages)
            .HasForeignKey(x => x.PortfolioItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
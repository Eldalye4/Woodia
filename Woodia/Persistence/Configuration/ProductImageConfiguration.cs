using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class ProductImageConfiguration:IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasOne(x=>x.Product)
            .WithMany(x=>x.ProductImages)
            .HasForeignKey(x=>x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
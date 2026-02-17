using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class ProductConfiguration:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    { 
        builder.HasKey(x=>x.Id);
        builder.HasOne(x=>x.Category)
            .WithMany(x=>x.Products)
            .HasForeignKey(x=>x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.ModelConfig)
            .IsRequired();
        builder.HasQueryFilter(x=>!x.IsDeleted);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Slug)
        .IsUnique();
        builder.Property(x => x.Slug)
        .HasMaxLength(100);
        builder.Property(x => x.ImageUrl)
        .HasMaxLength(400);
        builder.HasOne(x => x.Parent)
        .WithMany(x => x.SubCategories)
        .HasForeignKey(x => x.ParentId)
        .OnDelete(DeleteBehavior.Restrict);



    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.SlugAr)
        .IsUnique();
        builder.Property(x => x.SlugAr)
        .HasMaxLength(100);
        builder.HasIndex(x => x.SlugEn)
            .IsUnique();
        builder.Property(x => x.SlugEn)
            .HasMaxLength(100);
        
        
        builder.Property(x => x.ImageUrl)
        .HasMaxLength(400);
        builder.HasOne(x => x.Parent)
        .WithMany(x => x.SubCategories)
        .HasForeignKey(x => x.ParentId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.NameAr)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.NameEn)
            .HasMaxLength(150)
            .IsRequired();

    }
}
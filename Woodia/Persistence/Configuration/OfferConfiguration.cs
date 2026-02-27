using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class OfferConfiguration:IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description)
            .HasMaxLength(5000);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.CarpenterId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Job)
            .WithMany(x => x.Offers)
            .HasForeignKey(x => x.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
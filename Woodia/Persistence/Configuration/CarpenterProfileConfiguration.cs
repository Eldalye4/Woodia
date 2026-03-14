using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class CarpenterProfileConfiguration:IEntityTypeConfiguration<CarpenterProfile>
{
    public void Configure(EntityTypeBuilder<CarpenterProfile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ApplicationUser)
            .WithOne(x => x.CarpenterProfile)
            .HasForeignKey<CarpenterProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.HourlyRate)
            .HasPrecision(18, 2);
        

    }
}
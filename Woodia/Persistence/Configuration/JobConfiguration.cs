using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class JobConfiguration:IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x=>x.ApplicationUser)
            .WithMany(x=>x.Job)
            .HasForeignKey(x=>x.ClientId); 
        builder.Property(x=>x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .IsRequired();
        
        
    }
}
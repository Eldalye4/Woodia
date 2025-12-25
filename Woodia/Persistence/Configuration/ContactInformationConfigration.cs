using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContactInformationConfigration : IEntityTypeConfiguration<ContactInformation>
{
    public void Configure(EntityTypeBuilder<ContactInformation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.PhoneNumber)
        .IsUnique();
        builder.Property(x => x.PhoneNumber)
        .IsRequired()
        .HasMaxLength(20);

        builder.HasOne(x => x.governorate)
        .WithMany(x => x.contactInformation)
        .HasForeignKey(x => x.GovernateId);

        
        builder.Property(x => x.AddressLine)
        .HasMaxLength(200)
        .IsRequired();

        builder.Property(x => x.AdditionalInfo)
        .HasMaxLength(200);
        



    }
}


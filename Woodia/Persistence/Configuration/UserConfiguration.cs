using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.OwnsMany(x => x.RefreshTokens)
        .ToTable("RefreshTokens")
        .WithOwner()
        .HasForeignKey("UserId");

        builder.OwnsMany(x => x.otps)
        .ToTable("Otps")
        .WithOwner()
        .HasForeignKey("UserId");

        builder.HasOne(x => x.contactInformation)
        .WithOne(x => x.user)
        .HasForeignKey<ContactInformation>(x => x.UserId);

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "Woodia",
            LastName = "Admin",
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            EmailConfirmed = true,
            userType = DefaultUsers.userType,
            PasswordHash=DefaultUsers.AdminPassword

        });
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {

        builder.HasData([
          new ApplicationRole{
            Id=DefaultRoles.AdminRoleId,
            Name=DefaultRoles.Admin,
            NormalizedName=DefaultRoles.Admin.ToUpper(),
            ConcurrencyStamp=DefaultRoles.AdminRoleConcurrencyStamp

        },
        new ApplicationRole{
            Id=DefaultRoles.CarpenterRoleId,
            Name=DefaultRoles.Carpenter,
            NormalizedName=DefaultRoles.Carpenter.ToUpper(),
            ConcurrencyStamp=DefaultRoles.CarpenterRoleConcurrencyStamp

        },
        new ApplicationRole{
            Id=DefaultRoles.ClientRoleId,
            Name=DefaultRoles.Client,
            NormalizedName=DefaultRoles.Client.ToUpper(),
            ConcurrencyStamp=DefaultRoles.ClientRoleConcurrencyStamp

        }
        ]);
    }
}
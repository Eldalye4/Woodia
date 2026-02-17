using Microsoft.AspNetCore.Identity;
namespace Woodia.Entity;
public class ApplicationRole : IdentityRole
{
    public bool IsDeleted{ get; set; }
}
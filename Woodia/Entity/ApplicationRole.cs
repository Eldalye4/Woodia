using Microsoft.AspNetCore.Identity;

public class ApplicationRole : IdentityRole
{
    public bool IsDeleted{ get; set; }
}
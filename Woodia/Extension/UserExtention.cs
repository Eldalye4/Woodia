using System.Security.Claims;

public static class UserExtention
{
    public static string?  GetUserById(this ClaimsPrincipal user )
    
        => user.FindFirstValue(ClaimTypes.NameIdentifier);
    
}
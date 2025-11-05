public interface IjwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> Roles, IEnumerable<string> Permissions);
    string? ValidateToken(string token);
}
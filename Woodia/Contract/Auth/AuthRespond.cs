public record AuthRespond
(
    string Id,
    string? Email,
    string FirstName,
    string LastName,
    UserType UserType,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration
);
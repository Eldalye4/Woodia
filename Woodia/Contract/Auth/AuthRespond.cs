public record AuthRespond
(
    string Id,
    string? Email,
    string FirstName,
    string LastName,
    string PhotoUrl,
    string userType,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration,
    bool IsProfileComplete
);
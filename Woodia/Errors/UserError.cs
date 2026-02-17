public static class UserErrors
{
    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error DuplicatedPhone =
        new("User.DuplicatedPhone", "Another user with the same Phone is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
        new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);
    public static readonly Error InvalidRole =
       new("User.InvalidRole", "The specified role is not allowed for registration.", StatusCodes.Status400BadRequest);


    public static readonly Error InvalidCode =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status401Unauthorized);
    public static readonly Error InvalidOtp =
        new("User.InvalidCode", "Invalid code", StatusCodes.Status400BadRequest);


    public static readonly Error DuplicatedConfirmation =
        new("User.DuplicatedConfirmation", "Email already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error InvalidGoogleToken =
       new("GoogleToken.Invalid", "Invalid Google Token", StatusCodes.Status401Unauthorized);
       public static readonly Error InvalidFacebookToken =
       new("FacebookToken.Invalid", "Invalid Facebook Token", StatusCodes.Status401Unauthorized);
}

using System.Security.Cryptography;
using Google.Apis.Auth;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Woodia.Entity;


public class AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager
    , IEmailSender emailSender, IjwtProvider jwtProvider, SignInManager<ApplicationUser> signInManager,
EncryptionHelper encryptionHelper, IHttpContextAccessor httpContextAccessor, IOptions<GoogleSettings> optionsGoogle,
ILogger<AuthService> logger, IOptions<FacebookSettings> optionsFacebook, IHttpClientFactory httpClientFactory) : IAuth
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IjwtProvider _jwtProvider = jwtProvider;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly EncryptionHelper _encryptionHelper = encryptionHelper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly FacebookSettings _optionsFacebook = optionsFacebook.Value;
    
    private readonly GoogleSettings _optionsGoogle = optionsGoogle.Value;
    private readonly int _Otpxpiryminit = 15;
    private readonly int _refreshTokenExpiryDays = 14;
    public const string TokenValiateUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
    public const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,email,picture&access_token={0}";

    public async Task<Result<AuthRespond>> GetToken(string Email, string Password, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(Email) is not { } user)
        {
            return Result.failure<AuthRespond>(UserErrors.InvalidCredentials);
        }
        var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);
        if (!result.Succeeded)
        {
            return Result.failure<AuthRespond>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
        }
        var response = await AddTokenAndRefresh(user, cancellationToken);
        

        
        return Result.Success(response );
            
    }


    public async Task<Result<AuthRespond>> LoginGoogle(AuthGoogleRequest authGoogleRequest, CancellationToken cancellationToken)
    {

        var payload = await ValidateGoogleToken(authGoogleRequest.idToken);
        if (await _userManager.FindByEmailAsync(payload.Email) is not { } user)
        {
            return Result.failure<AuthRespond>(UserErrors.InvalidCredentials);
        }
        var response = await AddTokenAndRefresh(user, cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result<AuthRespond>> GetRefreshToken(string token, string refreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);
        _logger.LogInformation("{log}", userId);
        if (userId is null)
            return Result.failure<AuthRespond>(UserErrors.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.failure<AuthRespond>(UserErrors.InvalidCredentials);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);
        if (userRefreshToken is null)
            return Result.failure<AuthRespond>(UserErrors.InvalidRefreshToken);

        userRefreshToken.RevokedOn = DateTime.UtcNow;
        var response = await AddTokenAndRefresh(user, cancellationToken);

        return Result.Success(response);
    }


    public async Task<Result> Register(RegisterRequest request, CancellationToken cancellationToken)
    {

        if (!Enum.TryParse<UserType>(request.Type, true, out var userType))
            return Result.failure(UserErrors.InvalidRole);

        if (userType != UserType.Client && userType != UserType.Carpenter)
            return Result.failure(UserErrors.InvalidRole);
        var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (emailIsExists)
            return Result.failure(UserErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            var (uid, encrypted, expireotp) = GeterateRandExpireotp();
            user.otps.Add(
                new Otp
                {
                    Code = encrypted,
                    ExpiresOn = expireotp,
                    CreatedOn = DateTime.UtcNow,
                    otpType = OtpType.ConfirmEmail
                }
            );
            await _userManager.UpdateAsync(user);

            await SendConfirmationEmail(user, uid);
            return Result.Success();

        }
        var error = result.Errors.First();
        return Result.failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    public async Task<Result> ConfirmEmail(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.failure(UserErrors.InvalidCode);

        if (user.EmailConfirmed)
            return Result.failure(UserErrors.DuplicatedConfirmation);


        var Otp = user.otps
        .OrderByDescending(x => x.CreatedOn)
        .Where(x => x.otpType == OtpType.ConfirmEmail)
        .FirstOrDefault(x => x.IsActive);

        if (Otp is null)
            return Result.failure(UserErrors.InvalidCode);

        var resultOtpConfirm = _encryptionHelper.Decrypt(Otp.Code) == request.Code;
        if (!resultOtpConfirm)
            return Result.failure(UserErrors.InvalidCode);

        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            var roleName = GetRoleUser(user);
            await _userManager.AddToRoleAsync(user, roleName);
            return Result.Success();
        }
        var error = result.Errors.First();
        return Result.failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> RensendEmail(ResendRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success();
        if (await _userManager.IsEmailConfirmedAsync(user))
            return Result.failure(UserErrors.DuplicatedConfirmation);

        var (uid, encrypted, expireotp) = GeterateRandExpireotp();
        user.otps.Add(
            new Otp
            {
                Code = encrypted,
                ExpiresOn = expireotp,
                CreatedOn = DateTime.UtcNow,
                otpType = OtpType.ConfirmEmail
            }
        );
        await _userManager.UpdateAsync(user);

        await SendConfirmationEmail(user, uid);
        return Result.Success();

    }


    public async Task<Result<AuthRespond>> SignupGoogle(RegisterGoogleRequest authGoogleRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (!Enum.TryParse<UserType>(authGoogleRequest.userType, true, out var userType))
                return Result.failure<AuthRespond>(UserErrors.InvalidRole);

            if (userType != UserType.Client && userType != UserType.Carpenter)
                return Result.failure<AuthRespond>(UserErrors.InvalidRole);
            var payload = await ValidateGoogleToken(authGoogleRequest.idToken);

            var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == payload.Email, cancellationToken);
            if (emailIsExists)
                return Result.failure<AuthRespond>(UserErrors.DuplicatedEmail);
            
            var user = new ApplicationUser
            {
                FirstName = payload.GivenName ,
                LastName = payload.FamilyName,
                Email = payload.Email,
                UserName = payload.Email,
                userType = userType,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.failure<AuthRespond>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
            var roleName = GetRoleUser(user);
            await _userManager.AddToRoleAsync(user, roleName);

            
            var response = await AddTokenAndRefresh(user, cancellationToken);
            return Result.Success(response);
        }
        catch (InvalidJwtException ex)
        {
            return Result.failure<AuthRespond>(new Error("Google.InvalidToken", ex.Message, StatusCodes.Status401Unauthorized));
        }
    }

    public async Task<Result<AuthRespond>> AuthenticationWithFacebook(AuthFacebookRequest authFacebookRequest, CancellationToken cancellationToken)
    {
        try
        {
            if (!Enum.TryParse<UserType>(authFacebookRequest.userType, true, out var userType))
                return Result.failure<AuthRespond>(UserErrors.InvalidRole);

            if (userType != UserType.Client && userType != UserType.Carpenter)
                return Result.failure<AuthRespond>(UserErrors.InvalidRole);
            var validatedTokenResult = await validateAccessToken(authFacebookRequest.idToken);

            if (validatedTokenResult is null || validatedTokenResult.Data is null || !validatedTokenResult!.Data!.IsValid)
            {
                return Result.failure<AuthRespond>(UserErrors.InvalidFacebookToken);
            }
            var userInfo = await getUserInfo(authFacebookRequest.idToken);
            if (userInfo is null)
            {
                return Result.failure<AuthRespond>(UserErrors.InvalidFacebookToken);
            }
            if (await _userManager.FindByEmailAsync(userInfo!.Email!) is not { } user)
            {
                user = new ApplicationUser
                {
                    FirstName = userInfo.FirstName!,
                    LastName = userInfo.LastName!,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    userType = userType,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    var error = result.Errors.First();
                    return Result.failure<AuthRespond>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
                }
                var roleName = GetRoleUser(user);
                await _userManager.AddToRoleAsync(user, roleName);

            }
            var response = await AddTokenAndRefresh(user, cancellationToken);
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.failure<AuthRespond>(new Error("Facebook.InvalidToken", ex.Message, StatusCodes.Status401Unauthorized));
        }
    }
    private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleToken(string idToken)
    {
        var validationSettings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _optionsGoogle.ClientID }
        };
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
        return payload;
    }
    private async Task SendConfirmationEmail(ApplicationUser user, int uid)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string>
            {
                {"--- APP NAME ---","Woodia"},
                {"[User's Name]",$"{user.FirstName} {user.LastName}" },
                {"123456",$"{uid}"},
                {"next 15 minutes.",$"next {_Otpxpiryminit} minutes."},
                { "[Your Company]","Woodia"}
            });

        await _emailSender.SendEmailAsync(user.Email!, "Woodia", emailBody);
    }


    private (int uid, string encrypted, DateTime expireotp) GeterateRandExpireotp()
    {
        var rand = new Random();
        var uid = rand.Next(100000, 1000000);
        string encrypted = _encryptionHelper.Encrypt(uid.ToString());
        var expireotp = DateTime.UtcNow.AddMinutes(_Otpxpiryminit);
        return (uid, encrypted, expireotp);
    }



    private async Task<AuthRespond> AddTokenAndRefresh(ApplicationUser user, CancellationToken cancellationToken)
    {
        var (UserRoles, userPermissions) = await GetUserRolesAndPermission(user, cancellationToken);
        var (token, expiresIn) = _jwtProvider.GenerateToken(user, UserRoles, userPermissions);
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        await _userManager.UpdateAsync(user);
        var IsComplete = true;
        if (user.userType == UserType.Carpenter)
        {
            var CarpenterProfile = await _context.CarpenterProfiles.AnyAsync(x => x.UserId == user.Id,cancellationToken);
            if (!CarpenterProfile)
            {
                IsComplete = false;
            }
        }
        return new AuthRespond(user.Id, user.Email, user.FirstName, user.LastName, user.userType.ToString(), token, expiresIn, refreshToken, refreshTokenExpiration,IsComplete);
    }




    private static string GenerateRefreshToken()
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermission(ApplicationUser user, CancellationToken cancellationToken)
    {
        var UserRoles = await _userManager.GetRolesAsync(user);
        var userPermissions = await (from r in _context.Roles
                                     join p in _context.RoleClaims
                                     on r.Id equals p.RoleId
                                     where UserRoles.Contains(r.Name!)
                                     select p.ClaimValue!)
                                   .Distinct()
                                   .ToListAsync(cancellationToken);
        return (UserRoles, userPermissions);
    }

    private string GetRoleUser(ApplicationUser user)
    {
        var roleName = user.userType switch
        {
            UserType.Carpenter => DefaultRoles.Carpenter,
            UserType.Client => DefaultRoles.Client,
            _ => throw new InvalidOperationException("Invalid user type.")
        };
        return roleName;
    }



    private async Task<FacebookTokenValidationResult?> validateAccessToken(string accessToken)
    {
        var formattedUrl = string.Format(TokenValiateUrl, accessToken, _optionsFacebook.ClientID,
        _optionsFacebook.ClientSecret);
        var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
        if (!result.IsSuccessStatusCode)
        {

            return null;
        }
        var responseAsString = await result.Content.ReadAsStringAsync();

        
        var data= JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
 

        return data;
    }

    private async Task<FacebookUserInfo?> getUserInfo(string accessToken)
    {
        var formattedUrl = string.Format(UserInfoUrl, accessToken);
        var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
        if (!result.IsSuccessStatusCode)
        {
            return null;
        }
        var responseAsString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<FacebookUserInfo>(responseAsString)!;
    }

    

    
}
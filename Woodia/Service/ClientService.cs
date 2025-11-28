
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

public class ClientService : IClientService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly EncryptionHelper _encryptionHelper;
    private readonly ILogger<ClientService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly int _Otpxpiryminit = 15;

    public ClientService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, IEmailSender emailSender
    , EncryptionHelper encryptionHelper,ILogger<ClientService> logger,ApplicationDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _emailSender = emailSender;
        _encryptionHelper = encryptionHelper;
        _logger = logger;
        _context = context;
    }
    public async Task<Result<ClientProfileResponse>> GetProfile(String UserId)
    {
        var user = await _userManager.Users
        .Where(x => x.Id == UserId)
        .ProjectToType<ClientProfileResponse>()
        .SingleAsync();

        return Result.Success(user);

    }
    public async Task<Result> UpdateProfile(string userId, ProfileRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        user = request.Adapt(user);
        await _userManager.UpdateAsync(user!);
        return Result.Success();
    }
    public async Task<Result> UpadateEmail(string userId, EmailUpdateRequest emailUpdate)
    {
        var userByEmail = await _userManager.FindByEmailAsync(emailUpdate.Email);
        if (userByEmail is not null)
        {
            return Result.failure(UserErrors.DuplicatedEmail);
        }
        var user = await _userManager.FindByIdAsync(userId);
        var (uid, encrypted, expireotp) = GeterateRandExpireotp();
        user!.otps.Add(
            new Otp
            {
                Code = encrypted,
                ExpiresOn = expireotp,
                CreatedOn = DateTime.UtcNow,
                otpType = OtpType.VarifyEmail

            }
        );
       
        var result = await _userManager.UpdateAsync(user);
        await SendVerifyEmail(user, emailUpdate.Email, uid);

        if (result.Succeeded)
        {
            return Result.Success();
        }
        var error = result.Errors.First();

        return Result.failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }


    public async Task<Result> ResendEmail(string userId, string email)
    {
        if (await _userManager.FindByIdAsync(userId) is not { } user)
        {
            return Result.Success();
        }
        var (uid, encrypted, expireotp) = GeterateRandExpireotp();
        user!.otps.Add(
            new Otp
            {
                Code = encrypted,
                ExpiresOn = expireotp,
                CreatedOn = DateTime.UtcNow,
                otpType = OtpType.VarifyEmail

            }
        );
        var result = await _userManager.UpdateAsync(user);


        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        await SendVerifyEmail(user, email, uid);
        return Result.Success();
    }

    public async Task<Result> VerifyEmail(string userId,ConfirmEmailRequest confirmEmailRequest)
    {
        if (await _userManager.FindByIdAsync(userId) is not { } user)
        {
            return Result.Success();
        }


        var otp = user.otps
        .OrderByDescending(x => x.CreatedOn)
        .Where(x => x.otpType == OtpType.VarifyEmail)
        .FirstOrDefault(x => x.IsActive);

        _logger.LogInformation("otttttttttttttttttttttttp : {otp}", _encryptionHelper.Decrypt(otp!.Code));
        if (otp is null)
            return Result.failure(UserErrors.InvalidOtp);
        var resultOtpConfirm = _encryptionHelper.Decrypt(otp!.Code);
        _logger.LogInformation("{otp}", otp);
        if (confirmEmailRequest.Code != resultOtpConfirm)
        {
            return Result.failure(UserErrors.InvalidOtp);
        }
        user = confirmEmailRequest.Adapt(user);
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Result.Success();
        }
        var error = result.Errors.First();

        return Result.failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result<ContactInformationResponse>> AddContactInformation(string userId,ContactInformationRequest request ,CancellationToken cancellationToken)
    {
        var duplicatePhone =await  _context.contactInformation.AnyAsync(x => x.PhoneNumber == request.PhoneNumber,cancellationToken);
        if (duplicatePhone) {
            return Result.failure<ContactInformationResponse>(UserErrors.DuplicatedPhone);
        }
        var ContactInformation = request.Adapt<ContactInformation>();
        ContactInformation.UserId = userId;
        
        await _context.contactInformation.AddAsync(ContactInformation, cancellationToken);
        await _context.SaveChangesAsync();
        var Response = await _context.contactInformation
                    .Where(x=>x.Id==ContactInformation.Id)
                    .Include(x => x.governorate)
                    .ProjectToType<ContactInformationResponse>()
                    .FirstOrDefaultAsync();
        return Result.Success(Response!);
    }

    public async Task<Result<ContactInformationResponse>> GetContactInformation(int id, CancellationToken cancellationToken)
    {
        var findContactInformation = await _context.contactInformation.AnyAsync(x => x.Id == id, cancellationToken);
        if (!findContactInformation)
            return Result.failure<ContactInformationResponse>(ClientProfileError.ContactNotFound);
        var contactInformation = await _context.contactInformation
                                        .Where(x => x.Id == id)
                                        .Include(x => x.governorate)
                                        .ProjectToType<ContactInformationResponse>()
                                        .FirstAsync();
        return Result.Success(contactInformation);                             
    }

    public async Task<Result> UpdateContactInformation(int id,  ContactInformationRequest request,CancellationToken cancellationToken)
    {
        var findContactInformation = await _context.contactInformation.AnyAsync(x => x.Id == id, cancellationToken);
        if (!findContactInformation)
            return Result.failure<ContactInformationResponse>(ClientProfileError.ContactNotFound);
        var contactInformation = await _context.contactInformation
                                        .Where(x => x.Id == id)
                                        .Include(x => x.governorate)
                                        .FirstAsync();
        contactInformation.AdditionalInfo = request.AdditionalInfo;
        contactInformation.AddressLine = request.AddressLine;
        contactInformation.GovernateId= request.GovernateId;
        contactInformation.PhoneNumber = request.PhoneNumber;
        await _context.SaveChangesAsync();
        return Result.Success();
    }
    private async Task SendVerifyEmail(ApplicationUser user, string email, int uid)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailNewVerify",
            new Dictionary<string, string>
            {
                {"--- APP NAME ---","Woodia"},
                {"[User's Name]",$"{user.FirstName} {user.LastName}" },
                {"123456",$"{uid}"},
                {"next 15 minutes.",$"next {_Otpxpiryminit} minutes."},
                { "[Your Company]","Woodia"}
            });

        await _emailSender.SendEmailAsync(email!, "Woodia", emailBody);
    }


    private (int uid, string encrypted, DateTime expireotp) GeterateRandExpireotp()
    {
        var rand = new Random();
        var uid = rand.Next(100000, 1000000);
        string encrypted = _encryptionHelper.Encrypt(uid.ToString());
        var expireotp = DateTime.UtcNow.AddMinutes(_Otpxpiryminit);
        return (uid, encrypted, expireotp);
    }
}
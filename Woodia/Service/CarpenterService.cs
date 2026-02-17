using CloudinaryDotNet;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.Carpenter;
using Woodia.Entity;

using Woodia.Service.Interface;

namespace Woodia.Service;

public class CarpenterService:ICarpenterService
{
    public readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public CarpenterService(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<Result<ProfessionalProfileResponse>> AddProfessionalProfile(string userId,ProfessionalProfileRequest professionalProfile)
    {
        if (await _userManager.FindByIdAsync(userId) is not { } user)
        {
            return Result.failure<ProfessionalProfileResponse>(UserErrors.InvalidJwtToken);
        }
        var profileExists=await _context.CarpenterProfiles.AnyAsync(x=>x.UserId == userId);
        if (profileExists)
        {
            return Result.failure<ProfessionalProfileResponse>(CarpenterError.ConflictProfile);
        }
        var ProfessionalProfile = professionalProfile.Adapt<CarpenterProfile>();
        ProfessionalProfile.UserId=userId;
        await _context.CarpenterProfiles.AddAsync(ProfessionalProfile);
        await _context.SaveChangesAsync();
        return Result.Success(ProfessionalProfile.Adapt<ProfessionalProfileResponse>());
    }
    public async Task<Result> UpdateProfessionalProfile(string userId, ProfessionalProfileRequest professionalProfile)
    {
        if (await _context.CarpenterProfiles.FirstOrDefaultAsync(x => x.UserId == userId) is not { } carpenterProfile)
        {
            return Result.failure(UserErrors.InvalidJwtToken);
        }

        professionalProfile.Adapt(carpenterProfile);
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<ProfessionalProfileResponse>> GetProfessionalProfile(string userId)
    {
        if (await _context.CarpenterProfiles.FirstOrDefaultAsync(x => x.UserId == userId) is not { } carpenterProfile)
        {
            return Result.failure<ProfessionalProfileResponse>(UserErrors.InvalidJwtToken);
        }

        return Result.Success(carpenterProfile.Adapt<ProfessionalProfileResponse>());
        
    }

    public async Task<Result<PortfolioItemResponse>> AddPortfolioItem(string userId,PortfolioItemRequest portfolioItem,CancellationToken cancellationToken)
    {
        if (await _context.CarpenterProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken) is not
            { } carpenterProfile)
        {
            return Result.failure<PortfolioItemResponse>(UserErrors.InvalidJwtToken);
        }

        var NewPortfolioItem=portfolioItem.Adapt<PortfolioItem>();
        NewPortfolioItem.CarpenterProfileId = carpenterProfile.Id;
        await _context.PortfolioItems.AddAsync(NewPortfolioItem,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(NewPortfolioItem.Adapt<PortfolioItemResponse>());


    }

    public async Task<Result<PortfolioItemResponse>> GetPortfolioItem(int Id,CancellationToken cancellationToken)
    {
        if (await _context.PortfolioItems.Include(x=>x.PortfolioItemImages)
                .FirstOrDefaultAsync(x => x.Id == Id,cancellationToken) is not { } PortfolioItem)
        {
            return Result.failure<PortfolioItemResponse>(CarpenterError.PortfolioItemNotFound);
        }
        return Result.Success(PortfolioItem.Adapt<PortfolioItemResponse>());
    }

    public async Task<Result> UpdatePortfolioItem(int Id,PortfolioItemRequest portfolioItemRequest,CancellationToken cancellationToken)
    {
        if (await _context.PortfolioItems.Include(x=>x.PortfolioItemImages)
                .FirstOrDefaultAsync(x => x.Id == Id,cancellationToken) is not { } PortfolioItem)
        {
            return Result.failure(CarpenterError.PortfolioItemNotFound);
        }
        if (PortfolioItem.PortfolioItemImages != null)
        {
            _context.PortfolioItemImages.RemoveRange(PortfolioItem.PortfolioItemImages);
        }
        portfolioItemRequest.Adapt(PortfolioItem);
        
        PortfolioItem.PortfolioItemImages=portfolioItemRequest.PortfolioImageUrls
                                .Select(x=>new  PortfolioItemImage { Url=x })
                                .ToList();
        await _context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> DeletePortfolioItem(int Id, CancellationToken cancellationToken)
    {
        if (await _context.PortfolioItems
                .Include(x => x.PortfolioItemImages)
                .FirstOrDefaultAsync(x => x.Id == Id) is not { } PortfolioItem)
        {
            return Result.failure(CarpenterError.PortfolioItemNotFound);
        }
        
        _context.PortfolioItems.Remove(PortfolioItem);
        await _context.SaveChangesAsync();
        return Result.Success();

    }

    public async Task<Result<PaginatedList<PortfolioItemResponse>>> GetAllAsync(string userId,RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        if (await _context.CarpenterProfiles.FirstOrDefaultAsync(x => x.UserId == userId) is not { } carpenterProfile)
        {
            return Result.failure<PaginatedList<PortfolioItemResponse>>(UserErrors.InvalidJwtToken);
        }

        var PortfolioItems = _context.PortfolioItems
                                                        .Include(x=>x.PortfolioItemImages)
                                                        .Where(x => x.CarpenterProfileId == carpenterProfile.Id)
                                                        .ProjectToType<PortfolioItemResponse>()
                                                        .AsNoTracking();
        var response =
            await PaginatedList<PortfolioItemResponse>.CreateAsync(PortfolioItems, requestFilters.PageNumber, requestFilters.PageSize,cancellationToken);
        return Result.Success(response);

    }
}
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodia.Abstractions.Consts;
using Woodia.Contract.Role;
using Woodia.Entity;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class RoleService:IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    public RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken,bool? includeDisabled= false) =>
        await _roleManager.Roles
            .Where(x => x.Name != DefaultRoles.Carpenter && x.Name != DefaultRoles.Client
            && (!x.IsDeleted || (includeDisabled.HasValue && includeDisabled.Value)))
            .ProjectToType<RoleResponse>()
            .ToListAsync(cancellationToken);

    public async Task<Result<RoleDetailResponse>> GetAsync(string id)
    {
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.failure<RoleDetailResponse>(RoleError.RoleNotFound);
        var permissions = await _roleManager.GetClaimsAsync(role);
        var response = new RoleDetailResponse(role.Id, role.Name!, role.IsDeleted, permissions.Select(x=>x.Value));
        return Result.Success(response);
    }
    
    public async Task<Result<RoleDetailResponse>> AddAsync(RoleRequest request)
    {
        var roleIsExists = await _roleManager.RoleExistsAsync(request.Name);
        if (roleIsExists)
            return Result.failure<RoleDetailResponse>(RoleError.DuplicatedRole);
        var allowedPermissions = Permissions.GetAllPermissions();
        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.failure<RoleDetailResponse>(RoleError.InvalidPermissions);
        var role = new ApplicationRole
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        var result=await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
            var permissions = request.Permissions
                .Select(x => new IdentityRoleClaim<string>
                {
                    ClaimType = Permissions.Type,
                    ClaimValue = x,
                    RoleId = role.Id
                });
            await _context.AddRangeAsync(permissions);
            await _context.SaveChangesAsync();
            var response = new RoleDetailResponse(role.Id, role.Name!, role.IsDeleted, request.Permissions);
            return Result.Success(response);
        }

        var error = result.Errors.First();
        return Result.failure<RoleDetailResponse>(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest ));
    }
    
    public async Task<Result> UpdateAsync(string id,RoleRequest request)
    {
        var roleIsExists = await _roleManager.Roles.AnyAsync(x=>x.Name==request.Name && x.Id!=id);
        if (roleIsExists)
            return Result.failure<RoleDetailResponse>(RoleError.DuplicatedRole);
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.failure<RoleDetailResponse>(RoleError.RoleNotFound); 
        var allowedPermissions = Permissions.GetAllPermissions();
        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.failure<RoleDetailResponse>(RoleError.InvalidPermissions);
        role.Name=request.Name;
        
        var result=await _roleManager.UpdateAsync(role);
        if (result.Succeeded)
        {
            var currentPermissions = await _context.RoleClaims
                .Where(x => x.RoleId == id && x.ClaimType == Permissions.Type)
                .Select(x => x.ClaimValue)
                .ToListAsync();
            var newPermission = request.Permissions.Except(currentPermissions)
                .Select(x => new IdentityRoleClaim<string>
                {
                    ClaimType = Permissions.Type,
                    ClaimValue = x,
                    RoleId = role.Id
                });
            var removedPermissions = currentPermissions.Except(request.Permissions);
            await _context.RoleClaims
                .Where(x => x.RoleId == id && removedPermissions.Contains(x.ClaimValue))
                .ExecuteDeleteAsync();
            await _context.AddRangeAsync(newPermission);
            await _context.SaveChangesAsync();
            return Result.Success();
            
        }

        var error = result.Errors.First();
        return Result.failure<RoleDetailResponse>(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest ));
    }

    public async Task<Result> ToggleStatusAsync(string id)
    {
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.failure<RoleDetailResponse>(RoleError.RoleNotFound);
        role.IsDeleted = !role.IsDeleted ;
        await _roleManager.UpdateAsync(role);
        return Result.Success();
        
    }
}
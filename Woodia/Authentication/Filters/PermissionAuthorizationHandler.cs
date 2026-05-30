using Microsoft.AspNetCore.Authorization;
using Woodia.Abstractions.Consts;

namespace Woodia.Authentication.Filters;

public class PermissionAuthorizationHandler:AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var user = context.User.Identity;
        if(user is null || !user.IsAuthenticated)
            return;
        var requiredPerm = requirement.Permission;
        var requiredType = Permissions.Type;
        var haspermi = context.User.Claims.Any(x => x.Value == requirement.Permission && x.Type == Permissions.Type);
        if(!haspermi)
            return;
        // if (context.User.Identity is not { IsAuthenticated: true } ||
        //     !context.User.Claims.Any(x => x.Value == requirement.Permission && x.Type == Permissions.Type))
        //     return;
        context.Succeed(requirement);
        return;
    }
}
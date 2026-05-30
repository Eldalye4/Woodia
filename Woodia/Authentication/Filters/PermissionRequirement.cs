using Microsoft.AspNetCore.Authorization;

namespace Woodia.Authentication.Filters;

public class PermissionRequirement(string permission):IAuthorizationRequirement
{
    public string Permission { get; }=permission;
}
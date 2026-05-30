using Microsoft.AspNetCore.Authorization;

namespace Woodia.Authentication.Filters;

public class HasPermissionAttribute(string permission):AuthorizeAttribute(permission)
{
    
}
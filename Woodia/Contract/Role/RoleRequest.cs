namespace Woodia.Contract.Role;

public record RoleRequest(
    string Name,
    IList<string> Permissions);
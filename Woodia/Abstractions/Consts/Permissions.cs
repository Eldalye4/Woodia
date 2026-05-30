namespace Woodia.Abstractions.Consts;

public static class Permissions
{
    public static string Type{get;} = "Permissions";
    
    public const string GetCategorys = "categorys:read" ;
    public const string AddCategorys = "categorys:add";
    public const string UpdateCategorys = "categorys:update";
    public const string DeleteCategorys = "categorys:delete";
    
    public const string GetProducts = "products:read";
    public const string AddProducts = "products:add";
    public const string UpdateProducts = "products:update";
    public const string DeleteProducts = "products:delete";
    
    public static IList<string?> GetAllPermissions()=>
        typeof(Permissions).GetFields().Select(x=>x.GetValue(x) as string).ToList();
    
}
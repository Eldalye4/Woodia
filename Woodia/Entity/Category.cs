using Woodia.Entity;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string ImageUrl { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public bool IsDeleted{get;set;}
    public Category? Parent { get; set; }
    public List<Category> SubCategories { get; set; } = [];
    public List<Product> Products { get; set; } = [];

}


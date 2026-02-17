using Woodia.Enums;

namespace Woodia.Entity;

public class ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int DisplayOrder { get; set; }
    public string Url { get; set; } = string.Empty;
    public ImagePlace ImagePlace{get;set;} 
    public Product Product{get;set;}
}
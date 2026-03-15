using Woodia.Enums;

namespace Woodia.Entity;

public class Product
{
    public int Id { get; set; }
    public ProductLine ProductLine{get;set;}
    public int CategoryId{get;set;}
    public  string DescriptionAr { get; set; }=string.Empty;
    public  string DescriptionEn { get; set; }=string.Empty;
    public string ModelConfig{get;set;}=string.Empty;
    public ProductStatus ProductStatus { get; set; }
    =ProductStatus.Draft;
    public bool IsDeleted{get;set;}
    public List<ProductImage> ProductImages { get; set; } = [];
    public Category Category { get; set; } 
}
namespace Woodia.Entity;

public class PortfolioItem
{
    public int Id { get; set; }
    public string Title { get; set; }=string.Empty;
    public string Description{get;set;}=string.Empty;
    public int CarpenterProfileId { get; set; }
    public CarpenterProfile CarpenterProfile { get; set; } = null!;
    public List<PortfolioItemImage>? PortfolioItemImages { get; set; } 
}
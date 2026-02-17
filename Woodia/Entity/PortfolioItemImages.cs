namespace Woodia.Entity;

public class PortfolioItemImage
{
    public int Id { get; set; }
    public string Url { get; set; } =string.Empty;
    public int PortfolioItemId { get; set; }
    public PortfolioItem? PortfolioItem { get; set; }
}
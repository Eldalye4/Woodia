namespace Woodia.Entity;

public class CarpenterProfile
{
    public int Id { get; set; }
    public decimal HourlyRate { get; set; }
    public string ProfileOverview { get; set; } =string.Empty;
    public string UserId { get; set; }= null!;
    public ApplicationUser ApplicationUser { get; set; }=null!;
    public List<PortfolioItem>? PortfolioItems { get; set; } 
}
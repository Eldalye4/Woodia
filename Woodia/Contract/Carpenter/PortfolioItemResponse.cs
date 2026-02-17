namespace Woodia.Contract.Carpenter;

public record PortfolioItemResponse(
    int Id,
    string Title,
    string Description,
    List<string> PortfolioImageUrls);
namespace Woodia.Contract.Carpenter;

public record PortfolioItemRequest(
    string Title,
    string Description,
    List<string> PortfolioImageUrls
    );
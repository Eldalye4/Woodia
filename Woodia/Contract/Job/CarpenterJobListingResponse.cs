namespace Woodia.Contract.Job;

public record CarpenterJobListingResponse( int Id,
        string Title,
        string Description,
        int DeliveryDay,
        decimal? ExpectedBudget,
        DateTime CreatedAt,
        string CategoryName
        
    );

  
namespace Woodia.Contract.Job;

public record JoblistingResponse(
    int Id,
    string Title,
    string Description,
    int DeliveryDay,
    decimal ExpectedBudget,
    string JobStatus,
    DateTime CreatedAt,
    DateTime? UpdatedAt
    );


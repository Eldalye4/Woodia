using System.Text.Json.Nodes;

namespace Woodia.Contract.Job;

public record JobResponse(
    string ClientId,
    JsonNode ModelConfig,
    string Description,
    int DeliveryDay,
    string Title,
    decimal ExpectedBudget,
    string JobStatus,
    int CategoryId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
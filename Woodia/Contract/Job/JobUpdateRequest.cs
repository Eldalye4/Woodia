using System.Text.Json.Nodes;

namespace Woodia.Contract.Job;

public record JobUpdateRequest(
    JsonNode ModelConfig,
    string Description,
    int DeliveryDay,
    decimal? ExpectedBudget
);
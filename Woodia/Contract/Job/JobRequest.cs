using System.Text.Json.Nodes;

namespace Woodia.Contract.Job;

public record JobRequest(
    JsonNode ModelConfig,
    string Description,
    int DeliveryDay,
    string Title,
    decimal? ExpectedBudget,
    
    int CategoryId
    );
    

using System.Text.Json.Nodes;

namespace Woodia.Contract.Offer;

public record JobSummaryDto
(
    
    JsonNode ModelConfig,
    string Description,
    int DeliveryDay,
    string Title,
    decimal ExpectedBudget,
    string JobStatus

);
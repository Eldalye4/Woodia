using System.Text.Json.Nodes;

namespace Woodia.Contract.Job;

public record CarpenterJobResponse(
   int Id,
    JsonNode ModelConfig,
    string Description,
    int DeliveryDay,
    string Title,
    decimal? ExpectedBudget,
    string CategoryName,
    JobClientBriefDto jobClientBriefDto,
    DateTime CreatedAt
    );
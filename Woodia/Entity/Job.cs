using Woodia.Enums;

namespace Woodia.Entity;

public class Job:AuditableEntity
{
    public int Id{get;set;}
    public string ModelConfig{get;set;}=string.Empty;
    public string Description{get;set;} = string.Empty;
    public int DeliveryDay{get;set;}
    public string Title{get;set;} = string.Empty;
    public decimal? ExpectedBudget{get;set;} 
    public string ClientId{get;set;} = string.Empty;
    public JobStatus JobStatus{get;set;}= JobStatus.Open;
    public int CategoryId{get;set;}
    public ApplicationUser ApplicationUser{get;set;}=null!;
    public List<Offer> Offers { get; set; } = [];
}
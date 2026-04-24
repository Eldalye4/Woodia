
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Abstractions.Consts.ConvertJson;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Offer;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class OfferMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        

        config.NewConfig<Offer, OfferResponse>()
            .Map(dest => dest.Status, src => src.Status);
        config.NewConfig<Offer, OfferSummaryResponse>()
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.JobTitle, src => src.Job.Title);
        config.NewConfig<Offer, OfferDetailsResponse>()
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.JobSummary, src => src.Job);
        config.NewConfig<Job, JobSummaryDto>()
            .Map(dest => dest.JobStatus, src => src.JobStatus)
            .Map(dest => dest.ModelConfig, src => ConvertStringToJson.ValidateJson(src.ModelConfig))
            .Map(dest=>dest.Id,src=>src.Id);

        config.NewConfig<Offer, JobOfferSummaryResponse>()
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CarpenterId, src => src.CarpenterId)
            .Map(dest => dest.CarpenterName, src => $"{src.User.FirstName} {src.User.LastName}");
        config.NewConfig<Offer, JobOfferDetailsResponse>()
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.CarpenterId, src => src.CarpenterId)
            .Map(dest => dest.CarpenterName, src => $"{src.User.FirstName} {src.User.LastName}");
    }
}
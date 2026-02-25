using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Job;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class JobMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<JsonNode, JsonNode>.NewConfig().MapWith(src => src);
        config.NewConfig<JobRequest, Job>()
            .Map(dest => dest.ModelConfig, src => src.ModelConfig.ToString());

        config.NewConfig<Job, JobResponse>()
            .Map(dest => dest.JobStatus, src => src.JobStatus)
            .Map(dest => dest.ModelConfig, src => ValidateJson(src.ModelConfig))
            .Map(dest=>dest.UpdatedAt,src=>src.UpdateAt);
        config.NewConfig<Job, JoblistingResponse>()
            .Map(dest => dest.UpdatedAt, src => src.UpdateAt);

    }

    private static JsonNode ValidateJson(string jsonٍstirng)
    {
        if (string.IsNullOrWhiteSpace(jsonٍstirng))
        {
            return null;
        }

        try
        {
            return JsonNode.Parse(jsonٍstirng);
        }
        catch 
        {
            return null;
        }
    }
}
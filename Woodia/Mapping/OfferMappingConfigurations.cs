
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Mapster;
using Woodia.Contract.Carpenter;
using Woodia.Contract.Offer;
using Woodia.Contract.Product;
using Woodia.Entity;
using Woodia.Enums;

public class OfferMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OfferRequest, Offer>()
            .Map(dest => dest.Status, src => src.Status);

        config.NewConfig<Offer, OfferResponse>()
            .Map(dest => dest.Status, src => src.Status);
    }
}
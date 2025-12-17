
using Mapster;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
        .Map(dest => dest.UserName, src => src.Email)
        .Map(dest => dest.userType, src => src.Type);

        config.NewConfig<ContactInformation, ContactInformationResponse>()
        .Map(dest => dest.Governate, src => src.governorate.NameEn)
        .Map(dest => dest.UserId, src => src.UserId);
        ;

        

        config.NewConfig<ApplicationUser, ContactInformationResponse>()
        .Map(dest => dest.UserId, src => src.Id)
        .Map(dest => dest.AdditionalInfo, src => src.contactInformation.AdditionalInfo)
        .Map(dest => dest.AddressLine, src => src.contactInformation.AddressLine)
        .Map(dest => dest.Governate, src => src.contactInformation.governorate.NameEn)
        .Map(dest => dest.Id, src => src.contactInformation.Id)
        .Map(dest => dest.PhoneNumber, src => src.contactInformation.PhoneNumber);
    }
}
public class ContactInformation
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }= null!;
    public string AddressLine { get; set; } = null!;
    public string AdditionalInfo { get; set; } = string.Empty;
    public int GovernateId { get; set; }
    public string UserId { get; set; } = null!;
    public Governorate governorate { get; set; } = null!;
    public ApplicationUser user { get; set; } = null!;

}
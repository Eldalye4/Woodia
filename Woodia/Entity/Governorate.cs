public class Governorate
{
    public int Id;
    public string NameAr { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public ContactInformation? contactInformation { get; set; }
    
}
using System.Globalization;

namespace Woodia.Enums;

public static class EnumExtensions
{
    public static string ToLocalizedString(this ProductLine productLine,bool isArabic)
    {
        return productLine switch
        {
            ProductLine.OriginalClassic => isArabic ? "كلاسيك أصلي" : "Original Classic",
            ProductLine.OriginalModern => isArabic ? "مودرن أصلي" : "Original Modern",
            ProductLine.Edge => isArabic ? "إيدج" : "Edge",
            ProductLine.Tone => isArabic ? "تون" : "Tone",
            _ => productLine.ToString()
        };
    }
}
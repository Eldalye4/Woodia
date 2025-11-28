using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Namotion.Reflection;

public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
{
    public void Configure(EntityTypeBuilder<Governorate> builder)
    {
        builder.ToTable("Governorate");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NameAr)
        .HasMaxLength(100);
        builder.Property(x => x.NameEn)
        .HasMaxLength(100);

        builder.HasData([
            new Governorate { Id = 1, NameAr = "القاهرة", NameEn = "Cairo" },
            new Governorate { Id = 2, NameAr = "الجيزة", NameEn = "Giza" },
            new Governorate { Id = 3, NameAr = "الإسكندرية", NameEn = "Alexandria" },
            new Governorate { Id = 4, NameAr = "الدقهلية", NameEn = "Dakahlia" },
            new Governorate { Id = 5, NameAr = "البحر الأحمر", NameEn = "Red Sea" },
            new Governorate { Id = 6, NameAr = "البحيرة", NameEn = "Beheira" },
            new Governorate { Id = 7, NameAr = "الفيوم", NameEn = "Fayoum" },
            new Governorate { Id = 8, NameAr = "الغربية", NameEn = "Gharbiya" },
            new Governorate { Id = 9, NameAr = "الإسماعيلية", NameEn = "Ismailia" },
            new Governorate { Id = 10, NameAr = "المنوفية", NameEn = "Monufia" },
            new Governorate { Id = 11, NameAr = "المنيا", NameEn = "Minya" },
            new Governorate { Id = 12, NameAr = "القليوبية", NameEn = "Qalyubia" },
            new Governorate { Id = 13, NameAr = "الوادي الجديد", NameEn = "New Valley" },
            new Governorate { Id = 14, NameAr = "السويس", NameEn = "Suez" },
            new Governorate { Id = 15, NameAr = "أسوان", NameEn = "Aswan" },
            new Governorate { Id = 16, NameAr = "أسيوط", NameEn = "Assiut" },
            new Governorate { Id = 17, NameAr = "بني سويف", NameEn = "Beni Suef" },
            new Governorate { Id = 18, NameAr = "بورسعيد", NameEn = "Port Said" },
            new Governorate { Id = 19, NameAr = "دمياط", NameEn = "Damietta" },
            new Governorate { Id = 20, NameAr = "الشرقية", NameEn = "Sharkia" },
            new Governorate { Id = 21, NameAr = "جنوب سيناء", NameEn = "South Sinai" },
            new Governorate { Id = 22, NameAr = "كفر الشيخ", NameEn = "Kafr El Sheikh" },
            new Governorate { Id = 23, NameAr = "مطروح", NameEn = "Matrouh" },
            new Governorate { Id = 24, NameAr = "الأقصر", NameEn = "Luxor" },
            new Governorate { Id = 25, NameAr = "قنا", NameEn = "Qena" },
            new Governorate { Id = 26, NameAr = "شمال سيناء", NameEn = "North Sinai" },
            new Governorate { Id = 27, NameAr = "سوهاج", NameEn = "Sohag" }]

        );

    }
}
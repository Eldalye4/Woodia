using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EntityGovernorateandseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Governorate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorate", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Governorate",
                columns: new[] { "Id", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "القاهرة", "Cairo" },
                    { 2, "الجيزة", "Giza" },
                    { 3, "الإسكندرية", "Alexandria" },
                    { 4, "الدقهلية", "Dakahlia" },
                    { 5, "البحر الأحمر", "Red Sea" },
                    { 6, "البحيرة", "Beheira" },
                    { 7, "الفيوم", "Fayoum" },
                    { 8, "الغربية", "Gharbiya" },
                    { 9, "الإسماعيلية", "Ismailia" },
                    { 10, "المنوفية", "Monufia" },
                    { 11, "المنيا", "Minya" },
                    { 12, "القليوبية", "Qalyubia" },
                    { 13, "الوادي الجديد", "New Valley" },
                    { 14, "السويس", "Suez" },
                    { 15, "أسوان", "Aswan" },
                    { 16, "أسيوط", "Assiut" },
                    { 17, "بني سويف", "Beni Suef" },
                    { 18, "بورسعيد", "Port Said" },
                    { 19, "دمياط", "Damietta" },
                    { 20, "الشرقية", "Sharkia" },
                    { 21, "جنوب سيناء", "South Sinai" },
                    { 22, "كفر الشيخ", "Kafr El Sheikh" },
                    { 23, "مطروح", "Matrouh" },
                    { 24, "الأقصر", "Luxor" },
                    { 25, "قنا", "Qena" },
                    { 26, "شمال سيناء", "North Sinai" },
                    { 27, "سوهاج", "Sohag" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Governorate");
        }
    }
}

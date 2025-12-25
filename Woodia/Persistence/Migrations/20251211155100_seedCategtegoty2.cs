using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seedCategtegoty2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name", "ParentId", "Slug" },
                values: new object[,]
                {
                    { 3, "http", "Bookcase", 1, "bookcase" },
                    { 4, "http", "Tv Stands", 1, "Tv-Stands" },
                    { 5, "http", "Wardrobes", null, "wardrobes" },
                    { 6, "http", "Closed Wardrobes", 5, "closed-Wardrobes" },
                    { 7, "http", "Semi Open Wardrobes", 5, "Semi-Open-Wardrobes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}

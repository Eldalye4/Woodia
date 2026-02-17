using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-643eedf8cb5e",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELH2TzBeMMTO1Np3XRBC5a2b08avTbBN9qeBoq1vgYDYEZmAZXRXLPGBqSjdmJo79w==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-643eedf8cb5e",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBlEpw2FrdoaVwXLqkwN/8B+0XxnygTweGCvmnEFlvrgxFSHzZ9MPYaPvKTFf1aZLw==");
        }
    }
}

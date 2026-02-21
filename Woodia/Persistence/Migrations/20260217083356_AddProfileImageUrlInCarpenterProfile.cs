using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageUrlInCarpenterProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "CarpenterProfiles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "CarpenterProfiles");
        }
    }
}

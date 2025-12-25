using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editrelationbetweengovernorateandcontactinformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contactInformation_GovernateId",
                table: "contactInformation");

            migrationBuilder.CreateIndex(
                name: "IX_contactInformation_GovernateId",
                table: "contactInformation",
                column: "GovernateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contactInformation_GovernateId",
                table: "contactInformation");

            migrationBuilder.CreateIndex(
                name: "IX_contactInformation_GovernateId",
                table: "contactInformation",
                column: "GovernateId",
                unique: true);
        }
    }
}

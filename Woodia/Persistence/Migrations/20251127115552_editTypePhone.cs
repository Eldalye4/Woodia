using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editTypePhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_AspNetUsers_UserId",
                table: "ContactInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_Governorate_GovernateId",
                table: "ContactInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformation",
                table: "ContactInformation");

            migrationBuilder.RenameTable(
                name: "ContactInformation",
                newName: "contactInformation");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformation_UserId",
                table: "contactInformation",
                newName: "IX_contactInformation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformation_PhoneNumber",
                table: "contactInformation",
                newName: "IX_contactInformation_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformation_GovernateId",
                table: "contactInformation",
                newName: "IX_contactInformation_GovernateId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "contactInformation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_contactInformation",
                table: "contactInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_contactInformation_AspNetUsers_UserId",
                table: "contactInformation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contactInformation_Governorate_GovernateId",
                table: "contactInformation",
                column: "GovernateId",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contactInformation_AspNetUsers_UserId",
                table: "contactInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_contactInformation_Governorate_GovernateId",
                table: "contactInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contactInformation",
                table: "contactInformation");

            migrationBuilder.RenameTable(
                name: "contactInformation",
                newName: "ContactInformation");

            migrationBuilder.RenameIndex(
                name: "IX_contactInformation_UserId",
                table: "ContactInformation",
                newName: "IX_ContactInformation_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_contactInformation_PhoneNumber",
                table: "ContactInformation",
                newName: "IX_ContactInformation_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_contactInformation_GovernateId",
                table: "ContactInformation",
                newName: "IX_ContactInformation_GovernateId");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "ContactInformation",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformation",
                table: "ContactInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_AspNetUsers_UserId",
                table: "ContactInformation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_Governorate_GovernateId",
                table: "ContactInformation",
                column: "GovernateId",
                principalTable: "Governorate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

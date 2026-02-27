using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigrationInOfferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_AspNetUsers_CarpenterId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Jobs_JobId",
                table: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_JobId",
                table: "Offers",
                newName: "IX_Offers_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_CarpenterId",
                table: "Offers",
                newName: "IX_Offers_CarpenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_CarpenterId",
                table: "Offers",
                column: "CarpenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Jobs_JobId",
                table: "Offers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_CarpenterId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Jobs_JobId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_JobId",
                table: "Offer",
                newName: "IX_Offer_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_CarpenterId",
                table: "Offer",
                newName: "IX_Offer_CarpenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_AspNetUsers_CarpenterId",
                table: "Offer",
                column: "CarpenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Jobs_JobId",
                table: "Offer",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

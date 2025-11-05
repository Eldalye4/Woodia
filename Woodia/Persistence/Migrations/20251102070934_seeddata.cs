using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019a4315-0500-7a3e-82f2-64426f10845d", "019a4315-0500-7a3e-82f2-6443c8379619", false, "Admin", "ADMIN" },
                    { "019a4315-0500-7a3e-82f2-644478659818", "019a4315-0500-7a3e-82f2-644532fe0a00", false, "Carpenter", "CARPENTER" },
                    { "019a4315-0500-7a3e-82f2-64466d0e6d66", "019a4315-0500-7a3e-82f2-644751a53a9c", false, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "userType" },
                values: new object[] { "019a4315-0500-7a3e-82f2-643eedf8cb5e", 0, "019a4315-0500-7a3e-82f2-6440907334df", "admin@Woodia.com", true, "Woodia", "Admin", false, null, "ADMIN@WOODIA.COM", "ADMIN@WOODIA.COM", "AQAAAAIAAYagAAAAEBlEpw2FrdoaVwXLqkwN/8B+0XxnygTweGCvmnEFlvrgxFSHzZ9MPYaPvKTFf1aZLw==", null, false, "019a4315-0500-7a3e-82f2-643f095664f1", false, "admin@Woodia.com", 2 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019a4315-0500-7a3e-82f2-64426f10845d", "019a4315-0500-7a3e-82f2-643eedf8cb5e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-644478659818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-64466d0e6d66");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019a4315-0500-7a3e-82f2-64426f10845d", "019a4315-0500-7a3e-82f2-643eedf8cb5e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-64426f10845d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-643eedf8cb5e");
        }
    }
}

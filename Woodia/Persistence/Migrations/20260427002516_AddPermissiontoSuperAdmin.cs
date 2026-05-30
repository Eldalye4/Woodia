using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissiontoSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "categorys:read", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 2, "permissions", "categorys:add", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 3, "permissions", "categorys:update", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 4, "permissions", "categorys:delete", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 5, "permissions", "products:read", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 6, "permissions", "products:add", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 7, "permissions", "products:update", "019a4315-0500-7a3e-82f2-64426f10845d" },
                    { 8, "permissions", "products:delete", "019a4315-0500-7a3e-82f2-64426f10845d" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-64426f10845d",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a4315-0500-7a3e-82f2-64426f10845d",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });
        }
    }
}

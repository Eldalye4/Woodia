using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Woodia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumeJobsCountInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobsCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobsCount",
                table: "Products");
        }
    }
}

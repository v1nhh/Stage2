using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddManagementLogParametersColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Parameters",
                schema: "UserRole",
                table: "ManagementLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parameters",
                schema: "UserRole",
                table: "ManagementLog");
        }
    }
}

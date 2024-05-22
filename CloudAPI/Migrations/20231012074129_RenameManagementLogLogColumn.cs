using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class RenameManagementLogLogColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Log",
                schema: "UserRole",
                table: "ManagementLog",
                newName: "LogResourcePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogResourcePath",
                schema: "UserRole",
                table: "ManagementLog",
                newName: "Log");
        }
    }
}

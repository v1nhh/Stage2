using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class CabinetLogLogColumnAndParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Log",
                schema: "Cabinet",
                table: "CabinetLog",
                newName: "LogResourcePath");

            migrationBuilder.AddColumn<string>(
                name: "Parameters",
                schema: "Cabinet",
                table: "CabinetLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Parameters",
                schema: "Cabinet",
                table: "CabinetLog");

            migrationBuilder.RenameColumn(
                name: "LogResourcePath",
                schema: "Cabinet",
                table: "CabinetLog",
                newName: "Log");
        }
    }
}

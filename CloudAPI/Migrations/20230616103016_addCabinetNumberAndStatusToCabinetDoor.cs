using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class addCabinetNumberAndStatusToCabinetDoor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CabinetNumber",
                schema: "Cabinet",
                table: "CabinetDoor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Cabinet",
                table: "CabinetDoor",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabinetNumber",
                schema: "Cabinet",
                table: "CabinetDoor");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Cabinet",
                table: "CabinetDoor");
        }
    }
}

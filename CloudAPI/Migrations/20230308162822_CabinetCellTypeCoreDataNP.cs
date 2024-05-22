using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class CabinetCellTypeCoreDataNP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Depth", "Height", "Material", "ShortDescr", "SpecCode", "SpecType", "UpdateDT", "Width" },
                values: new object[] { new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187), 0.0, 0.0, "Insert", "KeyCop INS", "KEY", 1, new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187), 0.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Color", "CreateDT", "Depth", "Height", "Material", "ShortDescr", "SpecCode", "SpecType", "UpdateDT", "Width" },
                values: new object[] { "Grey", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 48.0, 15.0, "Metal", "NP Klein", "NP18", 0, new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 18.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Color", "CreateDT", "Depth", "ShortDescr", "SpecCode", "UpdateDT", "Width" },
                values: new object[] { "Grey", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 48.0, "NP Groot", "NP43", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 43.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Color", "CreateDT", "Depth", "Height", "ShortDescr", "SpecCode", "UpdateDT", "Width" },
                values: new object[] { "Grey", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 100.0, 100.0, "L100x100x100", "L100", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), 100.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Color", "Depth", "Height", "ShortDescr", "SpecCode", "Width" },
                values: new object[] { "Grey", 50.0, 50.0, "L50x50x50", "L50", 50.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Depth", "Height", "Material", "ShortDescr", "SpecCode", "SpecType", "UpdateDT", "Width" },
                values: new object[] { new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1179), 30.0, 30.0, "Metal", "Locker 30x30x30", "L30", 0, new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1183), 30.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Color", "CreateDT", "Depth", "Height", "Material", "ShortDescr", "SpecCode", "SpecType", "UpdateDT", "Width" },
                values: new object[] { "None", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187), 0.0, 0.0, "Insert", "KeyCop Insert", "I", 1, new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187), 0.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Color", "CreateDT", "Depth", "ShortDescr", "SpecCode", "UpdateDT", "Width" },
                values: new object[] { "Red", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1188), 30.0, "Locker 30x15x30", "L15", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1189), 30.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Color", "CreateDT", "Depth", "Height", "ShortDescr", "SpecCode", "UpdateDT", "Width" },
                values: new object[] { "Red", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1190), 45.0, 15.0, "Locker 45x45x15", "L45", new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1190), 45.0 });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "Color", "Depth", "Height", "ShortDescr", "SpecCode", "Width" },
                values: new object[] { "Red", 45.0, 15.0, "Locker 45x20x15", "L20", 20.0 });
        }
    }
}

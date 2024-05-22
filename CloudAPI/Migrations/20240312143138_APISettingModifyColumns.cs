using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class APISettingModifyColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Communication",
                table: "MailTemplate",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.DropColumn(
                name: "MessageHeader",
                schema: "Communication",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.AddColumn<string>(
                name: "AuthenticationTriggerName",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAuthentication",
                schema: "Communication",
                table: "APISetting",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Communication",
                table: "APISetting",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "API_KEY",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseT",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestT",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "API_BODY",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "APISetting",
                columns: new[] { "ID", "TriggerName", "RequestT", "ResponseT", "API_URL", "API_HEADERS", "API_BODY", "CrudOperation", "IsActive", "IntegrationSystem", "AuthenticationTriggerName", "HasAuthentication" },
                values: new object[,]
                {
                    { 1, "SEND_DEFECT_ON_SWAP", "", "", "http(s)://", "{}", "{}", 1, false, "", "OAUTH2_CLIENT_CREDENTIALS", true },
                    { 2, "SEND_REPAIRED", "", "", "http(s)://", "{}", "{}", 1, false, "", "OAUTH2_CLIENT_CREDENTIALS", true },
                    { 3, "SEND_REPLACED", "", "", "http(s)://", "{}", "{}", 1, false, "", "OAUTH2_CLIENT_CREDENTIALS", true },
                    { 4, "OAUTH2_CLIENT_CREDENTIALS", "", "", "http(s)://", "{\"Content-Type\":\"application/x-www-form-urlencoded\"}", "{\"grant_type\":\"client_credentials\",\"client_id\":\"{FILL_IN}\", \"client_secret\":\"{FILL_IN}\", \"scope\":\"{FILL_IN_OPTIONAL}\"}", 1, false, "", "", false },
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Communication",
                table: "MailTemplate");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.AddColumn<string>(
                name: "MessageHeader",
                schema: "Communication",
                table: "Request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HasAuthentication",
                schema: "Communication",
                table: "APISetting",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "Active",
                schema: "Communication",
                table: "APISetting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "API_KEY",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResponseT",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestT",
                schema: "Communication",
                table: "APISetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.DropColumn(
                name: "API_BODY",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.DropColumn(
                name: "HasAuthentication",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.DropColumn(
                name: "AuthenticationTriggerName",
                schema: "Communication",
                table: "APISetting");

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "APISetting",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "APISetting",
                columns: new[] { "ID", "API_HEADERS", "API_KEY", "API_URL", "Active", "CrudOperation", "IntegrationSystem", "RequestT", "ResponseT", "TriggerName" },
                values: new object[,]
                {
                    { 1, "{\"HeaderName1\":\"HeaderValue1\",\"HeaderName2\":\"HeaderValue2\"}", "key1", "http://localhost:5001/api/Request/swap", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.SwapNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.SwapNationalePolitieResponse", "SEND_DEFECT_ON_SWAP" },
                    { 2, "{}", "key2", "http://localhost:5001/api/Request/swap", 1, 2, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.SwapNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.SwapNationalePolitieResponse", "SEND_DEFECT_ON_SWAP" },
                    { 3, "{}", "key3", "http://localhost:5001/api/Request/repaired", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.RepairedNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.RepairedNationalePolitieResponse", "SEND_REPAIRED" },
                    { 4, "{}", "key4", "http://localhost:5001/api/Request/repaired", 1, 6, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.RepairedNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.RepairedNationalePolitieResponse", "SEND_REPAIRED" },
                    { 5, "{}", "key5", "http://localhost:5001/api/Request/requestTaakNummer", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.GetRTNNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.GetRTNNationalePolitieResponse", "GET-RTN" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddedMailTemplatesRepairedReplacedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 1, 10, 13, 59, 11, 992, DateTimeKind.Utc).AddTicks(328));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 19,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Item defect gemeld", "Beste IBK beheerder, <br/><br/>Item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode:  '{{errorCodeDescription}}'. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'." });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 20,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Item reported defect", "Dear cabinet administrator, <br/><br/>Item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode:  '{{errorCodeDescription}}'. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 26, "en-US", "personal_item_status_changed_to_replaced", "Your personal item is replaced", "Dear {{userName}}, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported replaced. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'.", null },
                    { 21, "nl-NL", "personal_item_status_changed_to_defect", "Persoonlijke item defect gemeld", "Beste IBK beheerder, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode: '{{errorCodeDescription}}'. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.", null },
                    { 24, "en-US", "personal_item_status_changed_to_repaired", "Your personal item is repaired", "Dear {{userName}}, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported repaired. <br/><br/>You can swap it back at cabinet '{{cabinetName}}' on position '{{positionAlias}}'.", null },
                    { 22, "en-US", "personal_item_status_changed_to_defect", "Personal item reported defect", "Dear cabinet administrator, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode: '{{errorCodeDescription}}'. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'.", null },
                    { 25, "nl-NL", "personal_item_status_changed_to_replaced", "Uw persoonlijke item is vervangen", "Beste {{userName}}, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is vervangen gemeld. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.", null },
                    { 23, "nl-NL", "personal_item_status_changed_to_repaired", "Uw persoonlijke item is gerepareerd", "Beste {{userName}}, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is gerepareerd gemeld. <br/><br/>U kunt het terug omruilen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.", null }
                });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(2726), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3108) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3749), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3785), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3786) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3787), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3788) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3789), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3792), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3792) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3794), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3796), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3799) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3800), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3801) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3802), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3803) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3804), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3805) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3806), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3807) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5249), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5620) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5900), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5917) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5949), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5950) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 1, 5, 13, 37, 32, 785, DateTimeKind.Utc).AddTicks(4011));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 19,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Persoonlijke item defect gemeld", "Beste IBK beheerder, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode:  '{{errorCodeDescription}}'. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'." });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 20,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Personal item reported defect", "Dear cabinet administrator, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode:  '{{errorCodeDescription}}'. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(4412), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(4776) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5445), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5483), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5485) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5487), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5489) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5491), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5493) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5495), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5496) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5498), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5500) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5502), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5503) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5505), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5507) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5509), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5513), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5514) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5516), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5518) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(2305), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(2969) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3506), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3541) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3563), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3564) });
        }
    }
}

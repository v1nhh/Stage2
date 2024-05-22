using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class mailTemplatesSwappedBackAndxternalReferenceID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferenceCode",
                schema: "Item",
                table: "Item",
                newName: "ExternalReferenceID");

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8550), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8554) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8556), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8557) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8557), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8558) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8558), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8559) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8559), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8559) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8560), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8563), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8563) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8563), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8564) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8564), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8565) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8566), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8567) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8567), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8567) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8568), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8568) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8694), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8695) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8696), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8696) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8701), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8701) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8703), new DateTime(2022, 11, 21, 15, 22, 47, 605, DateTimeKind.Utc).AddTicks(8703) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8169), new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8172) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8175), new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8176) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8177), new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8177) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8178), new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8178) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8179), new DateTime(2022, 11, 21, 15, 22, 47, 627, DateTimeKind.Utc).AddTicks(8180) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 11, 21, 15, 22, 47, 644, DateTimeKind.Utc).AddTicks(7288));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Verstoring aangemeld op {{putItemTypeDescription}} {{cabinetLocationDescr}} foutcode: {{errorCodeDescription}}", "Item <span style='font-style:italic'>'{{putItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> van het type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> is op <span style='font-style:italic'>'{{actionDT}}'</span> defect gemeld door gebruiker <span style='font-style:italic'>'{{userName}}'</span> met errorcode: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. U kunt het ophalen aan de IBK <span style='font-style:italic'>'{{cabinetName}}'</span> in kluisje <span style='font-style:italic'>'{{positionAlias}}'</span> op locatie <span style='font-style:italic'>'{{cabinetLocationDescr}}'</span>. <br/><br/>Gebruiker <span style='font-style:italic'>'{{userName}}'</span> heeft nu tijdelijk vervangend item <span style='font-style:italic'>'{{takeItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> van het type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span> in gebruik." });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 22,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Defect reported {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "Item {{putItemDescription}} reported defect on {{actionDT}} by {{userName}} with error: {{errorCodeDescription}}. Cabinet {{cabinetName}} in locker {{positionAlias}} on location {{cabinetLocationDescr}}. <br/><br/>User {{userName}} now has replacement item {{takeItemDescription}}." });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 27, "nl-NL", "personal_item_status_changed_to_swappedback", "Tijdelijke {{putItemTypeDescription}} ingeleverd {{cabinetLocationDescr}} foutcode: {{errorCodeDescription}}", "Agent heeft tijdelijk item bij een IBK weer ingeruild voor zijn eigen item. <br/><br/>Tijdelijk Item <span style='font-style:italic'>'{{putItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> van het type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> is op <span style='font-style:italic'>'{{actionDT}}'</span> door gebruiker <span style='font-style:italic'>'{{userName}}'</span> weer ingeleverd en moet daarom in CMDB ontkoppeld worden van de gebruiker en de status ‘In stock’ krijgen. Dit tijdelijke item heeft de defectmelding: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br/><br/>Gebruiker <span style='font-style:italic'>'{{userName}}'</span> is sinds <span style='font-style:italic'>'{{actionDT}}'</span> in bezit van item <span style='font-style:italic'>'{{takeItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> van het type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; in CMDB moet daarom item <span style='font-style:italic'>'{{takeItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> gekoppeld worden aan gebruiker <span style='font-style:italic'>'{{userName}}'</span> en de status ‘in use’ krijgen. <br/><br/>Graag na de CMDB-wijzigingen de status van het incident van item <span style='font-style:italic'>'{{takeItemDescription}}'</span> met nummer <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> op ‘Resolved’ zetten.", null },
                    { 28, "en-US", "personal_item_status_changed_to_swappedback", "Temporal item {{putItemTypeDescription}} returned {{cabinetLocationDescr}} {{errorCodeDescription}}", "User swapped back temporary item. <br/><br/>Temporal item <span style='font-style:italic'>'{{putItemDescription}}'</span> with <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> of type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> is swapped back at <span style='font-style:italic'>'{{actionDT}}'</span> by user <span style='font-style:italic'>'{{userName}}'</span> and should be disconnected from the user and get status ‘In stock’. This temporal item has errorcode: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br/><br/>User <span style='font-style:italic'>'{{userName}}'</span> is since <span style='font-style:italic'>'{{actionDT}}'</span> in possession of item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> of type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; in CMDB the item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> should be disconnected from user <span style='font-style:italic'>'{{userName}}'</span> and get status ‘in use’. <br/><br/>Please after CMDB-changed put the status of the incident of item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> to ‘Resolved’.", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 28);

            migrationBuilder.RenameColumn(
                name: "ExternalReferenceID",
                schema: "Item",
                table: "Item",
                newName: "ReferenceCode");

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8967), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8976) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8991), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8991) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8992), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8992) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8993), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8993) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8998) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9004) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9006), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9006) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9012), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9012) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9013), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9013) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9014), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9014) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9308), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9308) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9310), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9316) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8932), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8934) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8939), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8939) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8940), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8941) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8942), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8942) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8943), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8943) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 11, 9, 9, 33, 43, 202, DateTimeKind.Utc).AddTicks(2209));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Persoonlijke item defect gemeld", "Beste IBK beheerder, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode: '{{errorCodeDescription}}'. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'." });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 22,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Personal item reported defect", "Dear cabinet administrator, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode: '{{errorCodeDescription}}'. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." });
        }
    }
}

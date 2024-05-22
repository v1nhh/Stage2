using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddedItemDefectMailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Template" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 823, DateTimeKind.Utc).AddTicks(2163), @"<div>
  <span style='font-size:11pt;'>
  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>
    {{body}}
    <br/><br/><br/>


  </p>
    <img src='https://www.capturetech.com/wp-content/uploads/2020/03/Logo-Capturetech-nopayoff.png' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>
  </span></p>
  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>
    <tbody>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Lireweg 42, 2153 PH Nieuw-Vennep<br>
        The Netherlands<strong></strong></td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>
          <a href='http://www.capturetech.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.capturetech.com</a>
        </td>
      </tr>
    </tbody>
  </table>
  <span><a href='https://twitter.com/capturetechnl/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/twitter.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.linkedin.com/company/capturetech/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/linkedin.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.youtube.com/channel/UCA_hKzBYnr3UHDC0Cp0u2kQ' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/youtube.png' style='cursor: pointer; width: 24px; height: 24px;'></a></span>  
  <br>
</div>" });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 19, "nl-NL", "item_status_changed_to_defect", "Persoonlijke item defect gemeld", "Beste IBK beheerder, <br/><br/>Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode:  '{{errorCodeDescription}}'. <br/><br/>U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.", null },
                    { 20, "en-US", "item_status_changed_to_defect", "Personal item reported defect", "Dear cabinet administrator, <br/><br/>Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode:  '{{errorCodeDescription}}'. <br/><br/>You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'.", null }
                });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4221), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4441) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4817), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4826) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4834), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4835) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4836), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4837) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4838), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4838) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4839), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4841) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4843), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4844) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4845), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4845) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4846), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4847) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4848), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4849) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4850), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4851) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4852), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4853) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(5819), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6133) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6429), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6441) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6490), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6491) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Template" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 322, DateTimeKind.Utc).AddTicks(2712), @"<div>
  <span style='font-size:11pt;'>
  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>
    {{body}}
    <br/><br/><br/>


  </p>
    <img src='https://www.capturetech.com/wp-content/uploads/2020/03/Logo-Capturetech-nopayoff.png' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>
  </span></p>
  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>
    <tbody>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Lireweg 42, 2153 PH Nieuw-Vennep<br>
        The Netherlands<strong></strong></td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>
          <a href='http://www.capturetech.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.capturetech.com</a>
        </td>
      </tr>
    </tbody>
  </table>
  <span><a href='https://twitter.com/capturetechnl/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/twitter.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.linkedin.com/company/capturetech/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/linkedin.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.youtube.com/channel/UCA_hKzBYnr3UHDC0Cp0u2kQ' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/youtube.png' style='cursor: pointer; width: 24px; height: 24px;'></a></span>  
  <br>
</div>" });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(4838), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5046) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5538), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5544) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5555), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5556) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5557), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5557) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5558), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5559) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5560), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5561) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5563), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5563) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5564), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5565) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5566), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5567) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5568), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5568) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5569), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5571), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5571) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4290), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4591) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4860), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4873) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4905), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4906) });
        }
    }
}

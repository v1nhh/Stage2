using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddedCreateDtUIP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OutDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                nullable: false,
                defaultValueSql: "getutcdate()");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Template" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 366, DateTimeKind.Utc).AddTicks(3138), @"<div>
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
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(7611), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(7873) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8346), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8358) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8369), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8371) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8372), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8373) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8374), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8375) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8377), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8378) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8381) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8382), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8383) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8385), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8386) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8388), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8389) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8391) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(3988), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5323), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5352) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5371), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5373) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OutDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Template" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 614, DateTimeKind.Utc).AddTicks(5744), @"<div>
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
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(7966), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(8353) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9159), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9166) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9183), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9184) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9185), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9186) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9187), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9188) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9189), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9190) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9197), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9198) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9199), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9201), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9201) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9202), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9203) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9204), new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9205) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(633), new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(929) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1236), new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1253) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1308), new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1309) });
        }
    }
}

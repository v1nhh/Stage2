using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class CabinetActionGenerateLogResourcePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Cabinet.CabinetAction
                SET LogResourcePath = 
                    CASE 
                        WHEN Action = 0 THEN 'cabinetActionLog.none'
                        WHEN Action = 1 THEN 'cabinetActionLog.swapback'
                        WHEN Action = 2 THEN 'cabinetActionLog.return'
                        WHEN Action = 3 THEN 'cabinetActionLog.pickup'
                        WHEN Action = 4 THEN 'cabinetActionLog.borrow'
                        WHEN Action = 5 THEN 'cabinetActionLog.repair'
                        WHEN Action = 6 THEN 'cabinetActionLog.replace'
                        WHEN Action = 7 THEN 'cabinetActionLog.add'
                        WHEN Action = 8 THEN 'cabinetActionLog.remove'
                        WHEN Action = 9 THEN 'cabinetActionLog.swap'
                        WHEN Action = 10 THEN 'cabinetActionLog.repaired'
                    END
                WHERE LogResourcePath IS NULL OR LogResourcePath = ''
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Cabinet.CabinetAction
                SET LogResourcePath = ''
            ");
        }
    }
}

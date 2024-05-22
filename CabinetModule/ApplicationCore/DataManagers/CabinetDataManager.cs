using System;
using System.Collections.Generic;
using System.Linq;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CabinetModule.ApplicationCore.DataManagers
{
    public class CabinetDataManager
    {
        private readonly ILogger<CabinetDataManager> _logger;
        private readonly MainDbContext _context;

        public CabinetDataManager(MainDbContext context, ILogger<CabinetDataManager> logger)
        {
            _logger = logger;
            _context = context;
        }

        #region GetByCabinetNumber

        #nullable enable
        public IQueryable<Entities.Cabinet> GetCabinetByCabinetNumber(string cabinetNumber)
        {
            return _context.Cabinet()
                            .AsNoTracking()
                            .Where(c => c.CabinetNumber.Equals(cabinetNumber));                            
        }
        #nullable disable

        public IQueryable<Entities.CabinetUI> GetCabinetUIByCabinetNumber(string cabinetNumber)
        {
            return _context.CabinetUI()
                .AsNoTracking()
                .Where(cui => cui.CabinetNumber.Equals(cabinetNumber) || cui.CabinetNumber.Equals("DEFAULT"));                
        }

        public IQueryable<Entities.CabinetColumn> GetCabinetColumnsByCabinetNumber(string cabinetNumber)
        {
            return _context.CabinetColumn()
                .AsNoTracking()
                .Where(cc => cc.CabinetNumber.Equals(cabinetNumber));                
        }

        public IQueryable<Entities.CabinetPosition> GetCabinetPositionsByCabinetNumber(string cabinetNumber)
        {
            return _context.CabinetPosition()
                .AsNoTracking()
                .Where(cp => cp.CabinetNumber.Equals(cabinetNumber));                
        }

        public IQueryable<Entities.CTAMRole_Cabinet> GetCTAMRole_CabinetsByCabinetNumber(string cabinetNumber)
        {
            return _context.CTAMRole_Cabinet()
                .AsNoTracking()
                .Where(ctr => ctr.CabinetNumber.Equals(cabinetNumber));
        }
        #endregion

        public IQueryable<Entities.CabinetCell> GetCabinetCellsByCabinetColumnIDs(IEnumerable<int> cabinetColumnIDs)
        {
            return _context.CabinetCell()
                .AsNoTracking()
                .Where(cc => cabinetColumnIDs.Contains(cc.CabinetColumnID));                
        }

        public IQueryable<Entities.CabinetCellType> GetCabinetCellTypesByIds(IEnumerable<int> iDs)
        {
            return _context.CabinetCellType()
                .AsNoTracking()
                .Where(cct => iDs.Contains(cct.ID));
        }

        public IQueryable<Entities.CabinetCellType> GetAllCabinetCellTypes()
        {
            return _context.CabinetCellType()
                .AsNoTracking();
        }

        public IQueryable<Entities.CabinetDoor> GetCabinetDoors(string cabinetNumber)
        {
            return _context.CabinetDoor()
                .AsNoTracking()
                .Where(cd => cd.CabinetNumber == cabinetNumber);     
        }

        public IQueryable<Entities.CTAMRole_Cabinet> GetCTAMRole_CabinetsByRoleIDAndCabinetNumber(int roleID, string cabinetNumber)
        {
            return _context.CTAMRole_Cabinet()
                .AsNoTracking()
                .Where(ctr => ctr.CTAMRoleID.Equals(roleID) && ctr.CabinetNumber.Equals(cabinetNumber));
        }

        public IQueryable<Entities.CTAMRole_Cabinet> GetCTAMRole_CabinetsByRoleID(int roleID)
        {
            return _context.CTAMRole_Cabinet()
                .AsNoTracking()
                .Where(ctr => ctr.CTAMRoleID.Equals(roleID));
        }

        public IQueryable<Entities.CabinetAccessInterval> GetCabinetAccessIntervalsByCTAMRoleIDs(IEnumerable<int> cTAMRoleIDs)
        {
            return _context.CabinetAccessIntervals()
                .AsNoTracking()
                .Where(cai => cTAMRoleIDs.Contains(cai.CTAMRoleID));
        }
    }
}

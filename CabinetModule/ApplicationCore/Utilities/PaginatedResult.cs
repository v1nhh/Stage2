using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<CabinetLog, CabinetLogDTO>> Paginate<CabinetLogDTO>(this IQueryable<CabinetLog> source,
                                                int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<CabinetLog, CabinetLogDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<CabinetAction, CabinetActionDTO>> Paginate<CabinetActionDTO>(this IQueryable<CabinetAction> source,
                                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<CabinetAction, CabinetActionDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<Cabinet, CabinetDTO>> Paginate<CabinetDTO>(this IQueryable<Cabinet> source,
                                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<Cabinet, CabinetDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<CTAMRole_Cabinet, CabinetWebDTO>> Paginate<CabinetWebDTO>(this IQueryable<CTAMRole_Cabinet> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CTAMRole_Cabinet, IMapper, CabinetWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMRole_Cabinet, CabinetWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CabinetDoor, CabinetDoorDTO>> Paginate<CabinetDoorDTO>(this IQueryable<CabinetDoor> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CabinetDoor, IMapper, CabinetDoorDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CabinetDoor, CabinetDoorDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }


        public static async Task<PaginatedResult<Cabinet, CabinetWebDTO>> Paginate<CabinetWebDTO>(this IQueryable<Cabinet> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<Cabinet, IMapper, CabinetWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<Cabinet, CabinetWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

    }
}
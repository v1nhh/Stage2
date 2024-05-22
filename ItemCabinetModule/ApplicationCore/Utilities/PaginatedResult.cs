using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<Item, ItemDTO>> Paginate<ItemDTO>(this IQueryable<Item> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<Item, ItemDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<Item, ItemWebDTO>> Paginate<ItemWebDTO>(this IQueryable<Item> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<Item, IMapper, ItemWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<Item, ItemWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CTAMUserInPossession, UserInPossessionDTO>> Paginate<UserInPossessionDTO>(this IQueryable<CTAMUserInPossession> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<CTAMUserInPossession, UserInPossessionDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<CabinetStock, CabinetStockDTO>> Paginate<CabinetStockDTO>(this IQueryable<CabinetStock> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<CabinetStock, CabinetStockDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<CabinetPosition, CabinetPositionsDetailsEnvelope>> Paginate<CabinetPositionsDetailsEnvelope>(this IQueryable<CabinetPosition> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<CabinetPosition, CabinetPositionsDetailsEnvelope>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>> Paginate<CabinetAccessIntervalDTO>(this IQueryable<CabinetAccessInterval> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CabinetAccessInterval, IMapper, CabinetAccessIntervalDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CabinetAccessInterval, CabinetAccessIntervalDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CTAMUserPersonalItem, UserPersonalItemDTO>> Paginate<UserPersonalItemDTO>(this IQueryable<CTAMUserPersonalItem> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CTAMUserPersonalItem, IMapper, UserPersonalItemDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMUserPersonalItem, UserPersonalItemDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }
    }
}
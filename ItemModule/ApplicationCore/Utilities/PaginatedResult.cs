using AutoMapper;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<Item, ItemDTO>> Paginate<ItemDTO>(this IQueryable<Item> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<Item, ItemDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }
        public static async Task<PaginatedResult<ItemType, ItemTypeDTO>> Paginate<ItemTypeDTO>(this IQueryable<ItemType> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<ItemType, ItemTypeDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }

        public static async Task<PaginatedResult<ErrorCode, ErrorCodeWebDTO>> Paginate<ErrorCodeWebDTO>(this IQueryable<ErrorCode> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<ErrorCode, IMapper, ErrorCodeWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<ErrorCode, ErrorCodeWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>> Paginate<ItemTypeWebDTO>(this IQueryable<CTAMRole_ItemType> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CTAMRole_ItemType, IMapper, ItemTypeWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMRole_ItemType, ItemTypeWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<ItemType, ItemTypeWebDTO>> Paginate<ItemTypeWebDTO>(this IQueryable<ItemType> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<ItemType, IMapper, ItemTypeWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<ItemType, ItemTypeWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }
    }
}
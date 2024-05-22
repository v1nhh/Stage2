using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTAM.Core.Utilities
{
    /*
     * IQueryable<T> -> PaginatedResult { Object: List<S> }
     * This class is used to implement pagination for a specific entity.
     * Use an extension method to this functionality into a IQueryable
     * The IQueryable of type T will be mapped to a PaginagedResult<T, S>
     * object which contains a list of type S, and some page information
     */
    public class PaginatedResult<T, S>
    {
        private const int defaultPageSize = 50;

        public int Total { get; private set; }
        public int Limit { get; private set; }
        public int Page { get; private set; }
        public int OverallTotal { get; set; }
        public int OverallCheckedTotal { get; set; }
        public List<S> Objects { get; set; }

        public PaginatedResult(int pageNumber = 0, int pageSize = defaultPageSize)
        {
            Limit = pageSize;

            if (pageNumber != 0 && (pageNumber > 0))
            {
                Page = pageNumber;
            }
        }

        public async Task<PaginatedResult<T, S>> Paginate(IQueryable<T> queryable, IMapper mapper, Func<T, IMapper, S> mappingFunc = null, Dictionary<string, object> mapperOptions = null)
        {
            Total = queryable.Count();
            if (Limit < 0)
            {
                Limit = Total;
            }

            if (Limit > Total && Total > 0)
            {
                Limit = Total;
                Page = 0;
            }

            int skip = Page * Limit;
            if (skip + Limit > Total)
            {
                skip = Total - (Total % Limit);
                Page = Total / Limit;
            }
            var tempObjects = await queryable.Skip(skip).Take(Limit).ToListAsync();
            if (mappingFunc != null)
            {
                Objects = tempObjects.Select(src => mappingFunc(src, mapper)).ToList();
            }
            else
            {
                Objects = mapper.Map<List<S>>(tempObjects, opts =>
                {
                    if (mapperOptions != null)
                    {
                        foreach (var item in mapperOptions)
                        {
                            opts.Items[item.Key] = item.Value;
                        }
                    }
                });
            }

            return this;
        }
    }
}
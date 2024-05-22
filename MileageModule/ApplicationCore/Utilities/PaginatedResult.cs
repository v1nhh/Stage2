using AutoMapper;
using CTAM.Core.Utilities;
using MileageModule.ApplicationCore.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MileageModule.ApplicationCore.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<Mileage, MileageDTO>> Paginate<MileageDTO>(this IQueryable<Mileage> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<Mileage, MileageDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }
    }
}

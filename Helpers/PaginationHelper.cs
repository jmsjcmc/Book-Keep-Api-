using AutoMapper;
using AutoMapper.QueryableExtensions;
using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<List<TDestination>> paginateandproject<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber,
            int pageSize, 
            IMapper mapper)
        {
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public static Pagination<T> paginatedresponse<T>(
            List<T> items,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            return new Pagination<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public static async Task<Pagination<TDestination>> paginateandmap<TSource, TDestination>(
            IQueryable<TSource> query,
            int pageNumber,
            int pageSize,
            IMapper mapper)
        {
            var totalCount = await query.CountAsync();
            var items = await PaginationHelper.paginateandproject<TSource, TDestination>(
                query, pageNumber, pageSize, mapper);

            return PaginationHelper.paginatedresponse(items, totalCount, pageNumber, pageSize);
        }
    }
}

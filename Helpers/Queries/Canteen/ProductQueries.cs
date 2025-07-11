using Book_Keep.Models.Canteen;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Book_Keep.Helpers.Queries.Canteen
{
    public class ProductQueries
    {
        private readonly AppDbContext _context;
        public ProductQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all paginated products with optional filter for product name
        public IQueryable<Product> paginatedproducts(string? searchTerm = null)
        {
            var query = _context.Product
                .AsNoTracking()
                .Where(p => !p.Removed)
                .OrderByDescending(p => p.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name == searchTerm);
            }

            return query;
        }
        // Query for fetching all listed products with optional filter for product name
        public async Task<List<Product>> productslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Product
                    .AsNoTracking()
                    .Where(p => p.Name == searchTerm && !p.Removed)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Product
                    .AsNoTracking()
                    .Where(p => !p.Removed)
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific product for GET method
        public async Task<Product?> getproductid(int id)
        {
            return await _context.Product
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        // Query for fetching specific product for PATCH/PUT/DELETE methods
        public async Task<Product?> patchproductid(int id)
        {
            return await _context.Product
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

using Book_Keep.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries.Library
{
    public class ShelfSlotQueries
    {
        private readonly AppDbContext _context;
        public ShelfSlotQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all shelf slots with optional filter for shelf row or column
        public IQueryable<ShelfSlot> paginatedshelfslots (string? searchTerm = null)
        {
            var query = _context.ShelfSlot
                .AsNoTracking()
                .Include(s => s.Shelf)
                .Include(s => s.Book)
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query =query.Where(s => s.Row.ToString().Contains(searchTerm) || s.Column.ToString().Contains(searchTerm));
            }
            return query;
        }
        // Query for fetching all shelf slots with optional filter for shelf row or column
        public async Task<List<ShelfSlot>> shelfslotslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.ShelfSlot
                    .AsNoTracking()
                    .Include(s => s.Shelf)
                    .Include(s => s.Book)
                    .Where(s => s.Row.ToString().Contains(searchTerm) || s.Column.ToString().Contains(searchTerm))
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.ShelfSlot
                    .AsNoTracking()
                    .Include(s => s.Shelf)
                    .Include(s => s.Book)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();

            }
        }
        // Query for fetching specific shelf slot for GET method
        public async Task<ShelfSlot?> getshelfslotid(int id)
        {
            return await _context.ShelfSlot
                .AsNoTracking()
                .Include(s => s.Shelf)
                .Include(s => s.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        // Query for fetching specific shelf slot for PATCH/PUT/DELETE methods
        public async Task<ShelfSlot?> patchshelfslotid(int id)
        {
            return await _context.ShelfSlot
                .Include(s => s.Shelf)
                .Include(s => s.Book)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}

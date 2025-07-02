using Book_Keep.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries.Library
{
    public class ShelfQueries
    {
        private readonly AppDbContext _context;
        public ShelfQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all shelves with optional filter for shelve label (paginated)
        public IQueryable<Shelf> paginatedshelves(string? searchTerm = null)
        {
            var query = _context.Shelf
                .AsNoTracking()
                .Include(s => s.Section)
                .Include(s => s.Shelfslot)
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.Label == searchTerm);
            }
            return query;
        }
        // Query for fetching all shelves with optional filter for shelve label (list)
        public async Task<List<Shelf>> shelveslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Shelf
                    .AsNoTracking()
                    .Include(s => s.Section)
                    .Include(s => s.Shelfslot)
                    .Where(s => s.Label == searchTerm)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Shelf
                   .AsNoTracking()
                   .Include(s => s.Section)
                   .Include(s => s.Shelfslot)
                   .OrderByDescending(s => s.Id)
                   .ToListAsync();
            }
        }
        // Query for fetching specific shelf for GET method
        public async Task<Shelf?> getshelfid(int id)
        {
            return await _context.Shelf
                .AsNoTracking()
                .Include(s => s.Section)
                .Include(s => s.Shelfslot)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        // Query for fetching specific shelf for PATCH/PUT/DELETE methods
        public async Task<Shelf?> patchshelfid(int id)
        {
            return await _context.Shelf
                .Include(s => s.Section)
                .Include(s => s.Shelfslot)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}

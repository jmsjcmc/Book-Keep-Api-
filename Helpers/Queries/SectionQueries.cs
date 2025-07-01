using Book_Keep.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class SectionQueries
    {
        private readonly AppDbContext _context;
        public SectionQueries(AppDbContext context  )
        {
            _context = context;
        }
        // Query for fetching all sections with optional filter for section name (Paginated)
        public IQueryable<Section> paginatedsections(string? searchTerm = null)
        {
            var query = _context.Section
                .AsNoTracking()
                .Include(s => s.Room)
                .Include(s => s.Shelf)
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.Name == searchTerm);
            }
            return query;
        }
        // Query for fetching all sections with optinal filter for section name (List)
        public async Task<List<Section>> sectionslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Section
                    .AsNoTracking()
                    .Include(s => s.Room)
                    .Include(s => s.Shelf)
                    .Where(s => s.Name == searchTerm)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Section
                    .AsNoTracking()
                    .Include(s => s.Room)
                    .Include(s => s.Shelf)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific section for GET method
        public async Task<Section?> getsectionid(int id)
        {
            return await _context.Section
                .AsNoTracking()
                .Include(s => s.Room)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        // Query for fetching specific section for PATCH/PUT/DELETE methods
        public async Task<Section?> patchsectionid(int id)
        {
            return await _context.Section
                .Include(s => s.Room)
                .Include(s => s.Shelf)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}

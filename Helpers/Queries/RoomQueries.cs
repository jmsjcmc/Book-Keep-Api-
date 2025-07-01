using Book_Keep.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class RoomQueries
    {
        private readonly AppDbContext _context;
        public RoomQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all rooms with optional filter for room name (paginated)
        public IQueryable<Room> paginatedrooms (string? searchTerm = null)
        {
            var query = _context.Room
                .AsNoTracking()
                .Include(r => r.Section)
                .OrderByDescending(r => r.Id)
                .Where(r => !r.Removed)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r => r.Name == searchTerm);
            }
            return query;
        }
        // Query for fetching all rooms with optional filter for room name (list)
        public async Task<List<Room>> roomslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Room
                    .AsNoTracking()
                    .Include(r => r.Section)
                    .OrderByDescending(r => r.Id)
                    .Where(r => r.Name == searchTerm && !r.Removed)
                    .ToListAsync();
            }
            else
            {
                return await _context.Room
                    .AsNoTracking()
                    .Include(r => r.Section)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific room fo GET method
        public async Task<Room?> getroomid(int id)
        {
            return await _context.Room
                .AsNoTracking()
                .Include(r => r.Section)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific room for PATCH/PUT/DELETE methods
        public async Task<Room?> patchroomid(int id)
        {
            return await _context.Room
                .Include(r => r.Section)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

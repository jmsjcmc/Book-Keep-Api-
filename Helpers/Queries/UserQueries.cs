using AutoMapper;
using Book_Keep.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class UserQueries : BaseApiController
    {
        public UserQueries(AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            
        }
        // Query for fetching all users with optional filter for username
        public IQueryable<User> filteredusers(string? searchTerm = null)
        {
            var query = _context.User
                .AsNoTracking()
                .Include(u => u.Department)
                .OrderByDescending(u => u.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.Username == searchTerm);
            }

            return query;
        }
        // Query for fetching specific user for GET Method
        public async Task<User?> getmethoduserid(int id)
        {
            return await _context.User
                .AsNoTracking()
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific user for PUT/PATCH/DELETE methods
        public async Task<User?> patchmethoduserid(int id)
        {
            return await _context.User
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}


using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class UserQueries
    {
        private readonly AppDbContext _context;
        public UserQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all users with optional filter for first name & last name
        public IQueryable<User> paginatedusers(string? searchTerm = null)
        {
            var query = _context.User
                .AsNoTracking()
                .Include(u => u.Department)
                .Include(u => u.UserRole)
                .Where(u => !u.Removed)
                .OrderByDescending(u => u.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.FirstName == searchTerm || u.LastName == searchTerm);
            };
            return query;
        }
        // Query for fetching all users with pagination with optional filter for first name and last name
        public async Task<List<User>> userslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.User
                    .AsNoTracking()
                    .Include(u => u.Department)
                    .Include(u => u.UserRole)
                    .Where(u => u.FirstName == searchTerm || u.LastName == searchTerm)
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.User
                    .AsNoTracking()
                    .Include(u => u.Department)
                    .Include(u => u.UserRole)
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
        }
        // Query for fetching specific user for GET method
        public async Task<User?> getuserid(int id)
        {
            return await _context.User
                .AsNoTracking()
                .Include(u => u.Department)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        // Query for fetching specific users for PATCH/PUT/DELETE methods
        public async Task<User?> patchuserid(int id)
        {
            return await _context.User
                .Include(u => u.Department)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}

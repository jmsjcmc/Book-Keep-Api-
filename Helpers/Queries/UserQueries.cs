
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

        public IQueryable<User> paginatedusers(string? searchTerm = null)
        {
            var query = _context.User
                .AsNoTracking()
                .OrderByDescending(u => u.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.FirstName == searchTerm || u.LastName == searchTerm);
            };
            return query;
        }

        public async Task<List<User>> userslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.User
                    .AsNoTracking()
                    .Where(u => u.FirstName == searchTerm || u.LastName == searchTerm)
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.User
                    .AsNoTracking()
                    .OrderByDescending(u => u.Id)
                    .ToListAsync();
            }
        }

        public async Task<User?> getuserid(int id)
        {
            return await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> patchuserid(int id)
        {
            return await _context.User
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}

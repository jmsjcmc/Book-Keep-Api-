using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class RoleQueries
    {
        private readonly AppDbContext _context;
        public RoleQueries(AppDbContext context)
        {
            _context = context;
        }
        // query for fetching all paginated roles with optional filter for role name 
        public IQueryable<Role> paginatedroles(string? searchTerm = null)
        {
            var query = _context.Role
                .AsNoTracking()
                .Include(r => r.User)
                .OrderByDescending(r => r.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(r => r.Name == searchTerm);
            }
            return query;
        }
        // Query for fetching all listed roles with optional filter for role name
        public async Task<List<Role>> roleslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Role
               .AsNoTracking()
               .Where(r => r.Name == searchTerm)
               .OrderByDescending(r => r.Id)
               .ToListAsync();
            } else
            {
                return await _context.Role
              .AsNoTracking()
              .OrderByDescending(r => r.Id)
              .ToListAsync();
            }
           
        }
        // Query for fetching specific role for GET method
        public async Task<Role?> getroleid(int id)
        {
            return await _context.Role
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        // Query for fetching specific role for PATCH/PUT/DELETE methods
        public async Task<Role?> patchroleid(int id)
        {
            return await _context.Role
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}

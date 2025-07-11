using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class DepartmentQueries
    {
        private readonly AppDbContext _context;
        public DepartmentQueries(AppDbContext context)
        {
            _context = context;
        }
        // Query for fetching all departments list with optional filter for product name
        public async Task<List<Department>> departmentslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Department
                  .AsNoTracking()
                  .Include(d => d.User)
                  .Where(d => d.DepartmentName == searchTerm)
                  .OrderByDescending(d => d.Id)
                  .ToListAsync();
            }
            else
            {
                return await _context.Department
                  .AsNoTracking()
                  .Include(d => d.User)
                  .OrderByDescending(d => d.Id)
                  .ToListAsync();
            }
        }
        // Query for fetching specific department for GET method
        public async Task<Department?> getdepartmentid(int id)
        {
            return await _context.Department
                .AsNoTracking()
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        // Query for fetching specific deparment for PATCH/PUT/DELETE methods
        public async Task<Department?> patchdepartmentid(int id)
        {
            return await _context.Department
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}

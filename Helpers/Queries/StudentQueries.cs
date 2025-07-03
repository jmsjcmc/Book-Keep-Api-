using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries
{
    public class StudentQueries
    {
        private readonly AppDbContext _context;
        public StudentQueries(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Student> paginatedstudents(string? searchTerm = null)
        {
            var query = _context.Student
                .AsNoTracking()
                .Include(s => s.Department)
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s => s.FirstName == searchTerm || s.LastName == searchTerm);
            }

            return query;
        }

        public async Task<List<Student>> studentslist(string? searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Student
                    .AsNoTracking()
                    .Include(s => s.Department)
                    .Where(s => s.FirstName == searchTerm || s.LastName == searchTerm)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
            else
            {
                return await _context.Student
                    .AsNoTracking()
                    .Include(s => s.Department)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync();
            }
        }

        public async Task<Student?> getstudentid(int id)
        {
            return await _context.Student
                .AsNoTracking()
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student?> patchstudentid(int id)
        {
            return await _context.Student
                 .Include(s => s.Department)
                 .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}

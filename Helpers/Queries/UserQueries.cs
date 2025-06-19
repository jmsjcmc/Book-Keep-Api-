using AutoMapper;
using Book_Keep.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Book_Keep.Helpers.Queries
{
    public class UserQueries : BaseApiController
    {
        public UserQueries(AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            
        }
        public async Task<IQueryable<User>> filteredusers(string? searchTerm = null)
        {
            var query = _context.User
                .AsNoTracking()
                .Include(u => u.Department)
                .OrderByDescending(u => u.Id)
                .AsQueryable();
        }
    }
}

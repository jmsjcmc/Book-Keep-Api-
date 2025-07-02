using Book_Keep.Models.Library;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Helpers.Queries.Library
{
    public class BookQueries
    {
        private readonly AppDbContext _context;
        public BookQueries(AppDbContext context)
        {
            _context = context;
        }
       // Query for fetching all books with optional filter for title
       public IQueryable<Book> booksquery(string? searchTerm = null)
        {
            var query = _context.Book
                .AsNoTracking()
                .OrderByDescending(b => b.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Title == searchTerm);
            }

            return query;
        }
        // Query for fetching specific book for GET Method
        public async Task<Book?> getmethodbookquery(int id)
        {
            return await _context.Book
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        // Query for fetching specific book for PATCH/PUT/DELETE methods
        public async Task<Book?> patchmethodbookquery(int id)
        {
            return await _context.Book
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}

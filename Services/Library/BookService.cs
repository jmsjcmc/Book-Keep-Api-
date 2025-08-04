using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Library;
using Book_Keep.Interfaces.Library;
using Book_Keep.Models;
using Book_Keep.Models.Library;


namespace Book_Keep.Services.Library
{
    public class BookService : BaseService, IBookService
    {
        private readonly BookQueries _query;
        public BookService(AppDbContext context, IMapper mapper, BookQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("books")]
        public async Task<Pagination<BookResponse>> PaginatedBooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.booksquery();
            return await PaginationHelper.paginateandmap<Book, BookResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("book/{id}")]
        public async Task<BookResponse> GetBook(int id)
        {
            var book = await _query.getmethodbookquery(id);
            return _mapper.Map<BookResponse>(book);
        }
        // [HttpPost("book")]
        public async Task<BookResponse> CreateBook(BookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.AddedOn = TimeHelper.GetPhilippineStandardTime();

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return await BookResponse(book.Id);
        }
        // [HttpPatch("book/update/{id}")]
        public async Task<BookResponse> UpdateBook(BookRequest request, int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _mapper.Map(request, book);

            await _context.SaveChangesAsync();

            return await BookResponse(book.Id);
        }
        // [HttpPatch("book/hide/{id}")]
        public async Task<BookResponse> ToggleHide(int id)
        {
            var book = await _query.patchmethodbookquery(id);

            book.Removed = !book.Removed;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();

            return await BookResponse(book.Id);
        }
        // [HttpDelete("book/delete/{id}")]
        public async Task<BookResponse> DeleteBook (int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return await BookResponse(book.Id);
        }
        // Helpers
        private async Task<Book?> GetBookId(int id)
        {
            return await _query.getmethodbookquery(id);
        }
        private async Task<Book?> PatchBookId(int id)
        {
            return await _query.patchmethodbookquery(id);
        }
        private async Task<BookResponse> BookResponse(int id)
        {
            var response = await GetBookId(id);

            return _mapper.Map<BookResponse>(response);
        }
    }
}

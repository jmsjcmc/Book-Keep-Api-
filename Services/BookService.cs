using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;


namespace Book_Keep.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly BookQueries _query;
        public BookService(AppDbContext context, IMapper mapper, BookQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("books")]
        public async Task<Pagination<BookResponse>> paginatedbooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.booksquery();
            return await PaginationHelper.paginateandmap<Book, BookResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpPost("book")]
        public async Task<BookResponse> createbook(BookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.AddedOn = TimeHelper.GetPhilippineStandardTime();

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            return await bookResponse(book.Id);
        }
        // [HttpPatch("book/update/{id}")]
        public async Task<BookResponse> updatebook(BookRequest request, int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _mapper.Map(request, book);

            await _context.SaveChangesAsync();

            return await bookResponse(book.Id);
        }
        // [HttpPatch("book/hide/{id}")]
        public async Task<BookResponse> togglehide(int id)
        {
            var book = await _query.patchmethodbookquery(id);

            book.Removed = !book.Removed;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();

            return await bookResponse(book.Id);
        }
        // [HttpDelete("book/delete/{id}")]
        public async Task<BookResponse> deletebook (int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return await bookResponse(book.Id);
        }
        // Helpers
        private async Task<Book?> getbookid(int id)
        {
            return await _query.getmethodbookquery(id);
        }
        private async Task<Book?> patchbookid(int id)
        {
            return await _query.patchmethodbookquery(id);
        }
        private async Task<BookResponse> bookResponse(int id)
        {
            var response = await getbookid(id);

            return _mapper.Map<BookResponse>(response);
        }
    }
}

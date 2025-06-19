using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;
using Book_Keep.Models.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Services
{
    public class BookService : BaseApiController, IBookService
    {
        private readonly BookQueries _query;
        public BookService(AppDbContext context, IMapper mapper, BookQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("books")]
        public async Task<Pagination<BookResponse>>getbooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.booksquery();
            var totalCount = await query.CountAsync();

            var books = await PaginationHelper.paginateandproject<Book, BookResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(books, totalCount, pageNumber, pageSize);
        }
        // [HttpPost("book")]
        public async Task<BookResponse> createbook([FromBody] BookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.AddedOn = TimeHelper.GetPhilippineStandardTime();

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            var savedBook = await _query.getmethodbookquery(book.Id);
            return _mapper.Map<BookResponse>(savedBook);
        }
        // [HttpPatch("book/update/{id}")]
        public async Task<BookResponse> updatebook([FromBody] BookRequest request, int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _mapper.Map(request, book);

            await _context.SaveChangesAsync();

            return _mapper.Map<BookResponse>(book);
        }
        // [HttpPatch("book/hide/{id}")]
        public async Task<BookResponse> togglehide(int id)
        {
            var book = await _query.patchmethodbookquery(id);

            book.Removed = !book.Removed;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();

            var updatedBook = await _query.getmethodbookquery(book.Id);
            return _mapper.Map<BookResponse>(book);
        }
        // [HttpDelete("book/delete/{id}")]
        public async Task deletebook (int id)
        {
            var book = await _query.patchmethodbookquery(id);

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}

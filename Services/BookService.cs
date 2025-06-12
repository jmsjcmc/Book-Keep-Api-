using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<Pagination<BookResponse>>getbooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.booksquery();
            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BookResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new Pagination<BookResponse>
            {
                Items = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<BookResponse> createbook([FromBody] BookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            book.AddedOn = TimeHelper.GetPhilippineStandardTime();

            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            var savedBook = await _query.getmethodbookquery(book.Id);
            return _mapper.Map<BookResponse>(savedBook);
        }

        public async Task<BookResponse> togglehide(int id)
        {
            var book = await _query.patchmethodbookquery(id);

            book.Removed = !book.Removed;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();

            var updatedBook = await _query.getmethodbookquery(book.Id);
            return _mapper.Map<BookResponse>(book);
        }
    }
}

using Book_Keep.Models;
using Book_Keep.Models.Library.Book;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Interfaces
{
    public interface IBookService
    {
        Task<Pagination<BookResponse>> paginatedbooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<BookResponse> createbook(BookRequest request);
        Task<BookResponse> updatebook(BookRequest request, int id);
        Task<BookResponse> togglehide(int id);
        Task<BookResponse> deletebook(int id);
    }
}

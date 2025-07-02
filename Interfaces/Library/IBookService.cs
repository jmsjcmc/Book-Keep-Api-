using Book_Keep.Models;
using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
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

using Book_Keep.Models;
using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IBookService
    {
        Task<Pagination<BookResponse>> PaginatedBooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<BookResponse> GetBook(int id);
        Task<BookResponse> CreateBook(BookRequest request);
        Task<BookResponse> UpdateBook(BookRequest request, int id);
        Task<BookResponse> ToggleHide(int id);
        Task<BookResponse> DeleteBook(int id);
    }
}

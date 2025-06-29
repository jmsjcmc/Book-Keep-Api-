using Book_Keep.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Interfaces
{
    public interface IBookService
    {
        Task<Pagination<BookResponse>> getbooks(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<BookResponse> createbook([FromBody] BookRequest request);
        Task<BookResponse> updatebook([FromBody] BookRequest request, int id);
        Task<BookResponse> togglehide(int id);
        Task deletebook(int id);
    }
}

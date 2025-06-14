﻿using Book_Keep.Models;
using Book_Keep.Models.Book;
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
        Task<BookResponse> togglehide(int id);
    }
}

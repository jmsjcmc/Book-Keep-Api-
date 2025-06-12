using AutoMapper;
using AutoMapper.QueryableExtensions;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Models.Book;
using Book_Keep.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Controllers
{
    public class BookController : BaseApiController
    {
        private readonly ExcelHelper _excelHelper;
        private readonly BookService _bookService;
        public BookController(AppDbContext context, IMapper mapper, ExcelHelper excelHelper, BookService bookService) : base (context, mapper)
        {
            _excelHelper = excelHelper;
            _bookService = bookService;
        }

        [HttpGet("books/export")]
        public async Task<ActionResult> exportBooks()
        {
            try
            {
                var books = await _context.Book
                    .ToListAsync();

                var file = _excelHelper.exportbooks(books);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Books.xlsx");

            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("books/template")]
        public async Task<ActionResult> template()
        {
            try
            {
                var file = _excelHelper.generatebookstemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BookTemplate.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("books")]
        public async Task<ActionResult<Pagination<BookResponse>>> getBooks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _bookService.getbooks(pageNumber, pageSize, searchTerm);
                return response;

            }catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost("books/import")]
        public async Task<ActionResult> importBooks(IFormFile file)
        {
            try
            {
                var books = _excelHelper.importbooks(file);
                await _context.Book.AddRangeAsync(books);
                await _context.SaveChangesAsync();

                return Ok("Success.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost("book")]
        public async Task<ActionResult<BookResponse>> createBook([FromBody] BookRequest request)
        {
           
            try
            {
                var response = await _bookService.createbook(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BookResponse>> toggleHidden(int id)
        {
            try
            {
                var response = await _bookService.togglehide(id);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

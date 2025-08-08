using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Excel;
using Book_Keep.Models;
using Book_Keep.Models.Library;
using Book_Keep.Services.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Controllers.Library
{
    public class BookController : BaseApiController
    {
        private readonly BookExcel _excel;
        private readonly BookService _bookService;
        public BookController(AppDbContext context, IMapper mapper, BookExcel excel, BookService bookService) : base (context, mapper)
        {
            _excel = excel;
            _bookService = bookService;
        }
        // Export books
        [HttpGet("books/export")]
        public async Task<ActionResult> ExportBooks()
        {
            try
            {
                var books = await _context.Book
                    .ToListAsync();

                var file = _excel.exportbooks(books);
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Books.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Generate book template
        [HttpGet("books/template")]
        public async Task<ActionResult> BooksTemplate()
        {
            try
            {
                var file = _excel.generatebooktemplate();
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BookTemplate.xlsx");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all books (paginated)
        [HttpGet("books/paginated")]
        public async Task<ActionResult<Pagination<BookResponse>>> GetBooks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _bookService.PaginatedBooks(pageNumber, pageSize, searchTerm);
                return response;

            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific book
        [HttpGet("book/{id}")]
        public async Task<ActionResult<BookResponse>> GetBook(int id)
        {
            try
            {
                var response = await _bookService.GetBook(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Import all books
        [HttpPost("books/import")]
        public async Task<ActionResult<List<BookResponse>>> ImportBooks(IFormFile file)
        {
            try
            {
                var books = await _excel.importbooks(file);
                await _context.Book.AddRangeAsync(books);
                await _context.SaveChangesAsync();

                return Ok("Success.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create book
        [HttpPost("book")]
        public async Task<ActionResult<BookResponse>> CreateBook([FromBody] BookRequest request)
        {
            try
            {
                var response = await _bookService.CreateBook(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific book
        [HttpPatch("book/update/{id}")]
        public async Task<ActionResult<BookResponse>> UpdateBook([FromBody] BookRequest request, int id)
        {
            try
            {
                var response = await _bookService.UpdateBook(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Hide specific book without removing in Database
        [HttpPatch("book/hide/{id}")]
        public async Task<ActionResult<BookResponse>> ToggleHidden(int id)
        {
            try
            {
                var response = await _bookService.ToggleHide(id);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific book in database
        [HttpDelete("book/delete/{id}")]
        public async Task<ActionResult<BookResponse>> DeleteBook(int id)
        {
            try
            {
                var response = await _bookService.DeleteBook(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

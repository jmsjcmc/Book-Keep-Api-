using AutoMapper;
using AutoMapper.QueryableExtensions;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Models.Book;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Controllers
{
    public class BookController : BaseApiController
    {
        private readonly ExcelHelper _excelHelper;
        public BookController(AppDbContext context, IMapper mapper, ExcelHelper excelHelper) : base (context, mapper)
        {
            _excelHelper = excelHelper;
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
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = _context.Book
                    .OrderByDescending(b => b.Id)
                    .AsNoTracking();

                var totalCount = await query.CountAsync();

                var books = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<BookResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var response = new Pagination<BookResponse>
                {
                    Items = books,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

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
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                        .ToArray()
                    });
                return BadRequest(new
                {
                    Message = "Validation failed"
                })
            }
            try
            {
                var book = _mapper.Map<Book>(request);
                book.AddedOn = TimeHelper.GetPhilippineStandardTime();

                _context.Book.Add(book);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<BookResponse>(book);
                return response;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message ?? ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<BookResponse>> toggleHidden(int id)
        {
            try
            {
                var book = await _context.Book
                    .FindAsync(id);

                book.Hidden = !book.Hidden;

                await _context.SaveChangesAsync();

                return Ok($"Book with id {id} is changed!");
            }catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}

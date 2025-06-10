using AutoMapper;
using AutoMapper.QueryableExtensions;
using Book_Keep.Helpers;
using Book_Keep.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Controllers
{
    public class BookController : BaseApiController
    {
        public BookController(AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            
        }

        [HttpGet("books/export")]
        public async Task<ActionResult> exportBooks()
        {
            try
            {
                var books = await _context.Book
                    .AsNoTracking()
                    .ToListAsync();

                using var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Books");

                var currentRow = 1;
                workSheet.Cell(currentRow, 1).Value = "Title";
                workSheet.Cell(currentRow, 2).Value = "Isbn";
                workSheet.Cell(currentRow, 3).Value = "Author";
                workSheet.Cell(currentRow, 4).Value = "Publisher";
                workSheet.Cell(currentRow, 5).Value = "Publication Date";
                workSheet.Cell(currentRow, 6).Value = "Edition";
                workSheet.Cell(currentRow, 7).Value = "Language";

                foreach (var book in books)
                {
                    currentRow++;
                    workSheet.Cell(currentRow, 1).Value = book.Title;
                    workSheet.Cell(currentRow, 2).Value = book.Isbn;
                    workSheet.Cell(currentRow, 3).Value = book.Author;
                    workSheet.Cell(currentRow, 4).Value = book.Publisher;
                    workSheet.Cell(currentRow, 5).Value = book.PublicationDate;
                    workSheet.Cell(currentRow, 6).Value = book.Edition;
                    workSheet.Cell(currentRow, 7).Value = book.Language;
                }
                using var stream = new MemoryStream();
                workBook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Books.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("books/template")]
        public async Task<ActionResult> template()
        {
            try
            {
                using var workBook = new XLWorkbook();
                var workSheet = workBook.Worksheets.Add("Book Template");

                var currentRow = 1;
                workSheet.Cell(currentRow, 1).Value = "Title";
                workSheet.Cell(currentRow, 2).Value = "ISBN";
                workSheet.Cell(currentRow, 3).Value = "Author";
                workSheet.Cell(currentRow, 4).Value = "Publisher";
                workSheet.Cell(currentRow, 5).Value = "Publication Date";
                workSheet.Cell(currentRow, 6).Value = "Edition";
                workSheet.Cell(currentRow, 7).Value = "Language";
                workSheet.Row(currentRow).Style.Font.Bold = true;

                var secondRow = 2;
                workSheet.Cell(secondRow, 1).Value = "Sample Book";
                workSheet.Cell(secondRow, 2).Value = "1234567890";
                workSheet.Cell(secondRow, 3).Value = "John Doe";
                workSheet.Cell(secondRow, 4).Value = "Sample Publisher";
                workSheet.Cell(secondRow, 5).Value = "2023-01-01";
                workSheet.Cell(secondRow, 6).Value = "1st";
                workSheet.Cell(secondRow, 7).Value = "English";

                using var stream = new MemoryStream();
                workBook.SaveAs(stream);
                stream.Position = 0;

                return File(stream.ToArray(),
                   "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   "BookTemplate.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
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

            }catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpPost("books/import")]
        public async Task<ActionResult> importBooks(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var workBook = new XLWorkbook(stream);
                var workSheet = workBook.Worksheet(1);
                var rows = workSheet.RangeUsed().RowsUsed().Skip(1);

                var booksToAdd = new List<Book>();

                foreach (var row in rows)
                {
                    try
                    {
                        var book = new Book
                        {
                            Title = row.Cell(1).GetValue<string>(),
                            Isbn = row.Cell(2).GetValue<string>(),
                            Author = row.Cell(3).GetValue<string>(),
                            Publisher = row.Cell(4).GetValue<string>(),
                            PublicationDate = row.Cell(5).GetValue<string>(),
                            Edition = row.Cell(6).GetValue<string>(),
                            Language = row.Cell(7).GetValue<string>(),
                            AddedOn = TimeHelper.GetPhilippineStandardTime()
                        };

                        booksToAdd.Add(book);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing row: {ex.Message}");
                    }
                }

                await _context.Book.AddRangeAsync(booksToAdd);
                await _context.SaveChangesAsync();

                return Ok($"{booksToAdd.Count} books imported successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
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

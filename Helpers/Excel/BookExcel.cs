using Book_Keep.Models.Library;

namespace Book_Keep.Helpers.Excel
{
    public class BookExcel : ExcelHelper
    {
        private readonly string[] bookHeader =
        {
            "Title", "Isbn", "Author",
            "Publisher", "Publication Date", "Edition",
            "Language"
        };
        public BookExcel(AppDbContext context) : base (context)
        {
            
        }
        public byte[] generatebooktemplate()
        {
            var workSheet = createworksheet("Book Template");
            for (int i = 0; i < bookHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = bookHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
                workSheet.Column(i + 1).AdjustToContents();
            }
            save();
            return getbytes();
        }

        public byte[] exportbooks(IEnumerable<Book> books)
        {
            var workSheet = createworksheet("Books");
            int row = 2;

            for (int i = 0; i < bookHeader.Length; i++)
            {
                workSheet.Cell(1, i + 1).Value = bookHeader[i];
                workSheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            foreach (var book in books)
            {
                var values = new object[]
                {
                    book.Title, book.Isbn, book.Author,
                    book.Publisher, book.PublicationDate, book.Edition,
                    book.Language
                };
                for(int col = 0; col < values.Length; col++)
                    workSheet.Cell(row, col + 1).Value = values[col]?.ToString();
                row++;
            }
            workSheet.Columns().AdjustToContents();
            save();
            return getbytes();
        }

        public async Task<List<Book>> importbooks(IFormFile file)
        {
            var books = new List<Book>();
            var rows = getworksheetrows(file);

            foreach (var row in rows)
            {
                books.Add(new Book
                {
                    Title = row.Cell(1).GetValue<string>(),
                    Isbn = row.Cell(2).GetValue<string>(),
                    Author = row.Cell(3).GetValue<string>(),
                    Publisher = row.Cell(4).GetValue<string>(),
                    PublicationDate = row.Cell(5).GetValue<string>(),
                    Edition = row.Cell(6).GetValue<string>(),
                    Language = row.Cell(7).GetValue<string>(),
                });
            }
            return books;
        }
    }
}

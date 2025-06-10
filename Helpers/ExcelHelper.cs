using ClosedXML.Excel;

namespace Book_Keep.Helpers
{
    public class ExcelHelper
    {
        private readonly AppDbContext _context;
        private readonly XLWorkbook _workbook;
        private readonly MemoryStream _stream;

        private readonly string[] bookHeader =
        {
            "Title", "ISBN", "Author", "Publisher",
            "Publication Date", "Edition", "Language"
        };

        public ExcelHelper(AppDbContext context)
        {
            _context = context;
            _workbook = new XLWorkbook();
            _stream = new MemoryStream();
        }

        public byte[] generatebookstemplate()
        {
            var worksheet = createworksheet("Book Template");
            var row = 2;

            for (int i = 0; i < bookHeader.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = bookHeader[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                worksheet.Column(i + 1).AdjustToContents();
            }

            worksheet.Cell(row, 1).Value = "The Art of Clean Code";
            worksheet.Cell(row, 2).Value = "978-1-59327-828-1";
            worksheet.Cell(row, 3).Value = "Robert C. Martin";
            worksheet.Cell(row, 4).Value = "Pearson Education";
            worksheet.Cell(row, 5).Value = "2021-03-15";
            worksheet.Cell(row, 6).Value = "2nd Edition";
            worksheet.Cell(row, 7).Value = "English";

            save();
            return getbytes();
        }

        private IXLWorksheet createworksheet(string sheetname)
        {
            return _workbook.Worksheets.Add(sheetname);
        }

        private void save()
        {
            _workbook.SaveAs(_stream);
        }

        private byte[] getbytes()
        {
            return _stream.ToArray();
        }

        private IEnumerable<IXLRow> getworksheetrows(IFormFile file, int skiprows = 1)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            return worksheet.RowsUsed().Skip(skiprows);
        }
    }
}

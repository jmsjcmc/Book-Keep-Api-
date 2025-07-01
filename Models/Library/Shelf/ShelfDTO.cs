namespace Book_Keep.Models.Library
{
    public class ShelfRequest
    {
        public string Label { get; set; }
        public int Sectionid { get; set; }
    }
    public class ShelfResponse
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Section { get; set; }
        public Boolean Removed { get; set; }
    }
}

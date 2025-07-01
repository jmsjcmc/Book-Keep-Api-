namespace Book_Keep.Models.Library
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string PublicationDate { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public int Shelfslotid { get; set; }
        public ShelfSlot Shelfslot { get; set; }
        public bool Removed { get; set; }
        public DateTime AddedOn { get; set; }
    }
}

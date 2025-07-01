namespace Book_Keep.Models.Library
{
    public class ShelfSlot
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Boolean Removed { get; set; }
        public int Shelfid { get; set; }
        public Shelf Shelf { get; set; }
        public Book? Book { get; set; }
    }
}

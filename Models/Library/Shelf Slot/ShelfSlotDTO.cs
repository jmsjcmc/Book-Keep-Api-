namespace Book_Keep.Models.Library
{
    public class ShelfSlotRequest
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Shelfid { get; set; }
    }
    public class ShelfSlotResponse
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public string Label { get; set; }
        public Boolean Removed { get; set; }
    }
}

namespace Book_Keep.Models.Library
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Removed { get; set; }
        public int Roomid { get; set; }
        public Room Room { get; set; }
        public ICollection<Shelf> Shelf { get; set; }
    }
}

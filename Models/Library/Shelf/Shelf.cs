using Book_Keep.Models.Library;

namespace Book_Keep.Models.Library
{
    public class Shelf
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public Boolean Removed { get; set; }
        public int Sectionid { get; set; }
        public Section Section { get; set; }
        public ICollection<ShelfSlot> Shelfslot { get; set; }
    }
}

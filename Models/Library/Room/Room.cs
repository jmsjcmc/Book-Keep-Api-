namespace Book_Keep.Models.Library
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Removed { get; set; }
        public ICollection<Section> Section { get; set; }
    }
}

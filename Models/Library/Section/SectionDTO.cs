namespace Book_Keep.Models.Library
{
    public class SectionRequest
    {
        public string Name { get; set; }
        public int Roomid { get; set; }
    }
    public class SectionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public Boolean Removed { get; set; }
    }
}

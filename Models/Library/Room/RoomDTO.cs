namespace Book_Keep.Models.Library
{
    public class RoomRequest
    {
        public string Name { get; set; }
    }
    public class RoomResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Removed { get; set; }
    }
}

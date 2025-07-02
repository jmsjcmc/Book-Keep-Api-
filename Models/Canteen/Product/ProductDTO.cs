namespace Book_Keep.Models.Canteen
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Boolean Active { get; set; }
        public Boolean Removed { get; set; }
    }
}

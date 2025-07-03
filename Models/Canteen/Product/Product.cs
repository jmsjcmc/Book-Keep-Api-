namespace Book_Keep.Models.Canteen
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Variant { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public Boolean Active { get; set; }
        public Boolean Removed { get; set; }
    }
}

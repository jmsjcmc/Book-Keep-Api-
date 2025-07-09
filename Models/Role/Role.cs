namespace Book_Keep.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> User{ get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}

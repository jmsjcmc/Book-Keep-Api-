namespace Book_Keep.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public Boolean Removed { get; set; }
        public ICollection<User> User{ get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }

    public class UserRole
    {
        public int Userid { get; set; }
        public User User { get; set; }
        public int Roleid { get; set; }
        public Role Role { get; set; }
    }
}

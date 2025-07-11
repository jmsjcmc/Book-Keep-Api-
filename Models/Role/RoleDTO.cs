namespace Book_Keep.Models
{
    public class RoleRequest
    {
        public string Name { get; set; }
    }
    public class RoleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public Boolean Removed { get; set; }
    }
    public class UserRoleRequest
    {
        public int Userid { get; set; }
        public int Roleid { get; set; }
    }
    public class UserRoleResponse
    {
        public UserResponse User { get; set; }
        public RoleResponse Role { get; set; }
    }
}

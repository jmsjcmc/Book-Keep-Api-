namespace Book_Keep.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }

    public class RolePermission
    {
        public int Roleid { get; set; }
        public Role Role { get; set; }
        public int Permissionid { get; set; }
        public Permission Permission { get; set; }
    }
}

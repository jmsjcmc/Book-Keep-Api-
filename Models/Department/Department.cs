namespace Book_Keep.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public Boolean Removed { get; set; }
        public ICollection<User> User { get; set; }
    }
}

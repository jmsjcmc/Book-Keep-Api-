namespace Book_Keep.Models.User
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<User> User { get; set; }
    }
}

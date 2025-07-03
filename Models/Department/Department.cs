namespace Book_Keep.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public bool Removed { get; set; }
        public ICollection<User> User { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}

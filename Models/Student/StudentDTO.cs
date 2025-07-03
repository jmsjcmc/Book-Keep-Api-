using Book_Keep.Models.Library;

namespace Book_Keep.Models
{
    public class StudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
    }
    public class  StudentResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public string Password { get; set; }
        public Boolean Removed { get; set; }
        public string DepartmentName { get; set; }
    }
}

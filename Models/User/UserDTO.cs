namespace Book_Keep.Models
{
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public int Departmentid { get; set; }
    }
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Boolean Removed { get; set; }
    }
    public class UserWithDepartmentResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Userid { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Boolean Removed { get; set; }
    }

    public class Login
    {
        public string Userid { get; set; }
        public string Password { get; set; }
    }
}

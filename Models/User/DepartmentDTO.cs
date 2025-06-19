namespace Book_Keep.Models.User
{
    public class DepartmentRequest
    {
        public string DepartmentName { get; set; }
    }

    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
    }
}

using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IStudentService
    {
        Task<Pagination<StudentResponse>> paginatedstudents(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<StudentResponse>> studentslist(string? searchTerm = null);
        Task<StudentResponse> getstudent(int id);
        Task<StudentResponse> createstudent(StudentRequest request);
        Task<StudentResponse> updatestudent(StudentRequest request, int id);
        Task<StudentResponse> removestudent(int id);
        Task<StudentResponse> deletestudent(int id);
    }
}

using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponse>> DepartmentsList(string? searchTerm = null);
        Task<DepartmentResponse> GetDepartment(int id);
        Task<DepartmentResponse> CreateDepartment(DepartmentRequest request);
        Task<DepartmentResponse> UpdateDepartment(DepartmentRequest request, int id);
        Task<DepartmentResponse> RemoveDepartment(int id);
        Task<DepartmentResponse> DeleteDepartment(int id);
    }
}

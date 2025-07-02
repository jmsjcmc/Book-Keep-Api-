using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentResponse>> departmentslist(string? searchTerm = null);
        Task<DepartmentResponse> getdepartment(int id);
        Task<DepartmentResponse> createdepartment(DepartmentRequest request);
        Task<DepartmentResponse> updatedepartment(DepartmentRequest request, int id);
        Task<DepartmentResponse> removedepartment(int id);
        Task<DepartmentResponse> deletedepartment(int id);
    }
}

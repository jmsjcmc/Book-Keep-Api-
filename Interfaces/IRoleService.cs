using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> RolesList(string? searchTerm = null);
        Task<RoleResponse> GetRole(int id);
        Task<RoleResponse> CreateRole(RoleRequest request);
        Task<RoleResponse> UpdateRole(RoleRequest request, int id);
        Task<RoleResponse> ToggleStatus(int id);
        Task<RoleResponse> RemoveRole(int id);
        Task<RoleResponse> DeleteRole(int id);
    }
}

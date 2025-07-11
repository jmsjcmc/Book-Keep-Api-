using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleResponse>> roleslist(string? searchTerm = null);
        Task<RoleResponse> getrole(int id);
        Task<RoleResponse> createrole(RoleRequest request);
        Task<RoleResponse> updaterole(RoleRequest request, int id);
        Task<RoleResponse> togglestatus(int id);
        Task<RoleResponse> removerole(int id);
        Task<RoleResponse> deleterole(int id);
    }
}

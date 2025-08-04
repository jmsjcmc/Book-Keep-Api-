using Book_Keep.Models;
using System.Security.Claims;

namespace Book_Keep.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserWithDepartmentResponse>> PaginatedUsers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<UserWithDepartmentResponse>> UsersList(string? searchTerm = null);
        Task<UserWithDepartmentResponse> GetUser(int id);
        Task<UserResponse> GetUserDetail(ClaimsPrincipal detail);
        Task<object> UserLogin(Login request);
        //Task<UserResponse> AssignRole(UserRoleRequest request);
        Task<UserWithDepartmentResponse> CreateUser(UserRequest request);
        Task<UserWithDepartmentResponse> UpdateUser(UserRequest request, int id);
        Task<UserWithDepartmentResponse> RemoveUser(int id);
        Task<UserWithDepartmentResponse> DeleteUser(int id);
    }
}

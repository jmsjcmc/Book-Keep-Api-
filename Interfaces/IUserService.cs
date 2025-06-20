using Book_Keep.Models;
using Book_Keep.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<UserResponse> getuser(int id);
        Task<UserResponse> createuser([FromBody] UserRequest request);
        Task<UserResponse> updateuser([FromBody] UserRequest request, int id);
        Task hideuser(int id);
        Task deleteuser(int id);
    }
}

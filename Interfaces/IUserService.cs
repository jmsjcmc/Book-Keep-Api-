﻿using Book_Keep.Models;

namespace Book_Keep.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserWithDepartmentResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<UserWithDepartmentResponse>> userslist(string? searchTerm = null);
        Task<UserWithDepartmentResponse> getuser(int id);
        Task<UserWithDepartmentResponse> createuser(UserRequest request);
        Task<UserWithDepartmentResponse> updateuser(UserRequest request, int id);
        Task<UserWithDepartmentResponse> removeuser(int id);
        Task<UserWithDepartmentResponse> deleteuser(int id);
    }
}

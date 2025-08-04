using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;
using Book_Keep.Validators;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book_Keep.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserQueries _query;
        private readonly RoleQueries _roleQuery;
        private readonly UserValidator _validator;
        private readonly AuthenticationHelper _authHelper;
        public UserService(UserQueries query, RoleQueries roleQuery, UserValidator validator, AuthenticationHelper authHelper, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
            _roleQuery = roleQuery;
            _validator = validator;
            _authHelper = authHelper;
        }
        // [HttpGet("users/paginated")]
        public async Task<Pagination<UserWithDepartmentResponse>> PaginatedUsers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedusers(searchTerm);
            return await PaginationHelper.paginateandmap<User, UserWithDepartmentResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("users/list")]
        public async Task<List<UserWithDepartmentResponse>> UsersList(string? searchTerm = null)
        {
            var users = await _query.userslist(searchTerm);
            return _mapper.Map<List<UserWithDepartmentResponse>>(users);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserWithDepartmentResponse> GetUser(int id)
        {
            var user = await GetUserId(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
        // [HttpGet("user-detail")]
        public async Task<UserResponse> GetUserDetail(ClaimsPrincipal detail)
        {
            int userId = UserValidator.ValidateUserClaim(detail);
            var user = await GetUserId(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("login")]
        public async Task<object> UserLogin(Login request)
        {
            await _validator.ValidateLoginRequest(request);

            var user = await _context.User
                .SingleOrDefaultAsync(u => u.Userid == request.Userid);

            var accessToken = _authHelper.generateaccesstoken(user);
            await _context.SaveChangesAsync();
            return new
            {
                AccessToken = accessToken
            };
        }
        //public async Task<UserRoleResponse> assignrole(UserRoleRequest request)
        //{
        //    var user = await ValidateUser(request.Userid);
        //    var role = await ValidateRole(request.Roleid);
        //}
        // [HttpPost("user")]
        public async Task<UserWithDepartmentResponse> CreateUser(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.CreatedOn = TimeHelper.GetPhilippineStandardTime();

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserWithDepartmentResponse> UpdateUser(UserRequest request, int id)
        {
            var user = await PatchUserId(id);

            _mapper.Map(request, user);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserWithDepartmentResponse> RemoveUser(int id)
        {
            var user = await PatchUserId(id);

            user.Removed = true;

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task<UserWithDepartmentResponse> DeleteUser(int id)
        {
            var user = await PatchUserId(id);

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return await UserResponse(user.Id);
        }
        // Helpers
        private async Task<User?> PatchUserId(int id)
        {
            return await _query.patchuserid(id);
        }
        private async Task<User?> GetUserId(int id)
        {
            return await _query.getuserid(id);
        }
        private async Task<Role?> PatchRoleId(int id)
        {
            return await _roleQuery.patchroleid(id);
        }
        private async Task<Role?> GetRoleId(int id)
        {
            return await _roleQuery.getroleid(id);
        }
        private async Task<UserWithDepartmentResponse> UserResponse(int id)
        {
            var user = await GetUserId(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
        // Validators
        private async Task<User> ValidateUser(int id)
        {
            var user = await PatchUserId(id);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            return user;
        }
        private async Task<Role> ValidateRole(int id)
        {
            var role = await PatchRoleId(id);

            if (role == null)
            {
                throw new ArgumentException("Role not found.");
            }

            return role;
        }
       
    }
}

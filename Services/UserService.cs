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
        public async Task<Pagination<UserWithDepartmentResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedusers(searchTerm);
            return await PaginationHelper.paginateandmap<User, UserWithDepartmentResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("users/list")]
        public async Task<List<UserWithDepartmentResponse>> userslist(string? searchTerm = null)
        {
            var users = await _query.userslist(searchTerm);
            return _mapper.Map<List<UserWithDepartmentResponse>>(users);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserWithDepartmentResponse> getuser(int id)
        {
            var user = await getuserid(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
        // [HttpGet("user-detail")]
        public async Task<UserResponse> getuserdetail(ClaimsPrincipal detail)
        {
            int userId = UserValidator.ValidateUserClaim(detail);
            var user = await getuserid(userId);

            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("login")]
        public async Task<object> userlogin(Login request)
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

        public async Task<UserRoleResponse> assignrole(UserRoleRequest request)
        {
            var user = await ValidateUser(request.Userid);
            var role = await ValidateRole(request.Roleid);
        }
        // [HttpPost("user")]
        public async Task<UserWithDepartmentResponse> createuser(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.CreatedOn = TimeHelper.GetPhilippineStandardTime();

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserWithDepartmentResponse> updateuser(UserRequest request, int id)
        {
            var user = await patchuserid(id);

            _mapper.Map(request, user);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task<UserWithDepartmentResponse> removeuser(int id)
        {
            var user = await patchuserid(id);

            user.Removed = true;

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task<UserWithDepartmentResponse> deleteuser(int id)
        {
            var user = await patchuserid(id);

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // Helpers
        private async Task<User?> patchuserid(int id)
        {
            return await _query.patchuserid(id);
        }
        private async Task<User?> getuserid(int id)
        {
            return await _query.getuserid(id);
        }
        private async Task<Role?> patchroleid(int id)
        {
            return await _roleQuery.patchroleid(id);
        }
        private async Task<Role?> getroleid(int id)
        {
            return await _roleQuery.getroleid(id);
        }
        private async Task<UserWithDepartmentResponse> userResponse(int id)
        {
            var user = await getuserid(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
        // Validators
        private async Task<User> ValidateUser(int id)
        {
            var user = await patchuserid(id);

            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            return user;
        }
        private async Task<Role> ValidateRole(int id)
        {
            var role = await patchroleid(id);

            if (role == null)
            {
                throw new ArgumentException("Role not found.");
            }

            return role;
        }
       
    }
}

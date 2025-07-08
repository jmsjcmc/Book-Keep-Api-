using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;
using Book_Keep.Validators;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserQueries _query;
        private readonly UserValidator _validator;
        private readonly AuthenticationHelper _authHelper;
        public UserService(UserQueries query, UserValidator validator, AuthenticationHelper authHelper, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
            _validator = validator;
            _authHelper = authHelper;
        }
        // 
        public async Task<Pagination<UserWithDepartmentResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedusers(searchTerm);
            return await PaginationHelper.paginateandmap<User, UserWithDepartmentResponse>(query, pageNumber, pageSize, _mapper);
        }
        // 
        public async Task<List<UserWithDepartmentResponse>> userslist(string? searchTerm = null)
        {
            var users = await _query.userslist(searchTerm);
            return _mapper.Map<List<UserWithDepartmentResponse>>(users);
        }
        // 
        public async Task<UserWithDepartmentResponse> getuser(int id)
        {
            var user = await getuserid(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
        // 
        public async Task<object> userlogin(Login request)
        {
            await _validator.ValidateLoginRequest(request);

            var user = await _context.User
                .SingleOrDefaultAsync(u => u.Userid == request.Userid);

            var accessToken = _authHelper.generateaccesstoken(user);
            await _context.SaveChangesAsync();
            return new
            {
                accessToken = accessToken
            };
        }
        // 
        public async Task<UserWithDepartmentResponse> createuser(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.CreatedOn = TimeHelper.GetPhilippineStandardTime();

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // 
        public async Task<UserWithDepartmentResponse> updateuser(UserRequest request, int id)
        {
            var user = await patchuserid(id);

            _mapper.Map(request, user);

            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // 
        public async Task<UserWithDepartmentResponse> removeuser(int id)
        {
            var user = await patchuserid(id);

            user.Removed = true;

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return await userResponse(user.Id);
        }
        // 
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
        private async Task<UserWithDepartmentResponse> userResponse(int id)
        {
            var user = await getuserid(id);
            return _mapper.Map<UserWithDepartmentResponse>(user);
        }
    }
}

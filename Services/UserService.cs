using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;
using Book_Keep.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserQueries _query;
        public UserService(UserQueries query, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("users")]
        public async Task<Pagination<UserResponse>> paginatedusers(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.filteredusers(searchTerm);
            var totalcount = await query.CountAsync();

            var users = await PaginationHelper.paginateandproject<User, UserResponse>(
                query, pageNumber, pageSize, _mapper);

            return PaginationHelper.paginatedresponse(users, totalcount, pageNumber, pageSize);
        }
        // [HttpGet("user/{id}")]
        public async Task<UserResponse> getuser(int id)
        {
            var user = await _query.getmethoduserid(id);
            return _mapper.Map<UserResponse>(user);
        }
        // [HttpPost("user")]
        public async Task<UserResponse> createuser([FromBody] UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            var saveduser = _query.getmethoduserid(user.Id);
            return _mapper.Map<UserResponse>(saveduser);
        }
        // [HttpPatch("user/update/{id}")]
        public async Task<UserResponse> updateuser([FromBody] UserRequest request, int id)
        {
            var user = await _query.patchmethoduserid(id);
            _mapper.Map(request, user);
            user.UpdatedOn = TimeHelper.GetPhilippineStandardTime();

            await _context.SaveChangesAsync();
            var updateduser = _query.getmethoduserid(user.Id);
            return _mapper.Map<UserResponse>(updateduser);
        }
        // [HttpPatch("user/hide/{id}")]
        public async Task hideuser (int id)
        {
            var user = await _query.patchmethoduserid(id);

            user.Removed = true;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
        // [HttpDelete("user/delete/{id}")]
        public async Task deleteuser(int id)
        {
            var user = await _query.patchmethoduserid(id);

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

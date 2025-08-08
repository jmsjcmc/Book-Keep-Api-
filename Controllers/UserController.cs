using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserService _service;
        public UserController(AppDbContext context, IMapper mapper, UserService service) : base(context, mapper)
        {
            _service = service;
        }
        // Fetch all paginated users with optional filter for firstname & lastname
        [HttpGet("users/paginated")]
        public async Task<ActionResult<Pagination<UserWithDepartmentResponse>>> PaginatedUsers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.PaginatedUsers(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all listed users with optional filter for firstname & lastname
        [HttpGet("users/list")]
        public async Task<ActionResult<List<UserWithDepartmentResponse>>> UsersList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.UsersList(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> GetUser(int id)
        {
            try
            {
                var response = await _service.GetUser(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch authenticated user details
        [HttpGet("user-detail")]
        public async Task<ActionResult<UserResponse>> UserDetail()
        {
            try
            {
                var response = await _service.GetUserDetail(User);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // User login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login request)
        {
            try
            {
                var response = await _service.UserLogin(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new user
        [HttpPost("user")]
        public async Task<ActionResult<UserWithDepartmentResponse>> CreateUser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _service.CreateUser(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific user 
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> UpdateUser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _service.UpdateUser(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific user without removing in database (Soft delete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> RemoveUser(int id)
        {
            try
            {
                var response = await _service.RemoveUser(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific user in database 
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> DeleteUser(int id)
        {
            try
            {
                var response = await _service.DeleteUser(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

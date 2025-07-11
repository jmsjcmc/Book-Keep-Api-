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
        public async Task<ActionResult<Pagination<UserWithDepartmentResponse>>> paginatedusers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedusers(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all listed users with optional filter for firstname & lastname
        [HttpGet("users/list")]
        public async Task<ActionResult<List<UserWithDepartmentResponse>>> userslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.userslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> getuser(int id)
        {
            try
            {
                var response = await _service.getuser(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch authenticated user details
        [HttpGet("user-detail")]
        public async Task<ActionResult<UserResponse>> userdetail()
        {
            try
            {
                var response = await _service.getuserdetail(User);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // User login
        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] Login request)
        {
            try
            {
                var response = await _service.userlogin(request);
                return Ok(response);
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new user
        [HttpPost("user")]
        public async Task<ActionResult<UserWithDepartmentResponse>> createuser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _service.createuser(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific user 
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> updateuser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _service.updateuser(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific user without removing in database (Soft delete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> removeuser(int id)
        {
            try
            {
                var response = await _service.removeuser(id);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific user in database 
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult<UserWithDepartmentResponse>> deleteuser(int id)
        {
            try
            {
                var response = await _service.deleteuser(id);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

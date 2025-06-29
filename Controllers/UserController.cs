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

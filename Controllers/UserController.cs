using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Models.User;
using Book_Keep.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserService _userService;
        public UserController(AppDbContext context, IMapper mapper, UserService userservice) : base (context, mapper)
        {
            _userService = userservice;
        }
        // Fetch all users with optional filter for username
        [HttpGet("users")]
        public async Task<ActionResult<Pagination<UserResponse>>> allusers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string searchTerm = null)
        {
            try
            {
                var response = await _userService.paginatedusers(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific user
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponse>> specificuser(int id)
        {
            try
            {
                var response = await _userService.getuser(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }


        // Create user
        [HttpPost("user")]
        public async Task<ActionResult<UserResponse>> createuser([FromBody] UserRequest request)
        {
            try
            {
                var response = await _userService.createuser(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific user
        [HttpPatch("user/update/{id}")]
        public async Task<ActionResult<UserResponse>> updateuser([FromBody] UserRequest request, int id)
        {
            try
            {
                var response = await _userService.updateuser(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific user without removing in Database (Soft delete)
        [HttpPatch("user/hide/{id}")]
        public async Task<ActionResult> hideuser (int id)
        {
            try
            {
                await _userService.hideuser(id);
                return Ok($"User with id {id} removed.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific user in database
        [HttpDelete("user/delete/{id}")]
        public async Task<ActionResult> deleteuser (int id)
        {
            try
            {
                await _userService.deleteuser(id);
                return Ok($"User with id {id} deleted.");
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly RoleService _service;
        public RoleController(RoleService service, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all list of roles with optional filter for role name
        [HttpGet("roles/list")]
        public async Task<ActionResult<List<RoleResponse>>> RolesList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.roleslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific role
        [HttpGet("role/{id}")]
        public async Task<ActionResult<RoleResponse>> GetRole(int id)
        {
            try
            {
                var response = await _service.getrole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new role
        [HttpPost("role")]
        public async Task<ActionResult<RoleResponse>> CreateRole([FromBody] RoleRequest request)
        {
            try
            {
                var response = await _service.createrole(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific role 
        [HttpPatch("role/update/{id}")]
        public async Task<ActionResult<RoleResponse>> UpdateRole([FromBody] RoleRequest request, int id)
        {
            try
            {
                var response = await _service.updaterole(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle active status for specific role
        [HttpPatch("role/toggle-status")]
        public async Task<ActionResult<RoleResponse>> ToggleStatus(int id)
        {
            try
            {
                 var response = await _service.togglestatus(id);
                 return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific role without deleting in database 
        [HttpPatch("role/hide/{id}")]
        public async Task<ActionResult<RoleResponse>>  RemoveRole(int id)
        {
            try
            {
                var response = await _service.removerole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific role in database
        [HttpDelete("role/delete/{id}")]
        public async Task<ActionResult<RoleResponse>> DeleteRole(int id)
        {
            try
            {
                var response = await _service.deleterole(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e) ;
            }
        }
    }
}

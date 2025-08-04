using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers
{
    public class DepartmentController : BaseApiController
    {
        private readonly DepartmentService _service;
        public DepartmentController(DepartmentService service, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all list of departments with optional filter for department name
        [HttpGet("departments/list")]
        public async Task<ActionResult<List<DepartmentResponse>>> DepartmentsList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.departmentslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific department
        [HttpGet("department/{id}")]
        public async Task<ActionResult<DepartmentResponse>> GetDepartment(int id)
        {
            try
            {
                var response = await _service.getdepartment(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new department 
        [HttpPost("department")]
        public async Task<ActionResult<DepartmentResponse>> CreateDepartment([FromBody] DepartmentRequest request)
        {
            try
            {
                var response = await _service.createdepartment(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific department
        [HttpPatch("department/update/{id}")]
        public async Task<ActionResult<DepartmentResponse>> UpdateDepartment([FromBody] DepartmentRequest request, int id)
        {
            try
            {
                var response = await _service.updatedepartment(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific department without remove in database (Soft delete)
        [HttpPatch("department/hide/{id}")]
        public async Task<ActionResult<DepartmentResponse>> RemoveDepartment(int id)
        {
            try
            {
                var response = await _service.removedepartment(id);
                return response;
            }catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific department in database 
        [HttpDelete("department/delete/{id}")]
        public async Task<ActionResult<DepartmentResponse>> DeleteDepartment(int id)
        {
            try
            {
                var response = await _service.deletedepartment(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

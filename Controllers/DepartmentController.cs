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

        [HttpGet("departments/list")]
        public async Task<ActionResult<List<DepartmentResponse>>> departmentslist(string? searchTerm = null)
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

        [HttpGet("department/{id}")]
        public async Task<ActionResult<DepartmentResponse>> getdepartment(int id)
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

        [HttpPost("department")]
        public async Task<ActionResult<DepartmentResponse>> createdepartment([FromBody] DepartmentRequest request)
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

        [HttpPatch("department/update/{id}")]
        public async Task<ActionResult<DepartmentResponse>> updatedepartment([FromBody] DepartmentRequest request, int id)
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

        [HttpPatch("department/hide/{id}")]
        public async Task<ActionResult<DepartmentResponse>> removedepartment(int id)
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

        [HttpDelete("department/delete/{id}")]
        public async Task<ActionResult<DepartmentResponse>> deletedepartment(int id)
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

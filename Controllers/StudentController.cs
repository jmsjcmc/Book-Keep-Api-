using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly StudentService _service;
        public StudentController(AppDbContext context, IMapper mapper, StudentService service) : base (context, mapper)
        {
            _service = service;
        }

        [HttpGet("students/paginated")]
        public async Task<ActionResult<Pagination<StudentResponse>>> paginatedstudents(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedstudents(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("students/list")]
        public async Task<ActionResult<List<StudentResponse>>> studentslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.studentslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet("student/{id}")]
        public async Task<ActionResult<StudentResponse>> getstudent(int id)
        {
            try
            {
                var response = await _service.getstudent(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost("student")]
        public async Task<ActionResult<StudentResponse>> createstudent([FromBody] StudentRequest request)
        {
            try
            {
                var response = await _service.createstudent(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPatch("student/update/{id}")]
        public async Task<ActionResult<StudentResponse>> updatestudent([FromBody] StudentRequest request, int id)
        {
            try
            {
                var response = await _service.updatestudent(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPatch("student/hide/{id}")]
        public async Task<ActionResult<StudentResponse>> hidestudent(int id)
        {
            try
            {
                var response = await _service.removestudent(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete("student/delete/{id}")]
        public async Task<ActionResult<StudentResponse>> deletestudent(int id)
        {
            try
            {
                var response = await _service.deletestudent(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

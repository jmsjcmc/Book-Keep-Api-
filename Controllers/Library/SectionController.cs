using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models.Library;
using Book_Keep.Services.Library;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers.Library
{
    public class SectionController : BaseApiController
    {
        private readonly SectionService _service;
        public SectionController(AppDbContext context, IMapper mapper, SectionService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all sections list with optional filter for section name
        [HttpGet("sections/list")]
        public async Task<ActionResult<List<SectionResponse>>> sectionslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.sectionslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific section
        [HttpGet("section/{id}")]
        public async Task<ActionResult<SectionResponse>> getsection(int id)
        {
            try
            {
                var response = await _service.getsection(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new section
        [HttpPost("section")]
        public async Task<ActionResult<SectionResponse>> createsection([FromBody] SectionRequest request)
        {
            try
            {
                var response = await _service.createsection(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific section
        [HttpPatch("section/update/{id}")]
        public async Task<ActionResult<SectionResponse>> updatesection([FromBody] SectionRequest request, int id)
        {
            try
            {
                var response = await _service.updatesection(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific section without deleting in database (Soft delete)
        [HttpPatch("section/hide/{id}")]
        public async Task<ActionResult<SectionResponse>> removesection(int id)
        {
            try
            {
                var response = await _service.removesection(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific section in database
        [HttpDelete("section/delete/{id}")]
        public async Task<ActionResult<SectionResponse>> deletesection(int id)
        {
            try
            {
                var response = await _service.deletesection(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

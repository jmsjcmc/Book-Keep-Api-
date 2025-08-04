using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models.Library;
using Book_Keep.Services.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers.Library
{
    public class ShelfController : BaseApiController
    {
        private readonly ShelfService _service;
        public ShelfController(AppDbContext context, IMapper mapper, ShelfService service) : base(context, mapper)
        {
            _service = service;
        }
        // Fetch all shelves list with optional filter for shelve label
        [HttpGet("shelves/list")]
        public async Task<ActionResult<List<ShelfResponse>>> ShelvesList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.shelveslist(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific shelve
        [HttpGet("shelve/{id}")]
        public async Task<ActionResult<ShelfResponse>> GetShelve(int id)
        {
            try
            {
                var response = await _service.getshelve(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new shelve
        [HttpPost("shelve")]
        public async Task<ActionResult<ShelfResponse>> CreateShelve([FromBody] ShelfRequest request)
        {
            try
            {
                var response = await _service.createshelve(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific shelve
        [HttpPatch("shelve/update/{id}")]
        public async Task<ActionResult<ShelfResponse>> UpdateShelve([FromBody] ShelfRequest request, int id)
        {
            try
            {
                var response = await _service.updateshelve(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific shelve without removing in database (Soft delete)
        [HttpPatch("shelve/hide/{id}")]
        public async Task<ActionResult<ShelfResponse>> RemoveShelve(int id)
        {
            try
            {
                var response = await _service.removeshelve(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific shelve in database
        [HttpDelete("shelve/delete/{id}")]
        public async Task<ActionResult<ShelfResponse>> DeleteShelve(int id)
        {
            try
            {
                var response = await _service.deleteshelve(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

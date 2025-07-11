using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models.Library;
using Book_Keep.Services.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers.Library
{
    public class ShelfSlotController : BaseApiController
    {
        private readonly ShelfSlotService _service;
        public ShelfSlotController(AppDbContext context, IMapper mapper, ShelfSlotService service) : base(context, mapper)
        {
            _service = service;
        }
        // Fetch all list of shelf slots with optional filter for row or column
        [HttpGet("shelf-slots/list")]
        public async Task<ActionResult<List<ShelfSlotResponse>>> shelfslotslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.shelfslotslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific shelf slot
        [HttpGet("shelf-slot/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> getshelfslot(int id)
        {
            try
            {
                var response = await _service.getshelfslot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new shelf slot
        [HttpPost("shelf-slot")]
        public async Task<ActionResult<ShelfSlotResponse>> createshelfslot([FromBody] ShelfSlotRequest request)
        {
            try
            {
                var response = await _service.createshelfslot(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific shelf slot
        [HttpPatch("shelf-slot/update/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> updateshelfslot([FromBody] ShelfSlotRequest request, int id)
        {
            try
            {
                var response = await _service.updateshelfslot(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific shelf slot without removing in database (Soft delete)
        [HttpPatch("shelf-slot/hide/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> removeshelfslot(int id)
        {
            try
            {
                var response = await _service.removeshelfslot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific shelf slot in database
        [HttpDelete("shelf-slot/delete/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> deleteshelfslot(int id)
        {
            try
            {
                var response = await _service.deleteshelfslot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

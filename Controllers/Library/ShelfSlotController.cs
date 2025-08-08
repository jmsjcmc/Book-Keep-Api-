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
        public async Task<ActionResult<List<ShelfSlotResponse>>> ShelfSlotsList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.ShelfSlotsList(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific shelf slot
        [HttpGet("shelf-slot/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> GetShelfSlot(int id)
        {
            try
            {
                var response = await _service.GetShelfSlot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new shelf slot
        [HttpPost("shelf-slot")]
        public async Task<ActionResult<ShelfSlotResponse>> CreateShelfSlot([FromBody] ShelfSlotRequest request)
        {
            try
            {
                var response = await _service.CreateShelfSlot(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific shelf slot
        [HttpPatch("shelf-slot/update/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> UpdateShelfSlot([FromBody] ShelfSlotRequest request, int id)
        {
            try
            {
                var response = await _service.UpdateShelfSlot(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific shelf slot without removing in database (Soft delete)
        [HttpPatch("shelf-slot/hide/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> RemoveShelfSlot(int id)
        {
            try
            {
                var response = await _service.RemoveShelfSlot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific shelf slot in database
        [HttpDelete("shelf-slot/delete/{id}")]
        public async Task<ActionResult<ShelfSlotResponse>> DeleteShelfSlot(int id)
        {
            try
            {
                var response = await _service.DeleteShelfSlot(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

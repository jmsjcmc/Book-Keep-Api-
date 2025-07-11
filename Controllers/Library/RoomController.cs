using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models.Library;
using Book_Keep.Services.Library;
using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers.Library
{
    public class RoomController : BaseApiController
    {
        private readonly RoomService _service;
        public RoomController(AppDbContext context, IMapper mapper, RoomService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all rooms
        [HttpGet("rooms/list")]
        public async Task<ActionResult<List<RoomResponse>>> roomslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.roomslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific room
        [HttpGet("room/{id}")]
        public async Task<ActionResult<RoomResponse>> getroom(int id)
        {
            try
            {
                var response = await _service.getroom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new room
        [HttpPost("room")]
        public async Task<ActionResult<RoomResponse>> createroom([FromBody] RoomRequest request)
        {
            try
            {
                var response = await _service.createroom(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific room 
        [HttpPatch("room/update/{id}")]
        public async Task<ActionResult<RoomResponse>> updateroom([FromBody] RoomRequest request, int id)
        {
            try
            {
                var response = await _service.updateroom(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific room with deleting in database (Soft Delete)
        [HttpPatch("room/hide/{id}")]
        public async Task<ActionResult<RoomResponse>> removeroom(int id)
        {
            try
            {
                var response = await _service.removeroom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific room from database
        [HttpDelete("room/delete/{id}")]
        public async Task<ActionResult<RoomResponse>> deleteroom(int id)
        {
            try
            {
                var response = await _service.deleteroom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

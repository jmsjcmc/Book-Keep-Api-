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
        public async Task<ActionResult<List<RoomResponse>>> RoomsList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.RoomsList(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific room
        [HttpGet("room/{id}")]
        public async Task<ActionResult<RoomResponse>> GetRoom(int id)
        {
            try
            {
                var response = await _service.GetRoom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new room
        [HttpPost("room")]
        public async Task<ActionResult<RoomResponse>> CreateRoom([FromBody] RoomRequest request)
        {
            try
            {
                var response = await _service.CreateRoom(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific room 
        [HttpPatch("room/update/{id}")]
        public async Task<ActionResult<RoomResponse>> UpdateRoom([FromBody] RoomRequest request, int id)
        {
            try
            {
                var response = await _service.UpdateRoom(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific room with deleting in database (Soft Delete)
        [HttpPatch("room/hide/{id}")]
        public async Task<ActionResult<RoomResponse>> RemoveRoom(int id)
        {
            try
            {
                var response = await _service.RemoveRoom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific room from database
        [HttpDelete("room/delete/{id}")]
        public async Task<ActionResult<RoomResponse>> DeleteRoom(int id)
        {
            try
            {
                var response = await _service.DeleteRoom(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

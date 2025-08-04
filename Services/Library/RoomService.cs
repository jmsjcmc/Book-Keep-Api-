using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Library;
using Book_Keep.Interfaces.Library;
using Book_Keep.Models.Library;

namespace Book_Keep.Services.Library
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly RoomQueries _query;
        public RoomService(AppDbContext context, IMapper mapper, RoomQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("rooms/list")]
        public async Task<List<RoomResponse>> RoomsList(string? searchTerm = null)
        {
            var rooms = await _query.roomslist(searchTerm);
            return _mapper.Map<List<RoomResponse>>(rooms);
        }
        // [HttpGet("room/{id}")]
        public async Task<RoomResponse> GetRoom(int id)
        {
            var room = await GetRoomId(id);
            return _mapper.Map<RoomResponse>(room);
        }
        // [HttpPost("room")]
        public async Task<RoomResponse> CreateRoom(RoomRequest request)
        {
            var room = _mapper.Map<Room>(request);

            _context.Room.Add(room);
            await _context.SaveChangesAsync();  

            return await RoomResponse(room.Id);
        }
        // [HttpPatch("room/update/{id}")]
        public async Task<RoomResponse> UpdateRoom(RoomRequest request, int id)
        {
            var room = await PatchRoomId(id);

            _mapper.Map(request, room); 

            await _context.SaveChangesAsync();

            return await RoomResponse(room.Id);
        }
        // [HttpPatch("room/hide/{id}")]
        public async Task<RoomResponse> RemoveRoom(int id)
        {
            var room = await PatchRoomId(id);

            room.Removed = true;

            _context.Room.Update(room);
            await _context.SaveChangesAsync();

            return await RoomResponse(room.Id);
        }
        // [HttpDelete("room/delete/{id}")]
        public async Task<RoomResponse> DeleteRoom(int id)
        {
            var room = await PatchRoomId(id);
            
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return await RoomResponse(room.Id);
        }
        // Helpers
        private async Task<Room?> GetRoomId(int id)
        {
            return await _query.getroomid(id);
        }
        private async Task<Room?> PatchRoomId(int id)
        {
            return await _query.patchroomid(id);
        }
        private async Task<RoomResponse> RoomResponse(int id)
        {
            var response = await GetRoomId(id);
            return _mapper.Map<RoomResponse>(response);
        }
    }
}

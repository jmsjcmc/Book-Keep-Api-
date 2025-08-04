using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IRoomService
    {
        Task<List<RoomResponse>> RoomsList(string? searchTerm = null);
        Task<RoomResponse> GetRoom(int id);
        Task<RoomResponse> CreateRoom(RoomRequest request);
        Task<RoomResponse> UpdateRoom(RoomRequest request, int id);
        Task<RoomResponse> RemoveRoom(int id);
        Task<RoomResponse> DeleteRoom(int id);
    }
}

using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IRoomService
    {
        Task<List<RoomResponse>> roomslist(string? searchTerm = null);
        Task<RoomResponse> getroom(int id);
        Task<RoomResponse> createroom(RoomRequest request);
        Task<RoomResponse> updateroom(RoomRequest request, int id);
        Task<RoomResponse> removeroom(int id);
        Task<RoomResponse> deleteroom(int id);
    }
}

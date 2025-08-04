using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IShelfSlotService
    {
        Task<List<ShelfSlotResponse>> ShelfSlotsList(string? searchTerm = null);
        Task<ShelfSlotResponse> GetShelfSlot(int id);
        Task<ShelfSlotResponse> CreateShelfSlot(ShelfSlotRequest request);
        Task<ShelfSlotResponse> UpdateShelfSlot(ShelfSlotRequest request, int id);
        Task<ShelfSlotResponse> RemoveShelfSlot(int id);
        Task<ShelfSlotResponse> DeleteShelfSlot(int id);
    }
}

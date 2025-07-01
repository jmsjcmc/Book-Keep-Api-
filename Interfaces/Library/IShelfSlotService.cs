using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IShelfSlotService
    {
        Task<List<ShelfSlotResponse>> shelfslotslist(string? searchTerm = null);
        Task<ShelfSlotResponse> getshelfslot(int id);
        Task<ShelfSlotResponse> createshelfslot(ShelfSlotRequest request);
        Task<ShelfSlotResponse> updateshelfslot(ShelfSlotRequest request, int id);
        Task<ShelfSlotResponse> removeshelfslot(int id);
        Task<ShelfSlotResponse> deleteshelfslot(int id);
    }
}

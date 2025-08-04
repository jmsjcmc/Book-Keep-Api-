using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IShelfService
    {
        Task<List<ShelfResponse>> ShelvesList(string? searchTerm = null);
        Task<ShelfResponse> GetShelve(int id);
        Task<ShelfResponse> CreateShelve(ShelfRequest request);
        Task<ShelfResponse> UpdateShelve(ShelfRequest request, int id);
        Task<ShelfResponse> RemoveShelve(int id);
        Task<ShelfResponse> DeleteShelve(int id);
    }
}

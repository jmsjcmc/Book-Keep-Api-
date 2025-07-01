using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface IShelfService
    {
        Task<List<ShelfResponse>> shelveslist(string? searchTerm = null);
        Task<ShelfResponse> getshelve(int id);
        Task<ShelfResponse> createshelve(ShelfRequest request);
        Task<ShelfResponse> updateshelve(ShelfRequest request, int id);
        Task<ShelfResponse> removeshelve(int id);
        Task<ShelfResponse> deleteshelve(int id);
    }
}

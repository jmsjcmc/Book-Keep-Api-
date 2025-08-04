using Book_Keep.Models;
using Book_Keep.Models.Canteen;
using DocumentFormat.OpenXml.Math;

namespace Book_Keep.Interfaces.Canteen
{
    public interface IProductService
    {
        Task<Pagination<ProductResponse>> PaginatedProducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<ProductResponse>> ProductsList(string? searchTerm = null);
        Task<ProductResponse> GetProduct(int id);
        Task<ProductResponse> CreateProduct(ProductRequest request);
        Task<ProductResponse> UpdateProduct(ProductRequest request, int id);
        Task<ProductResponse> ToggleStatus(int id);
        Task<ProductResponse> RemoveProduct(int id);
        Task<ProductResponse> DeleteProduct(int id);
    }
}

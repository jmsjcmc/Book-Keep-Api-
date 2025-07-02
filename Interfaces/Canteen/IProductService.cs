using Book_Keep.Models;
using Book_Keep.Models.Canteen;
using DocumentFormat.OpenXml.Math;

namespace Book_Keep.Interfaces.Canteen
{
    public interface IProductService
    {
        Task<Pagination<ProductResponse>> paginatedproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null);
        Task<List<ProductResponse>> productslist(string? searchTerm = null);
        Task<ProductResponse> getproduct(int id);
        Task<ProductResponse> createproduct(ProductRequest request);
        Task<ProductResponse> updateproduct(ProductRequest request, int id);
        Task<ProductResponse> togglestatus(int id);
        Task<ProductResponse> removeproduct(int id);
        Task<ProductResponse> deleteproduct(int id);
    }
}

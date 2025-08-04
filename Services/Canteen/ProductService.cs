using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Canteen;
using Book_Keep.Interfaces.Canteen;
using Book_Keep.Models;
using Book_Keep.Models.Canteen;

namespace Book_Keep.Services.Canteen
{
    public class ProductService : BaseService, IProductService
    {
        private readonly ProductQueries _query;
        public ProductService(AppDbContext context, IMapper mapper, ProductQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("products/paginated")]
        public async Task<Pagination<ProductResponse>> PaginatedProducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedproducts(searchTerm);
            return await PaginationHelper.paginateandmap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }
        // [HttpGet("products/list")]
        public async Task<List<ProductResponse>> ProductsList(string? searchTerm = null)
        {
            var products = await _query.productslist(searchTerm);
            return _mapper.Map<List<ProductResponse>>(products);
        }
        // [HttpGet("product/{id}")]
        public async Task<ProductResponse> GetProduct(int id)
        {
            var product = await GetProductId(id);

            return _mapper.Map<ProductResponse>(product);
        }
        // [HttpPost("product")]
        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpPatch("product/update/{id}")]
        public async Task<ProductResponse> UpdateProduct(ProductRequest request, int id)
        {
            var product = await PatchProductId(id);

            _mapper.Map(request, product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpPatch("product/toggle-status")]
        public async Task<ProductResponse> ToggleStatus(int id)
        {
            var product = await PatchProductId(id);

            product.Active = !product.Active;

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpPatch("product/hide/{id}")]
        public async Task<ProductResponse> RemoveProduct(int id)
        {
            var product = await PatchProductId(id);

            product.Removed = true;

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // [HttpDelete("product/delete/{id}")]
        public async Task<ProductResponse> DeleteProduct(int id)
        {
            var product = await PatchProductId(id);

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return await ProductResponse(product.Id);
        }
        // Helpers
        private async Task<Product?> GetProductId(int id)
        {
            return await _query.getproductid(id);
        }
        private async Task<Product?> PatchProductId(int id)
        {
            return await _query.patchproductid(id);
        }
        private async Task<ProductResponse> ProductResponse(int id)
        {
            var response = await GetProductId(id);
            return _mapper.Map<ProductResponse>(response);
        }
    }
}

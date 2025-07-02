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

        public async Task<Pagination<ProductResponse>> paginatedproducts(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedproducts(searchTerm);
            return await PaginationHelper.paginateandmap<Product, ProductResponse>(query, pageNumber, pageSize, _mapper);
        }

        public async Task<List<ProductResponse>> productslist(string? searchTerm = null)
        {
            var products = await _query.productslist(searchTerm);
            return _mapper.Map<List<ProductResponse>>(products);
        } 

        public async Task<ProductResponse> getproduct(int id)
        {
            var product = await getproductid(id);

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> createproduct(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }

        public async Task<ProductResponse> updateproduct(ProductRequest request, int id)
        {
            var product = await patchproductid(id);

            _mapper.Map(request, product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }

        public async Task<ProductResponse> togglestatus(int id)
        {
            var product = await patchproductid(id);

            product.Active = !product.Active;

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }

        public async Task<ProductResponse> removeproduct(int id)
        {
            var product = await patchproductid(id);

            product.Removed = true;

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }

        public async Task<ProductResponse> deleteproduct(int id)
        {
            var product = await patchproductid(id);

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return await productResponse(product.Id);
        }
        private async Task<Product?> getproductid(int id)
        {
            return await _query.getproductid(id);
        }
        private async Task<Product?> patchproductid(int id)
        {
            return await _query.patchproductid(id);
        }
        private async Task<ProductResponse> productResponse(int id)
        {
            var response = await getproductid(id);
            return _mapper.Map<ProductResponse>(response);
        }
    }
}

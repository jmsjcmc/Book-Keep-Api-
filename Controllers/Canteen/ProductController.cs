using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Models;
using Book_Keep.Models.Canteen;
using Book_Keep.Services.Canteen;

using Microsoft.AspNetCore.Mvc;

namespace Book_Keep.Controllers.Canteen
{
    public class ProductController : BaseApiController
    {
        private readonly ProductService _service;
        public ProductController(AppDbContext context, IMapper mapper, ProductService service) : base(context, mapper)
        {
            _service = service;
        }
        // Fetch all products with optional filter for product name (Paginated) 
        [HttpGet("products/paginated")]
        public async Task<ActionResult<Pagination<ProductResponse>>> PaginatedProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.PaginatedProducts(pageNumber, pageSize, searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products with optional filter for product name (List)
        [HttpGet("products/list")]
        public async Task<ActionResult<List<ProductResponse>>> ProductsList(string? searchTerm = null)
        {
            try
            {
                var response = await _service.ProductsList(searchTerm);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific product
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            try
            {
                var response = await _service.GetProduct(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new product
        [HttpPost("product")]
        public async Task<ActionResult<ProductResponse>> CreateProduct([FromBody] ProductRequest request)
        {
            try
            {
                var response = await _service.CreateProduct(request);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Update specific product
        [HttpPatch("product/update/{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct([FromBody] ProductRequest request, int id)
        {
            try
            {
                var response = await _service.UpdateProduct(request, id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle active status for specific product
        [HttpPatch("product/toggle-status")]
        public async Task<ActionResult<ProductResponse>> ToggleStatus(int id)
        {
            try
            {
                var response = await _service.ToggleStatus(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific product without deleting in database (Soft Delete)
        [HttpPatch("product/hide/{id}")]
        public async Task<ActionResult<ProductResponse>> HideProduct(int id)
        {
            try
            {
                var response = await _service.RemoveProduct(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific product in database
        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(int id)
        {
            try
            {
                var response = await _service.DeleteProduct(id);
                return response;
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

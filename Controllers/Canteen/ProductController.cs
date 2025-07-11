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
        public ProductController(AppDbContext context, IMapper mapper, ProductService service) : base (context, mapper)
        {
            _service = service;
        }
        // Fetch all products with optional filter for product name (Paginated) 
        [HttpGet("products/paginated")]
        public async Task<ActionResult<Pagination<ProductResponse>>> paginatedproducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var response = await _service.paginatedproducts(pageNumber, pageSize, searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch all products with optional filter for product name (List)
        [HttpGet("products/list")]
        public async Task<ActionResult<List<ProductResponse>>> productslist(string? searchTerm = null)
        {
            try
            {
                var response = await _service.productslist(searchTerm);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Fetch specific product
        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductResponse>> getproduct(int id)
        {
            try
            {
                var response = await _service.getproduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Create new product
        [HttpPost("product")]
        public async Task<ActionResult<ProductResponse>> createproduct([FromBody] ProductRequest request)
        {
            try
            {
                var response = await _service.createproduct(request);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);  
            }
        }
        // Update specific product
        [HttpPatch("product/update/{id}")]
        public async Task<ActionResult<ProductResponse>> updateproduct([FromBody] ProductRequest request, int id)
        {
            try
            {
                var response = await _service.updateproduct(request, id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Toggle active status for specific product
        [HttpPatch("product/toggle-status")]
        public async Task<ActionResult<ProductResponse>> togglestatus(int id)
        {
            try
            {
                var response = await _service.togglestatus(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Remove specific product without deleting in database (Soft Delete)
        [HttpPatch("product/hide/{id}")]
        public async Task<ActionResult<ProductResponse>> hideproduct(int id)
        {
            try
            {
                var response = await _service.removeproduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
        // Delete specific product in database
        [HttpDelete("product/delete/{id}")]
        public async Task<ActionResult<ProductResponse>> deleteproduct(int id)
        {
            try
            {
                var response = await _service.deleteproduct(id);
                return response;
            } catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}

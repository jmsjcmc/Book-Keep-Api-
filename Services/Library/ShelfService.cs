using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Library;
using Book_Keep.Interfaces.Library;
using Book_Keep.Models.Library;

namespace Book_Keep.Services.Library
{
    public class ShelfService : BaseService, IShelfService
    {
        private readonly ShelfQueries _query;
        public ShelfService(AppDbContext context, IMapper mapper, ShelfQueries query) : base(context, mapper)
        {
            _query = query;
        }
        // [HttpGet("shelves/list")]
        public async Task<List<ShelfResponse>> shelveslist(string? searchTerm = null)
        {
            var shelves = await _query.shelveslist(searchTerm);
            return _mapper.Map<List<ShelfResponse>>(shelves);
        }
        // [HttpGet("shelve/{id}")]
        public async Task<ShelfResponse> getshelve(int id)
        {
            var shelve = await getshelfid(id);
            return _mapper.Map<ShelfResponse>(shelve);
        }
        // [HttpPost("shelve")]
        public async Task<ShelfResponse> createshelve(ShelfRequest request)
        {
            var shelve = _mapper.Map<Shelf>(request);   

            _context.Shelf.Add(shelve);
            await _context.SaveChangesAsync();

            return await shelfResponse(shelve.Id);
        }
        // [HttpPatch("shelve/update/{id}")]
        public async Task<ShelfResponse> updateshelve(ShelfRequest request, int id)
        {
            var shelve = await patchshelfid(id);

            _mapper.Map(request, shelve);

            await _context.SaveChangesAsync();

            return await shelfResponse(shelve.Id);
        }
        // [HttpPatch("shelve/hide/{id}")]
        public async Task<ShelfResponse> removeshelve(int id)
        {
            var shelve = await patchshelfid(id);

            shelve.Removed = true;

            _context.Shelf.Update(shelve);
            await _context.SaveChangesAsync();

            return await shelfResponse(shelve.Id);
        }
        // [HttpDelete("shelve/delete/{id}")]
        public async Task<ShelfResponse> deleteshelve(int id)
        {
            var shelve = await patchshelfid(id);

            _context.Shelf.Remove(shelve);
            await _context.SaveChangesAsync();

            return await shelfResponse(shelve.Id);
        }
        // Helpers
        private async Task<Shelf?> getshelfid(int id)
        {
            return await _query.getshelfid(id);
        }
        private async Task<Shelf?> patchshelfid(int id)
        {
            return await _query.patchshelfid(id);
        }
        private async Task<ShelfResponse> shelfResponse(int id)
        {
            var response = await getshelfid(id);
            return _mapper.Map<ShelfResponse>(response);
        }
    }
}

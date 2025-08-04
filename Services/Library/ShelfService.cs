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
        public async Task<List<ShelfResponse>> ShelvesList(string? searchTerm = null)
        {
            var shelves = await _query.shelveslist(searchTerm);
            return _mapper.Map<List<ShelfResponse>>(shelves);
        }
        // [HttpGet("shelve/{id}")]
        public async Task<ShelfResponse> GetShelve(int id)
        {
            var shelve = await GetShelfId(id);
            return _mapper.Map<ShelfResponse>(shelve);
        }
        // [HttpPost("shelve")]
        public async Task<ShelfResponse> CreateShelve(ShelfRequest request)
        {
            var shelve = _mapper.Map<Shelf>(request);   

            _context.Shelf.Add(shelve);
            await _context.SaveChangesAsync();

            return await ShelfResponse(shelve.Id);
        }
        // [HttpPatch("shelve/update/{id}")]
        public async Task<ShelfResponse> UpdateShelve(ShelfRequest request, int id)
        {
            var shelve = await PatchShelfId(id);

            _mapper.Map(request, shelve);

            await _context.SaveChangesAsync();

            return await ShelfResponse(shelve.Id);
        }
        // [HttpPatch("shelve/hide/{id}")]
        public async Task<ShelfResponse> RemoveShelve(int id)
        {
            var shelve = await PatchShelfId(id);

            shelve.Removed = true;

            _context.Shelf.Update(shelve);
            await _context.SaveChangesAsync();

            return await ShelfResponse(shelve.Id);
        }
        // [HttpDelete("shelve/delete/{id}")]
        public async Task<ShelfResponse> DeleteShelve(int id)
        {
            var shelve = await PatchShelfId(id);

            _context.Shelf.Remove(shelve);
            await _context.SaveChangesAsync();

            return await ShelfResponse(shelve.Id);
        }
        // Helpers
        private async Task<Shelf?> GetShelfId(int id)
        {
            return await _query.getshelfid(id);
        }
        private async Task<Shelf?> PatchShelfId(int id)
        {
            return await _query.patchshelfid(id);
        }
        private async Task<ShelfResponse> ShelfResponse(int id)
        {
            var response = await GetShelfId(id);
            return _mapper.Map<ShelfResponse>(response);
        }
    }
}

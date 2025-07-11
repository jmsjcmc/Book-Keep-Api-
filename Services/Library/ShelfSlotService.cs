using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Library;
using Book_Keep.Interfaces.Library;
using Book_Keep.Models.Library;

namespace Book_Keep.Services.Library
{
    public class ShelfSlotService : BaseService, IShelfSlotService
    {
        private readonly ShelfSlotQueries _query;
        public ShelfSlotService(AppDbContext context, IMapper mapper, ShelfSlotQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("shelf-slots/list")]
        public async Task<List<ShelfSlotResponse>> shelfslotslist(string? searchTerm = null)
        {
            var slots = await _query.shelfslotslist(searchTerm);
            return _mapper.Map<List<ShelfSlotResponse>>(slots);
        }
        // [HttpGet("shelf-slot/{id}")]
        public async Task<ShelfSlotResponse> getshelfslot(int id)
        {
            var slot = await getshelfslotid(id);
            return _mapper.Map<ShelfSlotResponse>(slot);
        }
        // [HttpPost("shelf-slot")]
        public async Task<ShelfSlotResponse> createshelfslot(ShelfSlotRequest request)
        {
            var slot = _mapper.Map<ShelfSlot>(request);

            _context.ShelfSlot.Add(slot);
            await _context.SaveChangesAsync();

            return await shelfslotResponse(slot.Id);
        }
        // [HttpPatch("shelf-slot/update/{id}")]
        public async Task<ShelfSlotResponse> updateshelfslot(ShelfSlotRequest request, int id)
        {
            var slot = await patchshelfslotid(id);

            _mapper.Map(request, slot);

            _context.ShelfSlot.Update(slot);
            await _context.SaveChangesAsync();

            return await shelfslotResponse(slot.Id);
        }
        // [HttpPatch("shelf-slot/hide/{id}")]
        public async Task<ShelfSlotResponse> removeshelfslot(int id)
        {
            var slot = await patchshelfslotid(id);

            slot.Removed = true;

            _context.ShelfSlot.Update(slot);
            await _context.SaveChangesAsync();

            return await shelfslotResponse(slot.Id);
        }
        // [HttpDelete("shelf-slot/delete/{id}")]
        public async Task<ShelfSlotResponse> deleteshelfslot(int id)
        {
            var slot = await patchshelfslotid(id);

            _context.ShelfSlot.Remove(slot);
            await _context.SaveChangesAsync();

            return await shelfslotResponse(slot.Id);
        }
        // Helpers
        private async Task<ShelfSlot?> getshelfslotid(int id)
        {
            return await _query.getshelfslotid(id);
        }
        private async Task<ShelfSlot?> patchshelfslotid(int id)
        {
            return await _query.patchshelfslotid(id);
        }
        private async Task<ShelfSlotResponse> shelfslotResponse(int id)
        {
            var response = await getshelfslotid(id);
            return _mapper.Map<ShelfSlotResponse>(response);
        }
    }
}

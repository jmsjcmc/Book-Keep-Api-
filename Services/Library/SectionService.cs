using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries.Library;
using Book_Keep.Interfaces.Library;
using Book_Keep.Models.Library;

namespace Book_Keep.Services.Library
{
    public class SectionService : BaseService, ISectionService
    {
        private readonly SectionQueries _query;
        public SectionService(AppDbContext context, IMapper mapper, SectionQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("sections/list")]
        public async Task<List<SectionResponse>> sectionslist(string? searchTerm = null)
        {
            var sections = await _query.sectionslist(searchTerm);
            return _mapper.Map<List<SectionResponse>>(sections);
        }
        // [HttpGet("section/{id}")]
        public async Task<SectionResponse> getsection(int id)
        {
            var section = await getsectionid(id);
            return _mapper.Map<SectionResponse>(section);
        }
        // [HttpPost("section")]
        public async Task<SectionResponse> createsection(SectionRequest request)
        {
            var section = _mapper.Map<Section>(request);

            _context.Section.Add(section);
            await _context.SaveChangesAsync();

            return await sectionResponse(section.Id);   
        }
        // [HttpPatch("section/update/{id}")]
        public async Task<SectionResponse> updatesection(SectionRequest request, int id)
        {
            var section = await patchsectionid(id);

            _mapper.Map(request, section);

            await _context.SaveChangesAsync();

            return await sectionResponse(section.Id);
        }
        // [HttpPatch("section/hide/{id}")]
        public async Task<SectionResponse> removesection(int id)
        {
            var section = await patchsectionid(id);

            section.Removed = true;

            _context.Section.Update(section);
            await _context.SaveChangesAsync();

            return await sectionResponse(section.Id);
        }
        // [HttpDelete("section/delete/{id}")]
        public async Task<SectionResponse> deletesection(int id)
        {
            var section = await patchsectionid(id);

            _context.Section.Remove(section);
            await _context.SaveChangesAsync();

            return await sectionResponse(section.Id);
        }
        // Helpers
        private async Task<Section?> getsectionid(int id)
        {
            return await _query.getsectionid(id);
        }
        private async Task<Section?> patchsectionid(int id)
        {
            return await _query.patchsectionid(id);
        }
        private async Task<SectionResponse> sectionResponse(int id)
        {
            var response = await getsectionid(id);
            return _mapper.Map<SectionResponse>(response);
        }
    }
}

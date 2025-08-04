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
        public async Task<List<SectionResponse>> SectionsList(string? searchTerm = null)
        {
            var sections = await _query.sectionslist(searchTerm);
            return _mapper.Map<List<SectionResponse>>(sections);
        }
        // [HttpGet("section/{id}")]
        public async Task<SectionResponse> GetSection(int id)
        {
            var section = await GetSectionId(id);
            return _mapper.Map<SectionResponse>(section);
        }
        // [HttpPost("section")]
        public async Task<SectionResponse> CreateSection(SectionRequest request)
        {
            var section = _mapper.Map<Section>(request);

            _context.Section.Add(section);
            await _context.SaveChangesAsync();

            return await SectionResponse(section.Id);   
        }
        // [HttpPatch("section/update/{id}")]
        public async Task<SectionResponse> UpdateSection(SectionRequest request, int id)
        {
            var section = await PatchSectionId(id);

            _mapper.Map(request, section);

            await _context.SaveChangesAsync();

            return await SectionResponse(section.Id);
        }
        // [HttpPatch("section/hide/{id}")]
        public async Task<SectionResponse> RemoveSection(int id)
        {
            var section = await PatchSectionId(id);

            section.Removed = true;

            _context.Section.Update(section);
            await _context.SaveChangesAsync();

            return await SectionResponse(section.Id);
        }
        // [HttpDelete("section/delete/{id}")]
        public async Task<SectionResponse> DeleteSection(int id)
        {
            var section = await PatchSectionId(id);

            _context.Section.Remove(section);
            await _context.SaveChangesAsync();

            return await SectionResponse(section.Id);
        }
        // Helpers
        private async Task<Section?> GetSectionId(int id)
        {
            return await _query.getsectionid(id);
        }
        private async Task<Section?> PatchSectionId(int id)
        {
            return await _query.patchsectionid(id);
        }
        private async Task<SectionResponse> SectionResponse(int id)
        {
            var response = await GetSectionId(id);
            return _mapper.Map<SectionResponse>(response);
        }
    }
}

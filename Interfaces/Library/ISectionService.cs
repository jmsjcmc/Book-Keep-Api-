using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface ISectionService
    {
        Task<List<SectionResponse>> SectionsList(string? searchTerm = null);
        Task<SectionResponse> GetSection(int id);
        Task<SectionResponse> CreateSection(SectionRequest request);
        Task<SectionResponse> UpdateSection(SectionRequest request, int id);
        Task<SectionResponse> RemoveSection(int id);
        Task<SectionResponse> DeleteSection(int id);
    }
}

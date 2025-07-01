using Book_Keep.Models.Library;

namespace Book_Keep.Interfaces.Library
{
    public interface ISectionService
    {
        Task<List<SectionResponse>> sectionslist(string? searchTerm = null);
        Task<SectionResponse> getsection(int id);
        Task<SectionResponse> createsection(SectionRequest request);
        Task<SectionResponse> updatesection(SectionRequest request, int id);
        Task<SectionResponse> removesection(int id);
        Task<SectionResponse> deletesection(int id);
    }
}

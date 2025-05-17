using AutoMapper;
using Book_Keep.Models;

namespace Book_Keep.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // Book Request
            CreateMap<BookRequest, Book>()
                .ForMember(d => d.AddedOn, o => o.Ignore());
            // Book Response
            CreateMap<Book, BookResponse>();
        }
    }
}

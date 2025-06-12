using AutoMapper;
using Book_Keep.Models.Book;

namespace Book_Keep.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
           
            CreateMap<BookRequest, Book>()
                .ForMember(d => d.AddedOn, o => o.Ignore());
            CreateMap<Book, BookResponse>();
        }
    }
}

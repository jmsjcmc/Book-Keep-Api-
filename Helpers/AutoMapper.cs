using AutoMapper;
using Book_Keep.Models.Book;
using Book_Keep.Models.User;

namespace Book_Keep.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
           // Books Mapping
            CreateMap<BookRequest, Book>()
                .ForMember(d => d.AddedOn, o => o.Ignore());

            CreateMap<Book, BookResponse>();
            // Users Mapping
            CreateMap<UserRequest, User>();

            CreateMap<User, UserResponse>();
            // Departments Mapping
            CreateMap<DepartmentRequest, Department>();

            CreateMap<Department, DepartmentResponse>();
        }
    }
}

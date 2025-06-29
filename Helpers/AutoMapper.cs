using AutoMapper;
using Book_Keep.Models;

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

            CreateMap<User, UserWithDepartmentResponse>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.DepartmentName));
            // Departments Mapping
            CreateMap<DepartmentRequest, Department>();

            CreateMap<Department, DepartmentResponse>();
        }
    }
}

using AutoMapper;
using Book_Keep.Models;
using Book_Keep.Models.Canteen;
using Book_Keep.Models.Library;

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
            // Room Mapping
            CreateMap<RoomRequest, Room>();

            CreateMap<Room, RoomResponse>();
            // Section Mapping
            CreateMap<SectionRequest, Section>();

            CreateMap<Section, SectionResponse>()
                .ForMember(d => d.Room, o => o.MapFrom(s => s.Room.Name));
            // Shelf Mapping
            CreateMap<ShelfRequest, Shelf>();

            CreateMap<Shelf, ShelfResponse>()
                .ForMember(d => d.Section, o => o.MapFrom(s => s.Section.Name));

            // Shelf Slot Mapping
            CreateMap<ShelfSlotRequest, ShelfSlot>();

            CreateMap<ShelfSlot, ShelfSlotResponse>()
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Shelf.Label));
            // Product Mapping
            CreateMap<ProductRequest, Product>();

            CreateMap<Product, ProductResponse>();
            // Student Mapping 
            CreateMap<StudentRequest, Student>();

            CreateMap<Student, StudentResponse>();
        }
    }
}

using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;

namespace Book_Keep.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly StudentQueries _query;
        public StudentService(AppDbContext context, IMapper mapper, StudentQueries query) : base(context, mapper)
        {
            _query = query;
        }
        public async Task<Pagination<StudentResponse>> paginatedstudents(
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null)
        {
            var query = _query.paginatedstudents(searchTerm);
            return await PaginationHelper.paginateandmap<Student, StudentResponse>(query, pageNumber, pageSize, _mapper);
        }

        public async Task<List<StudentResponse>> studentslist(string? searchTerm = null)
        {
            var students = await _query.studentslist(searchTerm);
            return _mapper.Map<List<StudentResponse>>(students);
        }

        public async Task<StudentResponse> getstudent(int id)
        {
            var student = await getstudentid(id);
            return _mapper.Map<StudentResponse>(student);
        }

        public async Task<StudentResponse> createstudent(StudentRequest request)
        {
            var student = _mapper.Map<Student>(request);

            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return await studentResponse(student.Id);
        }

        public async Task<StudentResponse> updatestudent(StudentRequest request, int id)
        {
            var student = await patchstudentid(id);

            _mapper.Map(request, student);

            await _context.SaveChangesAsync();

            return await studentResponse(student.Id);
        }

        public async Task<StudentResponse> removestudent(int id)
        {
            var student = await patchstudentid(id);

            student.Removed = true;

            _context.Student.Update(student);
            await _context.SaveChangesAsync();

            return await studentResponse(student.Id);
        }

        public async Task<StudentResponse> deletestudent(int id)
        {
            var student = await patchstudentid(id);

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return await studentResponse(student.Id);
        }
        private async Task<Student?> getstudentid(int id)
        {
            return await _query.getstudentid(id);
        }
        private async Task<Student?> patchstudentid(int id)
        {
            return await _query.patchstudentid(id);
        }
        private async Task<StudentResponse> studentResponse(int id)
        {
            var response = await getstudentid(id);
            return _mapper.Map<StudentResponse>(response);
        }
    }
}

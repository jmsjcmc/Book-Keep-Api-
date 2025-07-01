using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models.Department;
using Book_Keep.Models.Department.Department;

namespace Book_Keep.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly DepartmentQueries _query;
        public DepartmentService(DepartmentQueries query, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
        }
        public async Task<List<DepartmentResponse>> departmentslist(string? searchTerm = null)
        {
            var departments = await _query.departmentslist(searchTerm);
            return _mapper.Map<List<DepartmentResponse>>(departments);
        }

        public async Task<DepartmentResponse> getdepartment(int id)
        {
            var department = await getdepartmentid(id);
            return _mapper.Map<DepartmentResponse>(department);
        }

        public async Task<DepartmentResponse> createdepartment(DepartmentRequest request)
        {
            var department = _mapper.Map<Department>(request);
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return await departmentResponse(department.Id);
        }

        public async Task<DepartmentResponse> updatedepartment(DepartmentRequest request, int id)
        {
            var department = await patchdepartmentid(id);

            _mapper.Map(request, department);
            await _context.SaveChangesAsync();

            return await departmentResponse(department.Id);
        }

        public async Task<DepartmentResponse> removedepartment(int id)
        {
            var department = await patchdepartmentid(id);

            department.Removed = true;

            _context.Department.Update(department);
            await _context.SaveChangesAsync();

            return await departmentResponse(department.Id);
        }

        public async Task<DepartmentResponse> deletedepartment(int id)
        {
            var department = await patchdepartmentid(id);

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return await departmentResponse(department.Id);
        }
        private async Task<Department?> getdepartmentid(int id)
        {
            return await _query.getdepartmentid(id);
        }

        private async Task<Department?> patchdepartmentid(int id)
        {
            return await _query.patchdepartmentid(id);
        }
        private async Task<DepartmentResponse> departmentResponse(int id)
        {
            var response = await getdepartmentid(id);
            return _mapper.Map<DepartmentResponse>(response);
        }
    }
}

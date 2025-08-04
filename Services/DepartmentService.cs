using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;

namespace Book_Keep.Services
{
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly DepartmentQueries _query;
        public DepartmentService(DepartmentQueries query, AppDbContext context, IMapper mapper) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("departments/list")]
        public async Task<List<DepartmentResponse>> DepartmentsList(string? searchTerm = null)
        {
            var departments = await _query.departmentslist(searchTerm);
            return _mapper.Map<List<DepartmentResponse>>(departments);
        }
        // [HttpGet("department/{id}")]
        public async Task<DepartmentResponse> GetDepartment(int id)
        {
            var department = await GetDepartmentId(id);
            return _mapper.Map<DepartmentResponse>(department);
        }
        // [HttpPost("department")]
        public async Task<DepartmentResponse> CreateDepartment(DepartmentRequest request)
        {
            var department = _mapper.Map<Department>(request);
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return await DepartmentResponse(department.Id);
        }
        // [HttpPatch("department/update/{id}")]
        public async Task<DepartmentResponse> UpdateDepartment(DepartmentRequest request, int id)
        {
            var department = await PatchDepartmentId(id);

            _mapper.Map(request, department);
            await _context.SaveChangesAsync();

            return await DepartmentResponse(department.Id);
        }
        // [HttpPatch("department/hide/{id}")]
        public async Task<DepartmentResponse> RemoveDepartment(int id)
        {
            var department = await PatchDepartmentId(id);

            department.Removed = true;

            _context.Department.Update(department);
            await _context.SaveChangesAsync();

            return await DepartmentResponse(department.Id);
        }
        // [HttpDelete("department/delete/{id}")]
        public async Task<DepartmentResponse> DeleteDepartment(int id)
        {
            var department = await PatchDepartmentId(id);

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return await DepartmentResponse(department.Id);
        }
        // Helpers
        private async Task<Department?> GetDepartmentId(int id)
        {
            return await _query.getdepartmentid(id);
        }

        private async Task<Department?> PatchDepartmentId(int id)
        {
            return await _query.patchdepartmentid(id);
        }
        private async Task<DepartmentResponse> DepartmentResponse(int id)
        {
            var response = await GetDepartmentId(id);
            return _mapper.Map<DepartmentResponse>(response);
        }
    }
}

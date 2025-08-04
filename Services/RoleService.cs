using AutoMapper;
using Book_Keep.Helpers;
using Book_Keep.Helpers.Queries;
using Book_Keep.Interfaces;
using Book_Keep.Models;

namespace Book_Keep.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly RoleQueries _query;
        public RoleService(AppDbContext context, IMapper mapper, RoleQueries query) : base (context, mapper)
        {
            _query = query;
        }
        // [HttpGet("roles/list")]
        public async Task<List<RoleResponse>> RolesList(string? searchTerm = null)
        {
            var roles = await _query.roleslist(searchTerm);
            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("role/{id}")]
        public async Task<RoleResponse> GetRole(int id)
        {
            var role = await GetRoleId(id);
            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPost("role")]
        public async Task<RoleResponse> CreateRole(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);

            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpPatch("role/update/{id}")]
        public async Task<RoleResponse> UpdateRole(RoleRequest request, int id)
        {
            var role = await PatchRoleId(id);

            _mapper.Map(request, role);

            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpPatch("role/toggle-status")]
        public async Task<RoleResponse> ToggleStatus(int id)
        {
            var role = await PatchRoleId(id);

            role.Active = !role.Active;

            _context.Role.Update(role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpPatch("role/hide/{id}")]
        public async Task<RoleResponse> RemoveRole(int id)
        {
            var role = await PatchRoleId(id);

            role.Removed = !role.Removed;

            _context.Role.Update(role);
            await _context.SaveChangesAsync();

            return await RoleResponse(role.Id);
        }
        // [HttpDelete("role/delete/{id}")]
        public async Task<RoleResponse> DeleteRole(int id)
        {
            var role = await PatchRoleId(id);
            _context.Role.Remove(role);

            return await RoleResponse(role.Id);
        }
        // Helpers
        private async Task<Role?> GetRoleId(int id)
        {
            return await _query.getroleid(id);
        }
        private async Task<Role?> PatchRoleId(int id)
        {
            return await _query.patchroleid(id);
        }
        private async Task<RoleResponse> RoleResponse(int id)
        {
            var response = await GetRoleId(id);
            return _mapper.Map<RoleResponse>(response);
        }
    }
}

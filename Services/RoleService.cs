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
        public async Task<List<RoleResponse>> roleslist(string? searchTerm = null)
        {
            var roles = await _query.roleslist(searchTerm);
            return _mapper.Map<List<RoleResponse>>(roles);
        }
        // [HttpGet("role/{id}")]
        public async Task<RoleResponse> getrole(int id)
        {
            var role = await getroleid(id);
            return _mapper.Map<RoleResponse>(role);
        }
        // [HttpPost("role")]
        public async Task<RoleResponse> createrole(RoleRequest request)
        {
            var role = _mapper.Map<Role>(request);

            _context.Role.Add(role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpPatch("role/update/{id}")]
        public async Task<RoleResponse> updaterole(RoleRequest request, int id)
        {
            var role = await patchroleid(id);

            _mapper.Map(request, role);

            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpPatch("role/toggle-status")]
        public async Task<RoleResponse> togglestatus(int id)
        {
            var role = await patchroleid(id);

            role.Active = !role.Active;

            _context.Role.Update(role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpPatch("role/hide/{id}")]
        public async Task<RoleResponse> removerole(int id)
        {
            var role = await patchroleid(id);

            role.Removed = !role.Removed;

            _context.Role.Update(role);
            await _context.SaveChangesAsync();

            return await roleResponse(role.Id);
        }
        // [HttpDelete("role/delete/{id}")]
        public async Task<RoleResponse> deleterole(int id)
        {
            var role = await patchroleid(id);
            _context.Role.Remove(role);

            return await roleResponse(role.Id);
        }
        // Helpers
        private async Task<Role?> getroleid(int id)
        {
            return await _query.getroleid(id);
        }
        private async Task<Role?> patchroleid(int id)
        {
            return await _query.patchroleid(id);
        }
        private async Task<RoleResponse> roleResponse(int id)
        {
            var response = await getroleid(id);
            return _mapper.Map<RoleResponse>(response);
        }
    }
}

using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book_Keep.Validators
{
    public class UserValidator
    {
        private readonly AppDbContext _context;
        public UserValidator(AppDbContext context)
        {
            _context = context;
        }

        public async Task ValidateLoginRequest(Login request)
        {
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Userid == request.Userid);
            if (user != null)
            {
                return;
            }

            throw new Exception("User not found.");
        }
        
        public static int ValidateUserClaim(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID claim.");
            }

            return userId;
        }
    }
}

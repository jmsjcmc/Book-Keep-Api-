using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}

using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.DataAccess
{
    public class AuthContext : IdentityDbContext<User>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }
    }
}

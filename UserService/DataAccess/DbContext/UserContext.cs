using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;
namespace UserMicroservice.DataAccess
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }
}

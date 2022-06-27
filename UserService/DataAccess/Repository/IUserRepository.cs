using System.Collections.Generic;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.DataAccess
{
    public interface IUserRepository
    {
        Response<IEnumerable<UserDto>> GetExistingUsers();
        Task<Response<UserDto>> GetUserById(string id);
    }
}
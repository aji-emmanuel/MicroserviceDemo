using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserMicroservice.DataAccess;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        [HttpGet]
        [Route("existing")]
        public IActionResult Get()
        {
            var result = _userRepository.GetExistingUsers();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userRepository.GetUserById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UserMicroservice.DataAccess;

namespace UserMicroservice.Controllers
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
        public IActionResult GetAll()
        {
            var result = _userRepository.GetExistingUsers();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _userRepository.GetUserById(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}

using AuthenticationService.DataAccess;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegRequestDto model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("userEmail", model.Email);
                var result = await _authRepository.Register(model);
                return StatusCode(result.StatusCode, result);
            }
            return new StatusCodeResult(400);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("userEmail", model.Email);
                var result = await _authRepository.Login(model);
                return StatusCode(result.StatusCode, result);
            }
            return new BadRequestResult();
        }


        [HttpPost]
        [Route("confirm-phone")]
        public async Task<IActionResult> ConfirmPhoneNumber(string otp)
        {
            var email = HttpContext.Session.GetString("userEmail");
            var result = await _authRepository.ConfirmPhoneNumber(otp, email);
            return StatusCode(result.StatusCode, result);
        }
    }
}

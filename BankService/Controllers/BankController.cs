using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationService.DataAccess;
using System.Threading.Tasks;

namespace OperationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        [HttpGet]
        [Route("existing")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _bankRepository.GetExistingBanks();
            return StatusCode(result.StatusCode, result);
        }
    }
}

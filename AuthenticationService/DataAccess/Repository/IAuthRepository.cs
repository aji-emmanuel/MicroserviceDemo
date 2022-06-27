using AuthenticationService.Models;
using System.Threading.Tasks;

namespace AuthenticationService.DataAccess
{
    public interface IAuthRepository
    {
        Task<Response<string>> ConfirmPhoneNumber(string otp, string email);
        Task<Response<string>> Login(LoginReqDto model);
        Task<Response<string>> Register(RegRequestDto model);
    }
}
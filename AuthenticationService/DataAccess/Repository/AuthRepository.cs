using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OtpNet;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AuthenticationService.DataAccess
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Response<string>> Register(RegRequestDto model)
        {
            var user = new User()
            {
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email,
                StateOfResidence = model.StateOfResidence,
                LGA = model.LGA,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.MinValue
            };
            try
            {
                using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    transaction.Complete();
                    return new Response<string>()
                    {
                        Data = user.Id,
                        Message = $"User successfully registered! Confirm your phone number using the otp: {GenerateOtp()}. Valid for 5 minutes.",
                        StatusCode = StatusCodes.Status201Created,
                        Succeeded = true
                    };
                }
                transaction.Dispose();
                return new Response<string>()
                {
                    Message = GetErrors(result),
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Succeeded = false
                };
            }
            catch (Exception ex)
            {
                return new Response<string>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Succeeded = false
                };
            }
        }

        public async Task<Response<string>> Login(LoginReqDto model)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);//FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new Response<string>()
                {
                    Message = "Invalid Credentials",
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false
                };
            }
            if (!user.PhoneNumberConfirmed)
            {
                return new Response<string>()
                {
                    Message = $"Confirm your phone number using the otp: {GenerateOtp()}. Valid for 5 minutes.",
                    StatusCode = StatusCodes.Status403Forbidden,
                    Succeeded = false
                };
            }
            return new Response<string>()
            {
                Data = user.Id,
                Message = "Login successful!",
                StatusCode = StatusCodes.Status200OK,
                Succeeded = true
            };
        }


        public async Task<Response<string>> ConfirmPhoneNumber(string otp, string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (VerifyOtp(otp))
                {
                    user.PhoneNumberConfirmed = true;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return new Response<string>()
                        {
                            Message = "PhoneNumber successfully confirmed!",
                            StatusCode = StatusCodes.Status200OK,
                            Succeeded = true
                        };
                    }
                    return new Response<string>()
                    {
                        Message = "Something went wrong!",
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Succeeded = false
                    };
                }
                return new Response<string>()
                {
                    Message = "Invalid otp!",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Succeeded = false
                };
            }
            return new Response<string>()
            {
                Message = "Invalid otp!",
                StatusCode = StatusCodes.Status400BadRequest,
                Succeeded = false
            };
        }

        private string GenerateOtp()
        {
            var bytes = Base32Encoding.ToBytes(_configuration["EncodingKey"]);
            var totp = new Totp(bytes, mode: OtpHashMode.Sha256, step: 300);
            return totp.ComputeTotp(DateTime.UtcNow);
        }

        private bool VerifyOtp(string input)
        {
            var bytes = Base32Encoding.ToBytes(_configuration["EncodingKey"]);
            var totp = new Totp(bytes, mode: OtpHashMode.Sha256, step: 300);
            return totp.VerifyTotp(input, out long timeStepMatched, window: null);
        }

        private string GetErrors(IdentityResult result)
        {
            return result.Errors.Aggregate(string.Empty, (current, err) => current + err.Description + "\n");
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Response<IEnumerable<UserDto>> GetExistingUsers()
        {
            try
            {
                var users = _userManager.Users.Where(user => user.PhoneNumberConfirmed == true).ToList();
                var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);
                return new Response<IEnumerable<UserDto>>()
                {
                    Data = mappedUsers,
                    Message = "List of existing users.",
                    StatusCode = StatusCodes.Status200OK,
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<UserDto>>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Succeeded = false
                };
            }
        }

        public async Task<Response<UserDto>> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var mappedUser = _mapper.Map<UserDto>(user);
                    return new Response<UserDto>()
                    {
                        Data = mappedUser,
                        Message = $"User with id: {id} was found!",
                        StatusCode = StatusCodes.Status200OK,
                        Succeeded = true
                    };
                }
                return new Response<UserDto>()
                {
                    Message = $"User with id: {id} does not exist!",
                    StatusCode = StatusCodes.Status404NotFound,
                    Succeeded = false
                };
            }
            catch (Exception ex)
            {
                return new Response<UserDto>()
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Succeeded = false
                };
            }
        }
    }
}

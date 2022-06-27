using AutoMapper;
using UserMicroservice.Models;

namespace UserMicroservice.Utility
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

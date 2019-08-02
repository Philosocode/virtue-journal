using AutoMapper;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserGetDto>();
        }
    }
}
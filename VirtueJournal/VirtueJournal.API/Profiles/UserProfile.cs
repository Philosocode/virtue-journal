using AutoMapper;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.API.Profiles
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
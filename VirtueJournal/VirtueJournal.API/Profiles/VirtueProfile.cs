using System;
using AutoMapper;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.API.Profiles
{
    public class VirtueProfile : Profile
    {
        public VirtueProfile()
        {
            CreateMap<Virtue, VirtueGetDto>();
            CreateMap<VirtueCreateDto, Virtue>()
                .ForMember(
                    dest => dest.CreatedAt, opt => opt.MapFrom(
                        _ => DateTimeOffset.UtcNow
                    ));
                
            CreateMap<Virtue, VirtueEditDto>()
                .ReverseMap()
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}
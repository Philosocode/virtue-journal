using System;
using AutoMapper;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Profiles
{
    public class VirtueProfile : Profile
    {
        public VirtueProfile()
        {
            CreateMap<Virtue, VirtueGetDto>();
            CreateMap<VirtueCreateDto, Virtue>()
                .ForMember(
                    dest => dest.CreatedAt, opt => opt.MapFrom(
                        _ => DateTime.Now
                    ));
                
            CreateMap<Virtue, VirtueEditDto>()
                .ReverseMap()
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}
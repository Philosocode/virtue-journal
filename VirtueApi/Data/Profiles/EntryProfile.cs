using System;
using AutoMapper;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Profiles
{
    public class EntryProfile : Profile
    {
        public EntryProfile()
        {
            CreateMap<EntryCreateDto, Entry>()
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(_ => DateTime.Now)
                )
                .ForMember(dest => dest.VirtuesLink, opt => opt.Ignore());

            CreateMap<Entry, EntryGetDto>();
        }
    }
}
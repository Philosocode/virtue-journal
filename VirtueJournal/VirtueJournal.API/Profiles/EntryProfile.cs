using System;
using AutoMapper;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.API.Profiles
{
    public class EntryProfile : Profile
    {
        public EntryProfile()
        {
            CreateMap<EntryCreateDto, Entry>()
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(_ => DateTimeOffset.UtcNow)
                )
                .ForMember(dest => dest.VirtueLinks, opt => opt.Ignore());

            CreateMap<Entry, EntryGetDto>();

            CreateMap<EntryEditDto, Entry>()
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.PreCondition(src => src.CreatedAt != null))
                .ForMember(
                    dest => dest.LastEdited,
                    opt => opt.PreCondition(src => src.LastEdited != null))
                .ForMember(
                    dest => dest.Starred,
                    opt => opt.PreCondition(src => src.Starred != null))
                .ForMember(
                    dest => dest.VirtueLinks,
                    opt => opt.Ignore()
                )
                .ForAllOtherMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );

        }
    }
}
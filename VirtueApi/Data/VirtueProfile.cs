using System;
using AutoMapper;
using VirtueApi.Entities;

namespace VirtueApi.Data
{
    public class VirtueProfile : Profile
    {
        public VirtueProfile()
        {
            CreateMap<VirtueEditDTO, Virtue>()
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.PreCondition(
                        src => (src.CreatedAt != null)
                    )
                )
                .ForAllMembers(
                    opts => opts.Condition((src, dest, srcMember) => srcMember != null)
                );
        }
    }
}
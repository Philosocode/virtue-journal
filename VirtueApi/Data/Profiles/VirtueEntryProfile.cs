using AutoMapper;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Profiles
{
    public class VirtueEntryProfile : Profile
    {
        public VirtueEntryProfile()
        {
            CreateMap<VirtueEntry, VirtueEntryGetDto>();
        }
    }
}
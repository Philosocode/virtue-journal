using AutoMapper;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;

namespace VirtueJournal.API.Profiles
{
    public class VirtueEntryProfile : Profile
    {
        public VirtueEntryProfile()
        {
            CreateMap<VirtueEntry, VirtueEntryGetDto>();
        }
    }
}
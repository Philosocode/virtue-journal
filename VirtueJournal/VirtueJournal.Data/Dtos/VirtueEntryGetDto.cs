using VirtueJournal.Data.Entities;
using VirtueJournal.Data.Enums;

namespace VirtueJournal.Data.Dtos
{
    public class VirtueEntryGetDto
    {
        public int VirtueId { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
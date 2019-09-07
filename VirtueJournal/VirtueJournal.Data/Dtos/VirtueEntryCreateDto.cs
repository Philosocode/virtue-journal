using System.ComponentModel.DataAnnotations;
using VirtueJournal.Data.Entities;
using VirtueJournal.Data.Enums;

namespace VirtueJournal.Data.Dtos
{
    public class VirtueEntryCreateDto
    {
        [Required]
        public int VirtueId { get; set; }
        
        [Required]
        [Range(0, 4)]
        public Difficulty Difficulty { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using VirtueJournal.Data.Enums;

namespace VirtueJournal.Data.Entities
{
    public class VirtueEntry
    {
        [Required]
        public int VirtueId { get; set; }
        [Required]
        public int EntryId { get; set;  } 
        [Required]
        [Range(1, 5)]
        public Difficulty Difficulty { get; set; }
        
        [Required]
        public Virtue Virtue { get; set; }
        [Required]
        public Entry Entry { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Entities
{
    public class VirtueEntry
    {
        [Required]
        public int VirtueId { get; set; }
        [Required]
        public int EntryId { get; set;  } 
        [Required]
        [Range(0, 5)]
        public Difficulty Difficulty { get; set; }
        
        [Required]
        public Virtue Virtue { get; set; }
        [Required]
        public Entry Entry { get; set; }
    }
}

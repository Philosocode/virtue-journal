using System.ComponentModel.DataAnnotations;

namespace VirtueJournal.Data.Dtos
{
    public class VirtueCreateDto
    {
        [Required]
        [StringLength(24)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Color { get; set; }
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string Icon { get; set; } 
    }
}
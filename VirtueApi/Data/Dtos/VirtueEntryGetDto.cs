using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class VirtueEntryGetDto
    {
        [Required]
        public int VirtueId { get; set; }
        
        [Required]
        public Difficulty Difficulty { get; set; }
    }
}
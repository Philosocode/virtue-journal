using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Dtos;

namespace VirtueApi.Data.Entities
{
    public class Virtue
    {
        [Key]
        public int VirtueId { get; set; }
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
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set; }
        
        // Relationships
        public ICollection<VirtueEntry> EntriesLink { get; set; }
            = new List<VirtueEntry>();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Entities
{
    public class Entry
    {
        public int EntryId { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        public bool Starred { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset CreatedAt { get; set; }
        
        [DataType(DataType.Date)]
        public DateTimeOffset LastEdited { get; set; }
        
        // Relationships
        [Required]
        [MinLength(1)]
        public ICollection<VirtueEntry> VirtueLinks { get; set; } = new List<VirtueEntry>();
    }
}

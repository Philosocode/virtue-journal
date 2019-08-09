using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtueJournal.Data.Entities
{
    public class Virtue
    {
        public Virtue()
        {
            EntryLinks = new List<VirtueEntry>();
        }
        
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
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt {get; set; }
        
        // Relationships
        [Required]
        public ICollection<VirtueEntry> EntryLinks { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
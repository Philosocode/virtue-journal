using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtueJournal.Data.Dtos
{
    public class EntryCreateDto
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset CreatedAt { get; set; }
        
        public bool Starred { get; set; }

        public ICollection<VirtueEntryCreateDto> VirtueLinks { get; set; }
    }
}
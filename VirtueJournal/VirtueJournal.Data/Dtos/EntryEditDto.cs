using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtueJournal.Data.Dtos
{
    public class EntryEditDto
    {
        [StringLength(30)]
        public string Title { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        public DateTimeOffset? CreatedAt { get; set; }
                
        [DataType(DataType.Date)]
        public DateTimeOffset? LastEdited { get; set; }

        public bool? Starred { get; set; }
        
        public ICollection<VirtueEntryCreateDto> VirtueLinks { get; set; }
    }
}
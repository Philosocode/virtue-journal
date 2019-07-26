using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Data.Dtos
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
        
        [MinLength(1)]
        public ICollection<VirtueEntryCreateDto> VirtuesLink { get; set; }
    }
}
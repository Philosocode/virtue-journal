using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class EntryCreateDto
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        public bool Starred { get; set; }

        [Required] 
        [MinLength(1)]
        public ICollection<VirtueEntryCreateDto> VirtueLinks { get; set; }
    }
}
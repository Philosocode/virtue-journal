using System;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class VirtueEntryCreateDto
    {
        [Required]
        public int VirtueId { get; set; }
        
        [Required]
        [Range(0, 5)]
        public Difficulty Difficulty { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class EntryGetDto
    {
        public int EntryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastEdited { get; set; }
        public bool Starred { get; set; }
        
        public ICollection<VirtueEntryGetDto> VirtuesLink { get; set; } = new List<VirtueEntryGetDto>();
    }
}
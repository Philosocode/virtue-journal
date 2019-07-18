using System;
using System.Collections.Generic;
using VirtueApi.Data.Dtos;

namespace VirtueApi.Data.Entities
{
    public class Entry
    {
        public int EntryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastEdited { get; set; }
        public bool Starred { get; set; }
        
        // Relationships
        public ICollection<VirtueEntry> VirtuesLink { get; set; }
            = new List<VirtueEntry>();
    }
}

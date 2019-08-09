using System;
using System.Collections.Generic;

namespace VirtueJournal.Data.Dtos
{
    public class EntryGetDto
    {
        public int EntryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastEdited { get; set; }
        public bool Starred { get; set; }
        
        public ICollection<VirtueEntryGetDto> VirtueLinks { get; set; } = new List<VirtueEntryGetDto>();
    }
}
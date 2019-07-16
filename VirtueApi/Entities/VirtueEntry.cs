using System;
using VirtueApi.Enums;

namespace VirtueApi.Entities
{
    public class VirtueEntry
    {
        public int VirtueId { get; set; }
        public int EntryId { get; set;  } 
        public Difficulty Difficulty { get; set; }
        
        public Virtue Virtue { get; set; }
        public Entry Entry { get; set; }
    }
}

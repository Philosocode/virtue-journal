using System;
using System.Collections.Generic;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class EntryCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Starred { get; set; }

        public ICollection<VirtueEntry> VirtuesLink;
    }
}
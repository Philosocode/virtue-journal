using System;
using VirtueApi.Repositories;

namespace VirtueApi.Entities
{
    public class Entry : IEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastEdited { get; set; }
        public bool Starred { get; set; }
    }
}

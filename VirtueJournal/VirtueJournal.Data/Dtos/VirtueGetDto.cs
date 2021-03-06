using System;

namespace VirtueJournal.Data.Dtos
{
    public class VirtueGetDto
    {
        public int VirtueId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTimeOffset CreatedAt {get; set; }
    }
}
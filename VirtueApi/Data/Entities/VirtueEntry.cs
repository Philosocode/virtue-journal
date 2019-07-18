namespace VirtueApi.Data.Entities
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

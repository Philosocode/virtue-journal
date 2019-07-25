using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
{
    public class VirtueEntryGetDto
    {
        public int VirtueId { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace VirtueApi.Entities
{
    public class VirtueEditDTO
    {
        [StringLength(24)]
        public string Name { get; set; }
        [StringLength(10)]
        public string Color { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
        [StringLength(100)]
        public string Icon { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedAt {get; set; }
    }
}
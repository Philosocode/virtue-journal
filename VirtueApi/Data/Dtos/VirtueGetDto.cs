using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Data.Entities;

namespace VirtueApi.Data.Dtos
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
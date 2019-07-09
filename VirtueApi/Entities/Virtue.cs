
using System;
using System.ComponentModel.DataAnnotations;
using VirtueApi.Repositories;

namespace VirtueApi.Entities
{
    public class Virtue : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedAt {get; set; }
    }
}
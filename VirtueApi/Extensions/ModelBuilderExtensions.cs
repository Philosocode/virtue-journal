using System;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Entities;

namespace VirtueApi.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedVirtues(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Virtue>().HasData(
                new Virtue
                {
                    Id = 1,
                    Color = "Red",
                    Description = "Courageous Virtue",
                    Icon = "Cool Icon",
                    Name = "Courage",
                    CreatedAt = new DateTime()
                },
                new Virtue
                {
                    Id = 2,
                    Color = "Blue",
                    Description = "Sincere Virtue",
                    Icon = "Cool Icon",
                    Name = "Sincerity",
                    CreatedAt = new DateTime()
                }
            );
        }

        public static void SeedEntries(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>().HasData(
                new Entry
                {
                    Id = 1,
                    CreatedAt = new DateTime(),
                    Description = "Blah blah blah",
                    Starred = true,
                    Title = "My first entry"
                },
                new Entry
                {
                    Id = 2,
                    CreatedAt = new DateTime(),
                    Description = "Blah blah blah",
                    Starred = true,
                    Title = "My second entry"
                }
            );
        }
        
    }
}
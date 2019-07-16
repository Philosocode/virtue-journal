using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Entities;

namespace VirtueApi.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SnakeCaseRelations(this ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes()) 
            {
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                foreach(var property in entity.GetProperties()) 
                    property.Relational().ColumnName = property.Name.ToSnakeCase();

                foreach(var key in entity.GetKeys()) 
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach(var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();

                foreach(var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
            }
        }
        public static void SeedVirtues(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Virtue>().HasData(
                new Virtue
                {
                    VirtueId = 9998,
                    Color = "Red",
                    Description = "Courageous Virtue",
                    Icon = "Cool Icon",
                    Name = "Courage",
                    CreatedAt = new DateTime()
                },
                new Virtue
                {
                    VirtueId = 9999,
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
                    EntryId = 1,
                    CreatedAt = new DateTime(),
                    Description = "Blah blah blah",
                    Starred = true,
                    Title = "My first entry"
                },
                new Entry
                {
                    EntryId = 2,
                    CreatedAt = new DateTime(),
                    Description = "Blah blah blah",
                    Starred = true,
                    Title = "My second entry"
                }
            );
        }
        
    }
}
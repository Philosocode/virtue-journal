using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data.Entities;

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

        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var virtue1 = new Virtue
            {
                VirtueId = 1,
                Color = "Red",
                Description = "Courageous Virtue",
                Icon = "Cool Icon",
                Name = "Courage",
                CreatedAt = new DateTime()
            };
            var virtue2 = new Virtue
            {
                VirtueId = 2,
                Color = "Blue",
                Description = "Sincere Virtue",
                Icon = "Cool Icon",
                Name = "Sincerity",
                CreatedAt = new DateTime()
            };

            var entry1 = new Entry
            {
                EntryId = 9998,
                CreatedAt = new DateTime(),
                Description = "Blah blah blah",
                Starred = true,
                Title = "My first entry"
            };
            var entry2 = new Entry
            {
                EntryId = 9999,
                CreatedAt = new DateTime(),
                Description = "Blah blah blah",
                Starred = true,
                Title = "My second entry"
            };

            modelBuilder.Entity<Virtue>().HasData(virtue1, virtue2);
            modelBuilder.Entity<Entry>().HasData(entry1, entry2);
        }
    }
}
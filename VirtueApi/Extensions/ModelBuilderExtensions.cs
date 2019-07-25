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

            modelBuilder.Entity<Virtue>().HasData(virtue1);
        }
    }
}
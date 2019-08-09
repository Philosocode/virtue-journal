using System;
using Microsoft.EntityFrameworkCore;
using VirtueJournal.Data.Entities;
using VirtueJournal.Shared.Extensions;

namespace VirtueJournal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        
        /* DbSet<TEntity>
         * Represents a table in the DB of class TEntity
         * LINQ queries will be transformed to SQL
        */
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Virtue> Virtues { get; set; }
        public virtual DbSet<VirtueEntry> VirtueEntries { get; set; }
        
        /* snake_case mapping for tables instead of PascalCase
         * @from: https://animesh.blog/ef-core-code-first-with-postgres/
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Set primary key of VirtueEntry
            modelBuilder.Entity<VirtueEntry>()
                .HasKey(x => new {x.VirtueId, x.EntryId});

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
    }
}
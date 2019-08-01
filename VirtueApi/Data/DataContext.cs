using Microsoft.EntityFrameworkCore;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Extensions;

/*
 * DbContext: represents db session; data access layer
 * Allows for CRUD using CLR objects (entities)
 * Entity Framework maps entities and relations defined in models to a database
*/
namespace VirtueApi.Data
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
            
            modelBuilder.SnakeCaseRelations();
            
            // Set primary key of VirtueEntry
            modelBuilder.Entity<VirtueEntry>()
                .HasKey(x => new {x.VirtueId, x.EntryId});
            
//            modelBuilder.SeedData();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using VirtueApi.Entities;
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
        public DbSet<Entry> Entries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Virtue> Virtues { get; set; }
        
        /* snake_case mapping for tables instead of PascalCase
         * @see: https://animesh.blog/ef-core-code-first-with-postgres/
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SnakeCaseRelations();
            modelBuilder.SeedVirtues();
            modelBuilder.SeedEntries();
        }
    }
}
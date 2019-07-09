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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach(var entity in builder.Model.GetEntityTypes()) 
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
            
            builder.SeedVirtues();
            builder.SeedEntries();
        }
    }
}
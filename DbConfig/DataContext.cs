using DotnetPlayground.DataEntities;
using DotnetPlayground.DbConfig.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DotnetPlayground.DbConfig
{
    public class DataContext : DbContext
    { 
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PeopleConfiguration());
            modelBuilder.ApplyConfiguration(new AllocationConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.HandleBaseEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleBaseEntities()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["Id"] = Guid.NewGuid();
                        break;
                }
            }
        }

        public DbSet<People> People { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Allocation> Allocations { get; set; }

    }
}

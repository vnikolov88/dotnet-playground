using DotnetPlayground.DataEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DotnetPlayground.DbConfig.EntitiesConfiguration
{
    public class AllocationConfiguration : IEntityTypeConfiguration<Allocation>
    {
        public void Configure(EntityTypeBuilder<Allocation> builder)
        {
            builder.HasKey(c => new { c.ProjectId, c.PeopleId});

            builder.HasOne(x => x.People)
            .WithMany(x => x.Allocations);

            builder.HasOne(x => x.Project)
           .WithMany(x => x.Allocations);

            builder.Property(x => x.Start).IsRequired();

            builder.ToTable("Allocations");

        }
    }
   
}

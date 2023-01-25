using DotnetPlayground.DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DotnetPlayground.DbConfig.EntitiesConfiguration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Color).IsRequired().HasMaxLength(20);

            builder.ToTable("Projects");
        }
    }
}

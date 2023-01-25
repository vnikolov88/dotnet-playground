using DotnetPlayground.DataEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DotnetPlayground.DbConfig.EntitiesConfiguration
{
    public class PeopleConfiguration : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Tags).IsRequired();

        }
    }

}

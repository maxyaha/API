using Microservice.DataAccress.Entites.Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Events.Configurable
{
    internal class AggregateRootConfiguration : IEntityTypeConfiguration<AggregateRoot>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<AggregateRoot> builder)
        {
            builder.ToTable("AggregateRoot");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Version).HasColumnName("Version").IsRequired();
            builder.Property(x => x.EventVersion).HasColumnName("EventVersion").IsRequired();
        }
    }
}

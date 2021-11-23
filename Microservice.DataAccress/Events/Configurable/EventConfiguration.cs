using Microservice.DataAccress.Entites.Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Events.Configurable
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.AggregateID).HasColumnName("AggregateID").IsRequired();
            builder.Property(x => x.Data).HasColumnName("Data").IsRequired();
            builder.Property(x => x.Timestamp).HasColumnName("Timestamp").IsRequired();
        }
    }
}

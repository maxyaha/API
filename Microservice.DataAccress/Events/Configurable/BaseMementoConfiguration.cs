using Microservice.DataAccress.Entites.Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Events.Configurable
{
    internal class BaseMementoConfiguration : IEntityTypeConfiguration<BaseMemento>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<BaseMemento> builder)
        {
            builder.ToTable("BaseMementoe");

            builder.HasKey(x => new { x.ID, x.Version });

            builder.Property(x => x.ID).HasColumnName("ID").IsRequired();
            builder.Property(x => x.Code).HasColumnName("Code").IsRequired();
            builder.Property(x => x.Version).HasColumnName("Version").IsRequired();
            builder.Property(x => x.Data).HasColumnName("Data").IsRequired();
        }
    }
}

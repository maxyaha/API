using Microservice.DataAccress.Entites.Features.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Features.Configurable
{
    internal class PrivacyTypeConfiguration : IEntityTypeConfiguration<PrivacyType>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<PrivacyType> builder)
        {
            builder.ToTable("PrivacyType");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID).HasColumnName("ID").IsRequired().HasDefaultValue();
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();
            builder.Property(o => o.Active).HasColumnName("Active").IsRequired();
            builder.Property(o => o.Code).HasColumnName("Code");
            builder.Property(o => o.Name).HasColumnName("Name");
            builder.Property(o => o.Description).HasColumnName("Description");

        }
    }
}

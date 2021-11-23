using Microservice.DataAccress.Entites.Features.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Features.Configurable
{
    internal class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Feature");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID).HasColumnName("ID").IsRequired().HasDefaultValue();
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();
            builder.Property(o => o.Active).HasColumnName("Active").IsRequired();
            builder.Property(o => o.Topics).HasColumnName("Topics").IsRequired();
            builder.Property(o => o.Description).HasColumnName("Description").IsRequired();
        }
    }
}

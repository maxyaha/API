using Microservice.DataAccress.Entites.Features.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Features.Configurable
{
    internal class GuideConfiguration : IEntityTypeConfiguration<Guide>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.ToTable("Guide");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID).HasColumnName("ID").IsRequired().HasDefaultValue();
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();
            builder.Property(o => o.Active).HasColumnName("Active").IsRequired();
            builder.Property(o => o.Image).HasColumnName("Image").IsRequired();
            builder.Property(o => o.FeatureID).HasColumnName("FeatureID");
            builder.Property(o => o.PrivacyTypeID).HasColumnName("PrivacyTypeID");

            builder.HasOne(o => o.Feature).WithMany(o => o.Guides).HasForeignKey(o => o.FeatureID);
            builder.HasOne(o => o.PrivacyType).WithMany(o => o.Guides).HasForeignKey(o => o.PrivacyTypeID);
        }
    }
}

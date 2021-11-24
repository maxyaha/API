using System;
using Microservice.DataAccress.Entites.IPD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.IPD.Configurable
{
    internal class PendingCodeConfiguration : IEntityTypeConfiguration<PendingCode>
    {
        public void Configure(EntityTypeBuilder<PendingCode> builder)
        {
            builder.ToTable("PendingCode");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID).HasColumnName("ID").IsRequired().HasDefaultValue(Guid.Empty);
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();

            builder.Property(o => o.Code).HasColumnName("Code");
            builder.Property(o => o.DescriptionEN).HasColumnName("DescriptionEN");
            builder.Property(o => o.DescriptionTH).HasColumnName("DescriptionTH");
            builder.Property(o => o.Flag).HasColumnName("Flag");


        }
    }
}

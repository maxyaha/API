using Microservice.DataAccress.Entites.Tester.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Tester.Configurable
{
    internal class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test");

            builder.HasKey(o => o.ID);

            //builder.Property(o => o.ID).HasColumnName("ID").IsRequired().HasDefaultValueSql();
            builder.Property(o => o.ID).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();
            builder.Property(o => o.Active).HasColumnName("Active").IsRequired();

            builder.Property(o => o.Age).HasColumnName("Age");
            builder.Property(o => o.Name).HasColumnName("Name");


        }
    }
}

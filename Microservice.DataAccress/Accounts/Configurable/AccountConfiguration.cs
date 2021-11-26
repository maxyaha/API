using System;
using Microservice.DataAccress.Entites.Accounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.DataAccress.Accounts.Configurable
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        private readonly string schema;

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID).HasColumnName("ID").IsRequired();
            builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(o => o.ModifiedDate).HasColumnName("ModifiedDate");
            builder.Property(o => o.Version).HasColumnName("Version").IsRequired();
            builder.Property(o => o.Active).HasColumnName("Active").IsRequired();

            builder.Property(o => o.Username).HasColumnName("Username");
            builder.Property(o => o.Password).HasColumnName("Password");


        }
    }
}

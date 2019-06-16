using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(80).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(80).IsRequired();
            builder.Property(u => u.Password).HasMaxLength(50).IsRequired();


            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");


            


        }
    }
}

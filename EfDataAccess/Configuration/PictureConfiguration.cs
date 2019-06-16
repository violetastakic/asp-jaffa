using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configuration
{
    class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasIndex(p => p.Path).IsUnique();

            builder.Property(p => p.Path).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Alt).HasMaxLength(30).IsRequired();

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }

}

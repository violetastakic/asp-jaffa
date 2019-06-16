using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Subtitle).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Description).IsRequired();

            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasMany(p => p.PostCategories)
                .WithOne(pc => pc.Post)
                .HasForeignKey(pc => pc.PostId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}

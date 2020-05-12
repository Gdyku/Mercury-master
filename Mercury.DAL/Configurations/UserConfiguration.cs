using Mercury.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(b => b.Id)
                .HasColumnName("Id");

            builder.Property(b => b.Name)
                .HasColumnName("Name");

            builder.Property(b => b.Email)
                .HasColumnName("Email");

            builder.Property(b => b.PasswordHash)
                .HasColumnName("PasswordHash");

            builder.Property(b => b.PasswordSalt)
                .HasColumnName("PasswordSalt");

            builder.Property(b => b.IsNewsletterEnabled)
                .HasColumnName("IsNewsletterEnabled");

            builder.Property(b => b.IsRecommendedReadingEnabled)
                .HasColumnName("IsRecommendedReadingEnabled");
        }
    }
}

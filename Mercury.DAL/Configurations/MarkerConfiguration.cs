using Mercury.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Configurations
{
    public class MarkerConfiguration : IEntityTypeConfiguration<Marker>
    {
        public void Configure(EntityTypeBuilder<Marker> builder)
        {
            builder.ToTable("Markers");

            builder.Property(b => b.Id)
                .HasColumnName("Id");

            builder.Property(b => b.Name)
                .HasColumnName("Name");

            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive");

            builder.Property(b => b.Latitude)
                .HasColumnName("Lat");

            builder.Property(b => b.Longitude)
                .HasColumnName("Lng");

            builder.Property(b => b.UserId)
                .HasColumnName("UserId");

            builder.HasOne(m => m.User)
              .WithMany(u => u.Markers)
              .HasForeignKey(m => m.UserId);

            builder.HasMany(m => m.ScheduleMarkers)
              .WithOne(u => u.Marker)
              .HasForeignKey(m => m.MarkerId);
        }
    }
}

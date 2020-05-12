using Mercury.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Configurations
{
    public class ScheduleMarkerConfiguration : IEntityTypeConfiguration<ScheduleMarker>
    {
        public void Configure(EntityTypeBuilder<ScheduleMarker> builder)
        {
            builder.ToTable("ScheduleMarkers");

            builder.Property(b => b.Id)
                .HasColumnName("Id");

            builder.Property(b => b.SchduleId)
                .HasColumnName("ScheduleId");

            builder.Property(b => b.MarkerId)
                .HasColumnName("MarkerId");

            builder.HasOne(m => m.Schedule)
              .WithMany(u => u.ScheduleMarkers)
              .HasForeignKey(m => m.SchduleId);

            builder.HasOne(m => m.Marker)
              .WithMany(u => u.ScheduleMarkers)
              .HasForeignKey(m => m.MarkerId);
        }
    }
}

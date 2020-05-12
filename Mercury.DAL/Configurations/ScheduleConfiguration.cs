using Mercury.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.Property(b => b.Id)
                .HasColumnName("Id");

            builder.Property(b => b.Name)
                .HasColumnName("Name");

            builder.Property(b => b.Description)
                .HasColumnName("Description");

            builder.Property(b => b.IsActive)
                .HasColumnName("IsActive");

            builder.HasMany(m => m.ScheduleMarkers)
              .WithOne(u => u.Schedule)
              .HasForeignKey(m => m.SchduleId);
        }
    }
}

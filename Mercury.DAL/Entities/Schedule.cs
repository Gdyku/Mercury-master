using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Entities
{
    public class Schedule
    {
        public Schedule()
        {
            ScheduleMarkers = new List<ScheduleMarker>();
            IsActive = true;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ScheduleMarker> ScheduleMarkers { get; set; }
        public bool IsActive { get; set; }
    }
}

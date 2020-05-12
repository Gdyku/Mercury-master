using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Entities
{
    public class ScheduleMarker
    {
        public long Id { get; set; }

        public long SchduleId { get; set; }
        public Schedule Schedule { get; set; }

        public long MarkerId { get; set; }
        public Marker Marker { get; set; }
    }
}

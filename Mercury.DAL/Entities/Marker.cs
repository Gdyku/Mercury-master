using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.DAL.Entities
{
    public class Marker
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }

        public ICollection<ScheduleMarker> ScheduleMarkers { get; set; }
    }
}

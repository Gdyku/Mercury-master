using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mercury.BLL.Dtos
{
    public class ScheduleDto
    {
        public ScheduleDto()
        {
            Markers = new List<MarkerDto>();
        }

        public long Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        [MinLength(3)]
        public string Description { get; set; }
        public List<MarkerDto> Markers { get; set; }
    }
}

using Mercury.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Intefaces
{
    public interface IScheduleLogic
    {
        Task<List<ScheduleDto>> GetSchedules();
        Task<ScheduleDto> InsertSchedule(ScheduleDto schedule);
    }
}

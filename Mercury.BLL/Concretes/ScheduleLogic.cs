using AutoMapper;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository;
using Mercury.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Concretes
{
    public class ScheduleLogic : IScheduleLogic
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMarkerRepository _markerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleLogic(IScheduleRepository scheduleRepository, IUnitOfWork unitOfWork,
            IMapper mapper, IMarkerRepository markerRepository)
        {
            _scheduleRepository = scheduleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _markerRepository = markerRepository;
        }

        public async Task<List<ScheduleDto>> GetSchedules()
        {
            var schedules = await _scheduleRepository.GetAll();

            var mappedSchedules = _mapper.Map<List<Schedule>, List<ScheduleDto>>(schedules.ToList());

            return mappedSchedules;
        }

        public async Task<ScheduleDto> InsertSchedule(ScheduleDto schedule)
        {
            var scheduleInDb = _mapper.Map<ScheduleDto, Schedule>(schedule);

            foreach(var marker in schedule.Markers)
            {
                var markerInDb = _mapper.Map<MarkerDto, Marker>(marker);

                var scheduleMarker = new ScheduleMarker()
                {
                    Schedule = scheduleInDb,
                    Marker = markerInDb
                };

                scheduleInDb.ScheduleMarkers.Add(scheduleMarker);
            }

            _scheduleRepository.Add(scheduleInDb);

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<Schedule, ScheduleDto>(scheduleInDb);
        }
    }
}

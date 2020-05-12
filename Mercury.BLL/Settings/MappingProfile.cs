using AutoMapper;
using Mercury.BLL.Dtos;
using Mercury.DAL.Entities;
using Mercury.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.BLL.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<NewUserDto, User>();
            CreateMap<MarkerDto, Marker>().ReverseMap();
            CreateMap<User, Password>();

            CreateMap<Schedule, ScheduleDto>()
                .ForMember(o => o.Markers, a => a.MapFrom(b => b.ScheduleMarkers.Select(c => c.Marker)));

            CreateMap<ScheduleDto, Schedule>()
                .ForMember(o => o.ScheduleMarkers, a => a.Ignore());
        }
    }
}

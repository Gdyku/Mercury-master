using Mercury.DAL.Data;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.DAL.Repository.Concretes
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override async Task<IEnumerable<Schedule>> GetAll()
        {
            return await _context.Set<Schedule>()
                .Include(s => s.ScheduleMarkers)
                .ThenInclude(c => c.Marker)
                .Where(m => m.IsActive)
                .ToListAsync();
        }
    }
}

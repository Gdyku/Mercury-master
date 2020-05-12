using Mercury.DAL.Data;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.DAL.Repository.Concretes
{
    public class MarkerRepository : Repository<Marker>, IMarkerRepository
    {
        public MarkerRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public override async Task<Marker> GetWithThrow(Expression<Func<Marker, bool>> predicate)
        {
            var marker = await _context.Set<Marker>()
                .Include(m => m.User)
                .FirstOrDefaultAsync(predicate);

            if(marker == null)
            {
                throw new InvalidOperationException();
            }

            return marker;
        }

        public override async Task<IEnumerable<Marker>> GetAll()
        {
            return await _context.Set<Marker>()
                .Where(m => m.IsActive)
                .ToListAsync();
        }

        public override void Remove(Marker entity)
        {
            entity.IsActive = false;
        }
    }
}

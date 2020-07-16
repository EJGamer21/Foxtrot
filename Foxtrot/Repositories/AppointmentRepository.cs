using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Foxtrot.Dtos;
using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Foxtrot.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly FoxtrotContext _context;
        public AppointmentRepository(FoxtrotContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Appointment> GetById(object id)
        {
            return await _context.Appointments
                .Include(a => a.Creator)
                .Include(a => a.Provider)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == (Guid) id);
        }

        public override async Task<IEnumerable<Appointment>> Get(Expression<Func<Appointment, bool>> filter = null,
            Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy = null, string includeProperties = "")
        {
            return await _context.Appointments
                .Include(a => a.Creator)
                .Include(a => a.Provider)
                .Include(a => a.Service)
                .ToListAsync();
        }
    }
}
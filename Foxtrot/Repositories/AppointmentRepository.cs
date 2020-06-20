using Foxtrot.Models;
using Foxtrot.Repositories.Contracts;

namespace Foxtrot.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}
using Foxtrot.Models;

namespace Foxtrot.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        public AppointmentRepository(FoxtrotContext context) : base(context)
        {
        }
    }
}
using System.Collections.Generic;

namespace Foxtrot.Models
{
    public class AppointmentStatus : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
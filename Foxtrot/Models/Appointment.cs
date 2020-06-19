using System;

namespace Foxtrot.Models
{
    public class Appointment : BaseModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User Creator { get; set; }
        public Service Service { get; set; }
        public User Provider { get; set; }
    }
}
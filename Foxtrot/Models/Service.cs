using System;
using System.Collections.Generic;

namespace Foxtrot.Models
{
    public class Service : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
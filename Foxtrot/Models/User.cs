using System;
using System.Collections.Generic;
using Foxtrot.Models.Contracts;

namespace Foxtrot.Models
{
    public class User : BaseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Dni { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
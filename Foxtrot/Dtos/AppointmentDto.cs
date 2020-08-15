using System;

namespace Foxtrot.Dtos
{
    public class AppointmentDto
    {
        public Guid? Id { get; set; }
        public Guid? ServiceId { get; set; }
        public Guid? ProviderId { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; }
        public int StatusId { get; set; }
    }
}
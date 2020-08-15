using System;

namespace Foxtrot.Dtos
{
    public class DashboardDto
    {
        public int Opened { get; set; }
        public int Pending { get; set; }
        public int Closed { get; set; }
        public int Cancelled { get; set; }
        public double ClosedAvg { get; set; }
    }
}
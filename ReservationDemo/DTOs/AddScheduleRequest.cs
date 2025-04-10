using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class AddScheduleRequest
    {
        public int TrainId { get; set; }
        public int StationId { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int StopNumber { get; set; }
        public DateTime JourneyDate { get; set; }
    }
}
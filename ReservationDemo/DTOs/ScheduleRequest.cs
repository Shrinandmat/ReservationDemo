using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class ScheduleRequest
    {
        public int train_id { get; set; }
        public int station_id { get; set; }
        public TimeSpan arrival_time { get; set; }
        public TimeSpan departure_time { get; set; }
        public int stop_number { get; set; }
        public DateTime journey_date { get; set; }
    }
}
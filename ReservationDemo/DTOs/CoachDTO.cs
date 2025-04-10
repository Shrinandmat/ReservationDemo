using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class CoachDTO
    {
        public int train_id { get; set; }
        public string coach_type { get; set; }
        public int seat_capacity { get; set; }
        public decimal fare_multiplier { get; set; }
    }
}
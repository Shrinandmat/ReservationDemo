using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class SeatRequest
    {

        public int CoachId { get; set; }
        public int SeatNumber { get; set; }
    }
}
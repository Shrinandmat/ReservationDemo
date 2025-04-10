using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class BookTicketRequest
    {
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public int NumberOfPassengers { get; set; }

        public List<SeatRequest> Seats { get; set; }
    }
}
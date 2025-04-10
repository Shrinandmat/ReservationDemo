using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class AddStationRequest
    {
        public string StationName { get; set; }
        public string Location { get; set; }
    }
}
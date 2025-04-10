using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class AddTrainRequest
    {
        public string TrainName { get; set; }
        public string TrainType { get; set; }
        public int StartStationId { get; set; }
        public int EndStationId { get; set; }
        public decimal BaseFare { get; set; }
    }
}
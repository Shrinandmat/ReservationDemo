namespace ReservationDemo.Controllers
{
    internal class TrainDto
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string TrainType { get; set; }
        public int? StartStationId { get; set; }
        public int? EndStationId { get; set; }
        public decimal BaseFare { get; set; }
    }
}
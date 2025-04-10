using ReservationDemo.Models;
using System.Linq;
using System.Web.Http;

namespace ReservationDemo.Controllers
{
    public class TrainController : ApiController
    {
        private RailwayReservationEntities1 db = new RailwayReservationEntities1();

        [HttpGet]
        [Route("api/trains")]
        public IHttpActionResult GetTrains()
        {
            var trains = db.Trains.ToList();  // assuming a table named Trains
            return Ok(trains);
        }

        [HttpGet]
        [Route("api/trains/{id}")]
        public IHttpActionResult GetTrain(int id)
        {
            var train = db.Trains.FirstOrDefault(t => t.train_id == id);
            if (train == null)
                return NotFound();

            return Ok(train);
        }
    }
}
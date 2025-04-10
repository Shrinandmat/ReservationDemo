using ReservationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ReservationDemo.DTOs;
using ReservationDemo.Models;

namespace ReservationDemo.Controllers
{
    [RoutePrefix("api/trains")]
    public class SearchController : ApiController
    {
        private readonly RailwayReservationEntities1 db = new RailwayReservationEntities1();

        //[HttpGet]
        //[Route("search")]
        //public async Task<IHttpActionResult> SearchTrains(string from, string to, DateTime date)
        //{
        //    var schedules = await db.Train_Schedules
        //        .Include(s => s.Trains )
        //        .Include(s => s.Station)
        //        .Where(s => s.Station.station_name == from && s.journey_date == date)
        //        .ToListAsync();
        //    var result = schedules.Select(s => new
        //    {
        //        TrainId = s.Train.train_id,
        //        TrainName = s.Train.train_name,
        //        TrainType = s.Train.train_type,
        //        FromStation = from,
        //        ToStation = to,
        //        JourneyDate = s.journey_date,
        //        DepartureTime = s.departure_time

        //    });

        //    return Ok(result);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //[HttpGet]
        //[Route("search")]
        //public async Task<IHttpActionResult> SearchTrains(string from, string to, DateTime date)
        //{
        //    var fromStationId = db.Stations.FirstOrDefault(x => x.station_name == from)?.station_id;
        //    var toStationId = db.Stations.FirstOrDefault(x => x.station_name == to)?.station_id;

        //    if (fromStationId == null || toStationId == null)
        //    {
        //        return NotFound();
        //    }

        //    var schedules = await db.Train_Schedules
        //        .Include(s => s.Train)
        //        .Where(s => s.start_station_id == fromStationId
        //                 && s.end_station_id == toStationId
        //                 && s.journey_date == date)
        //        .ToListAsync();

        //    var result = schedules.Select(s => new
        //    {
        //        TrainId = s.Train.train_id,
        //        TrainName = s.Train.train_name,
        //        TrainType = s.Train.train_type,
        //        FromStation = from,
        //        ToStation = to,
        //        JourneyDate = s.journey_date,
        //        DepartureTime = s.departure_time
        //    });

        //    return Ok(result);
        //}
        //[HttpGet]
        //[Route("search-trains")]
        //public HttpResponseMessage SearchTrains(string from, string to)
        //{
        //    var startStation = db.Stations.FirstOrDefault(s => s.station_name == from);
        //    var endStation = db.Stations.FirstOrDefault(s => s.station_name == to);

        //    if (startStation == null || endStation == null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, "Station Not Found");
        //    }

        //    var trains = db.Trains
        //        .Where(t => t.start_station_id == startStation.station_id && t.end_station_id == endStation.station_id)
        //        .Select(t => new
        //        {
        //            t.train_id,
        //            t.train_name,
        //            t.train_type
        //        })
        //        .ToList();

        //    if (!trains.Any())
        //    {
        //        return Request.CreateResponse(HttpStatusCode.NotFound, "No Trains Found");
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, trains);
        //}
        [HttpGet]
        [Route("search-train-by-name")]
        public HttpResponseMessage SearchTrainByName(string trainName)
        {
            var train = db.Trains
                .Where(t => t.train_name.Contains(trainName))  // Use Contains for partial search
                .Select(t => new
                {
                    t.train_id,
                    t.train_name,
                    t.train_type,
                    StartStation = db.Stations.Where(s => s.station_id == t.start_station_id).Select(s => s.station_name).FirstOrDefault(),
                    EndStation = db.Stations.Where(s => s.station_id == t.end_station_id).Select(s => s.station_name).FirstOrDefault()
                })
                .ToList();

            if (!train.Any())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Train Found with given Name");
            }

            return Request.CreateResponse(HttpStatusCode.OK, train);
        }



    }
}



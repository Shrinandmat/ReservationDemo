using ReservationDemo.DTOs;
using ReservationDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReservationDemo.Controllers
{
    [RoutePrefix("api/train")]
    public class AdminController : ApiController
    {
        private readonly RailwayReservationEntities1 _context = new RailwayReservationEntities1();

        [HttpGet]
        [Route("get-all-trains")]
        public IHttpActionResult GetAllTrains()
        {
            var trains = _context.Trains
                .Select(t => new TrainDto
                {
                    TrainId = t.train_id,
                    TrainName = t.train_name,
                    TrainType = t.train_type,
                    StartStationId = t.start_station_id,
                    EndStationId = t.end_station_id,
                    BaseFare = t.base_fare
                }).ToList();

            return Ok(trains);
        }


        [HttpPost]
        [Route("addtrain")]
        public IHttpActionResult AddTrain(AddTrainRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid train data.");
            }

            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    Train newTrain = new Train
                    {
                        train_name = request.TrainName,
                        train_type = request.TrainType,
                        start_station_id = request.StartStationId,
                        end_station_id = request.EndStationId,
                        base_fare = request.BaseFare
                    };

                    db.Trains.Add(newTrain);
                    db.SaveChanges();
                }

                return Ok("Train Added Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        [HttpPut]
        [Route("updatetrain/{id}")]
        public IHttpActionResult UpdateTrain(int id, AddTrainRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid train data.");
            }

            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var train = db.Trains.Find(id);

                    if (train == null)
                    {
                        return NotFound();
                    }

                    train.train_name = request.TrainName;
                    train.train_type = request.TrainType;
                    train.start_station_id = request.StartStationId;
                    train.end_station_id = request.EndStationId;
                    train.base_fare = request.BaseFare;

                    db.SaveChanges();
                }

                return Ok("Train Updated Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("deletetrain/{id}")]
        public IHttpActionResult DeleteTrain(int id)
        {
            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var train = db.Trains.Find(id);

                    if (train == null)
                    {
                        return NotFound(); // Train not found
                    }

                    db.Trains.Remove(train);
                    db.SaveChanges();
                }

                return Ok("Train Deleted Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        //[HttpPost]
        //[ActionName("UpdateTrain")]
        //public IHttpActionResult UpdateTrain(int trainId, TrainDto updateDto)
        //{
        //    var trainData = _context.Trains.Find(trainId);

        //    if (trainData == null)
        //        return NotFound();

        //    var  = _context.Stations.Any(s => s.StationId == updateDto.StartStationId);
        //    var endStationExists = _context.Stations.Any(s => s.StationId == updateDto.EndStationId);

        //    if (!startStationExists || !endStationExists)
        //        return BadRequest("Invalid Station Ids");

        //    trainData.TrainName = updateDto.TrainName;
        //    trainData.StartStationId = updateDto.StartStationId;
        //    trainData.EndStationId = updateDto.EndStationId;

        //    _context.SaveChanges();

        //    return Ok("Train Updated Successfully");
        //}

        // This should be outside UpdateTrain
        //[HttpDelete]
        //[Route("{id:int}")]
        //public IHttpActionResult DeleteTrain(int id)
        //{
        //    var train = _context.Trains.Find(id);
        //    if (train == null)
        //        return NotFound();

        //    _context.Trains.Remove(train);
        //    _context.SaveChanges();

        //    return StatusCode(System.Net.HttpStatusCode.NoContent);
        //}


        [HttpGet]
        [Route("stations")]
        public IHttpActionResult GetAllStations()
        {
            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var stations = db.Stations
                                     .Select(s => new
                                     {
                                         s.station_id,
                                         s.station_name,
                                         s.location
                                     })
                                     .ToList();

                    if (stations == null)
                    {
                        return NotFound();
                    }

                    return Ok(stations);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("addstation")]
        public IHttpActionResult AddStation(AddStationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid station data.");
            }

            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    Station newStation = new Station
                    {
                        station_name = request.StationName,
                        location = request.Location
                    };

                    db.Stations.Add(newStation);
                    db.SaveChanges();
                }

                return Ok("Station Added Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("updatestation/{id}")]
        public IHttpActionResult UpdateStation(int id, AddStationRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid station data.");
            }

            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var station = db.Stations.Find(id);

                    if (station == null)
                    {
                        return NotFound();
                    }

                    station.station_name = request.StationName;
                    station.location = request.Location;

                    db.SaveChanges();
                }

                return Ok("Station Updated Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("deletestation/{id}")]
        public IHttpActionResult DeleteStation(int id)
        {
            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var station = db.Stations.Find(id);

                    if (station == null)
                    {
                        return NotFound();
                    }

                    db.Stations.Remove(station);
                    db.SaveChanges();
                }

                return Ok("Station Deleted Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpGet]
        [Route("getallschedules")]
        public IHttpActionResult GetAllSchedules()
        {
            var db = new RailwayReservationEntities1();
            var schedules = db.Train_Schedules
                        .Select(s => new
                        {
                            s.schedule_id,
                            s.train_id,
                            s.station_id,
                            s.arrival_time,
                            s.departure_time,
                            s.stop_number,
                            s.journey_date
                        })
                        .ToList();

            if (schedules == null)
                return NotFound();

            return Ok(schedules);
        }

        //[HttpPost]
        //[Route("api/schedule/add")]
        //public IHttpActionResult AddSchedule([FromBody] Train_Schedules schedule)
        //{
        //    var db = new RailwayReservationEntities1();
        //    if (schedule == null)
        //        return BadRequest("Invalid Data");

        //    db.Train_Schedules.Add(schedule);
        //    db.SaveChanges();

        //    return Ok("Schedule Added Successfully");
        //}
        [HttpPost]
        [Route("addschedule")]
        public IHttpActionResult AddSchedule([FromBody] ScheduleRequest request)
        {
            var db = new RailwayReservationEntities1();
            if (request == null)
                return BadRequest("Invalid Data");

            Train_Schedules schedule = new Train_Schedules
            {
                train_id = request.train_id,
                station_id = request.station_id,
                arrival_time = request.arrival_time,
                departure_time = request.departure_time,
                stop_number = request.stop_number,
                journey_date = request.journey_date
            };

            db.Train_Schedules.Add(schedule);
            db.SaveChanges();

            return Ok("Schedule Added Successfully");
        }
        [HttpPut]
        [Route("updateschedule/{id}")]
        public IHttpActionResult UpdateSchedule(int id, AddScheduleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid schedule data.");
            }

            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var existingSchedule = db.Train_Schedules.Find(id);

                    if (existingSchedule == null)
                    {
                        return NotFound();
                    }

                    // Update the schedule details
                    existingSchedule.train_id = request.TrainId;
                    existingSchedule.station_id = request.StationId;
                    existingSchedule.arrival_time = request.ArrivalTime;
                    existingSchedule.departure_time = request.DepartureTime;
                    existingSchedule.stop_number = request.StopNumber;
                    existingSchedule.journey_date = request.JourneyDate;

                    db.SaveChanges();
                }

                return Ok("Schedule Updated Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("deleteschedule/{id}")]
        public IHttpActionResult DeleteSchedule(int id)
        {
            try
            {
                using (var db = new RailwayReservationEntities1())
                {
                    var schedule = db.Train_Schedules.Find(id);

                    if (schedule == null)
                    {
                        return NotFound();
                    }

                    db.Train_Schedules.Remove(schedule);
                    db.SaveChanges();
                }

                return Ok("Schedule Deleted Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //[HttpGet]
        //[Route("api/coach/all")]
        //public IHttpActionResult GetAllCoaches()
        //{
        //    var db = new RailwayReservationEntities1();
        //    var coaches = db.Coaches.ToList();
        //    return Ok(coaches);
        //}
        [HttpGet]
        [Route("get-all-coaches")]
        public IHttpActionResult GetAllCoaches()
        {
            var db = new RailwayReservationEntities1();
            var coaches = db.Coaches
                .Select(x => new
                {
                    x.coach_id,
                    x.train_id,
                    x.coach_type,
                    x.seat_capacity,
                    x.fare_multiplier
                })
                .ToList();

            if (coaches == null)
            {
                return NotFound();  // No Coaches Found
            }

            return Ok(coaches);
        }

        [HttpPost]
        [Route("addcoach")]
        public IHttpActionResult AddCoach(CoachDTO coachDto)
        {
            var db = new RailwayReservationEntities1();
            if (coachDto == null)
                return BadRequest("Invalid Data.");

            Coach coach = new Coach
            {
                train_id = coachDto.train_id,
                coach_type = coachDto.coach_type,
                seat_capacity = coachDto.seat_capacity,
                fare_multiplier = coachDto.fare_multiplier
            };

            db.Coaches.Add(coach);
            db.SaveChanges();

            return Ok("Coach Added Successfully.");
        }
        [HttpPut]
        [Route("updatecoach/{id}")]
        public IHttpActionResult UpdateCoach(int id, CoachDTO coachDto)
        {
            if (coachDto == null)
                return BadRequest("Invalid Data.");

            using (var db = new RailwayReservationEntities1())
            {
                var coach = db.Coaches.FirstOrDefault(x => x.coach_id == id);

                if (coach == null)
                    return NotFound();

                coach.train_id = coachDto.train_id;
                coach.coach_type = coachDto.coach_type;
                coach.seat_capacity = coachDto.seat_capacity;
                coach.fare_multiplier = coachDto.fare_multiplier;

                db.SaveChanges();
            }

            return Ok("Coach Updated Successfully.");
        }
        [HttpDelete]
        [Route("deletecoach/{id}")]
        public IHttpActionResult DeleteCoach(int id)
        {
            using (var db = new RailwayReservationEntities1())
            {
                var coach = db.Coaches.FirstOrDefault(x => x.coach_id == id);

                if (coach == null)
                    return NotFound();

                db.Coaches.Remove(coach);
                db.SaveChanges();
            }

            return Ok("Coach Deleted Successfully.");
        }




    }
}


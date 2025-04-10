using ReservationDemo.DTOs;
using ReservationDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReservationDemo.Controllers
{
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {
        private readonly RailwayReservationEntities1 _context;

        public BookingController()
        {
            _context = new RailwayReservationEntities1();
        }

        [HttpGet]
        [Route("get-all-trains")]
        public HttpResponseMessage GetAllTrains()
        {
            var trains = _context.Trains.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, trains);
        }
        private RailwayReservationEntities1 db = new RailwayReservationEntities1();

     


        [HttpGet]
        [Route("search-trains")]
        public HttpResponseMessage SearchTrains(int startStationId, int endStationId)
        {
            var trains = _context.Trains
                .Where(t => t.start_station_id == startStationId && t.end_station_id == endStationId)
                .ToList();

            if (trains.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Trains Found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, trains);
        }



        [HttpGet]
        [Route("available-seats")]
        public HttpResponseMessage GetAvailableSeats(int scheduleId, int coachId)
        {
            var schedule = _context.Train_Schedules.FirstOrDefault(s => s.schedule_id == scheduleId);
            if (schedule == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Schedule!");

            var coach = _context.Coaches.FirstOrDefault(c => c.coach_id == coachId);
            if (coach == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Coach for this Train!");

            var availableSeats = _context.Seats
                .Where(s => s.coach_id == coachId && s.status == "Available")
                .Select(s => new { s.seat_id, s.seat_number })
                .ToList();

            if (!availableSeats.Any())
                return Request.CreateResponse(HttpStatusCode.OK, "No Available Seats!");

            return Request.CreateResponse(HttpStatusCode.OK, availableSeats);
        }

        [HttpPost]
        [Route("book-ticket")]
        public HttpResponseMessage BookTicket(BookTicketRequest request)
        {
            if (request.Seats.Count != request.NumberOfPassengers)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Number of passengers should match the number of seats provided.");

            var schedule = _context.Train_Schedules.FirstOrDefault(s => s.schedule_id == request.ScheduleId);
            if (schedule == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Schedule!");

            var train = _context.Trains.FirstOrDefault(t => t.train_id == schedule.train_id);
            if (train == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Train Not Found!");

            var reservations = new List<Reservation>();

            foreach (var seatReq in request.Seats)
            {
                var seat = _context.Seats
                    .FirstOrDefault(s => s.coach_id == seatReq.CoachId && s.seat_number == seatReq.SeatNumber && s.status == "Available");

                if (seat == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Seat {seatReq.SeatNumber} in Coach {seatReq.CoachId} is not available");

                var reservation = new Reservation
                {
                    user_id = request.UserId,
                    schedule_id = request.ScheduleId,
                    seat_id = seat.seat_id,
                    pnr_number = Guid.NewGuid().ToString().Substring(0, 10).ToUpper(),
                    status = "Booked",
                    booked_at = DateTime.Now,
                    number_of_passengers = 1,
                    total_fare = train.base_fare
                };

                seat.status = "Booked";

                _context.Reservations.Add(reservation);
                reservations.Add(reservation);
            }

            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                Message = "Tickets Booked Successfully!",
                ReservationDetails = reservations
            });
        }

       
        [HttpGet]
        [Route("api/booking/viewbooking/{pnr}")]
        public IHttpActionResult ViewBooking(string pnr)
        {
            var reservation = db.Reservations
                                .Include("Train_Schedules")
                                .Include("Seat")
                                .Include("User")
                                .FirstOrDefault(r => r.pnr_number == pnr);

            if (reservation == null)
            {
                return NotFound();
            }

            var result = new
            {
                ReservationId = reservation.reservation_id,
                PnrNumber = reservation.pnr_number,
                Status = reservation.status,
                TotalFare = reservation.total_fare,
                BookedAt = reservation.booked_at,
                NumberOfPassengers = reservation.number_of_passengers,

                User = new
                {
                    reservation.User.name,
                    reservation.User.email,
                    reservation.User.phone
                },

                Seat = new
                {
                    reservation.Seat.seat_id,
                    reservation.Seat.seat_number
                },

                Schedule = new
                {
                    reservation.Train_Schedules.schedule_id,
                    reservation.Train_Schedules.train_id,
                    reservation.Train_Schedules.station_id,
                    reservation.Train_Schedules.journey_date,
                    reservation.Train_Schedules.arrival_time,
                    reservation.Train_Schedules.departure_time
                }
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("cancel-ticket")]
        public HttpResponseMessage CancelTicket(string pnr)
        {
            var reservations = _context.Reservations.Where(r => r.pnr_number == pnr).ToList();

            if (reservations == null || reservations.Count == 0)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid PNR Number!");

            foreach (var reservation in reservations)
            {
                reservation.status = "Cancelled";

                var seat = _context.Seats.FirstOrDefault(s => s.seat_id == reservation.seat_id);
                if (seat != null)
                {
                    seat.status = "Available";
                }

                decimal cancellationCharges = reservation.total_fare * 0.2m; // 20% Charges
                decimal refundAmount = reservation.total_fare - cancellationCharges; // 80% Refund

                var cancellation = new Cancellation
                {
                    reservation_id = reservation.reservation_id,
                    cancelled_at = DateTime.Now,
                    cancellation_date = DateTime.Now.Date,  // If you want only Date
                    cancellation_charges = cancellationCharges,
                    refund_amount = refundAmount
                };

                _context.Cancellations.Add(cancellation);
            }

            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Ticket Cancelled Successfully!");
        }





    }
}

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
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        private readonly RailwayReservationEntities1 dbcontext; // Changed to EDMX context
        private readonly IMailSendService emailService;

        public EmailController()
        {
            this.dbcontext = new RailwayReservationEntities1(); // EDMX context
            this.emailService = new MailService(); // Direct instantiation
        }

        [HttpPost]
        [Route("notify-user/{userId}")]
        public IHttpActionResult NotifyUser(int userId)
        {
            var user = dbcontext.Users.FirstOrDefault(u => u.user_id == userId);
            if (user == null)
                return NotFound();

            var reservations = dbcontext.Reservations
                .Where(r => r.user_id == userId)
                .Select(r => new
                {
                    r.pnr_number,
                    r.total_fare,
                    r.status,
                    r.number_of_passengers,
                    r.booked_at
                }).ToList();

            string reservationDetails = reservations.Any()
                ? string.Join("\n\n", reservations.Select(r => $@"
                PNR Number: {r.pnr_number}
                Total Fare: ₹{r.total_fare}
                Status: {r.status}
                No. of Passengers: {r.number_of_passengers}
                Booked At: {r.booked_at:yyyy-MM-dd HH:mm:ss}"))
                : "No reservations found.";

            var emailBody = $@"
Hi {user.name},

Thank you for registering with the Railway Reservation System!

Here are your registration details:
------------------------------------
Name: {user.name}
Email: {user.email}
Phone: {user.phone}
Gender: {user.gender}
Age: {user.age}
Address: {user.address}
Role: {user.role}
Registered On: {user.created_at:yyyy-MM-dd HH:mm:ss}

Your Reservation(s):
---------------------
{reservationDetails}

We're happy to serve you!

Regards,
Railway Reservation Team
";

            var emailDto = new EmailDto
            {
                To = user.email,
                Subject = "Your Registration & Reservation Details - Railway Reservation System",
                Body = emailBody
            };

            emailService.SendEmail(emailDto);

            return Ok("Detailed registration email sent to user.");
        }
    }
}


    


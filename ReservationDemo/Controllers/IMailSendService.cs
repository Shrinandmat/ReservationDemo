using ReservationDemo.DTOs;

namespace ReservationDemo.Controllers
{
    public interface IMailSendService
    {
        void SendEmail(EmailDto request);
    }
}
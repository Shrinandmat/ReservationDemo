using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationDemo.DTOs
{
    public class LoginResponseDto
    {
        public string Message { get; set; }
        public UserDto User { get; set; }
    }
}
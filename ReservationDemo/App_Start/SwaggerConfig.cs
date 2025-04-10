using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using ReservationDemo; 
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ReservationDemo
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Railway Reservation API");  // API Info
                })
                .EnableSwaggerUi();  // Swagger UI
        }
    }
}

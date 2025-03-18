using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MVC_Webserver.Models
{

    public class Appointment
    {
        // Properties
        public int AppointmentId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FK_donorId { get; set; }

    }
}

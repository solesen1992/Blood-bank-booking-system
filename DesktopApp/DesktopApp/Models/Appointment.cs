using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int FK_donorId { get; set; }
    }
}

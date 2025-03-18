using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    public class Appointment
    {
        private string? donorName;
        private string? donorCpr;
        private string? startTime1;
        private string? endTime1;

        public Appointment(int appointmentId, string? donorName, string? donorCpr, string? startTime1, string? endTime1)
        {
            AppointmentId = appointmentId;
            this.donorName = donorName;
            this.donorCpr = donorCpr;
            this.startTime1 = startTime1;
            this.endTime1 = endTime1;
        }

        public int AppointmentId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int FK_donorId { get; set; }
    }
}


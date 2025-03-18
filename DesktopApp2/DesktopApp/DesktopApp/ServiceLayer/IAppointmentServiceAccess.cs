using DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ServiceLayer
{
    public interface IAppointmentServiceAccess
    {
        bool DeleteAppointmentByStartTime(int donorId, DateTime startTime);
        Task<List<Appointment>?> GetDonorAppointments();
    }
}

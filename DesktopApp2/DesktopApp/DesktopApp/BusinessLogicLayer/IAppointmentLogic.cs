using DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.BusinessLogicLayer
{
    public interface IAppointmentLogic
    {
        Task<List<Appointment>?> GetAllAppointments();
        Task<List<Appointment>?> GetUpcomingAndWholeDayAppointments();
        Task<List<Appointment>?> SearchAppointments(string search);
        Task<Appointment?> GetAppointmentByCprNo(string cprNo);
        Task<Appointment?> GetAppointmentByCprNoOnlyOnUpcomingAppointsment(string cprNo);
        bool DeleteAppointmentByStartTime(int donorId, DateTime startTime);
    }
}

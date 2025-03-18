using DesktopApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp.BusinessLogicLayer
{
    public interface IDonorLogic
    {
        Task<List<Donor>?> GetAllDonors();
        Task<List<Donor>?> SearchDonor(string search);
        Task<Donor?> GetDonorByCprNo(string cprNo);
        Task<bool> UpdateDonor(string cprNo, Donor updatedDonor);

        // Appointment methods
        Task<List<Appointment>?> GetAllAppointments();
        Task<List<Appointment>?> GetUpcomingAndWholeDayAppointments();
        Task<List<Appointment>?> SearchAppointments(string search);
        Task<Appointment?> GetAppointmentByCprNo(string cprNo);
        Task<Appointment?> GetAppointmentByCprNoOnlyOnUpcomingAppointsment(string cprNo);
        bool DeleteAppointmentByStartTime(int donorId, DateTime startTime);

    }
}

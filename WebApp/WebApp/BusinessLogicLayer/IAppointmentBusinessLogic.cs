using WebApp.Models;

namespace WebApp.BusinessLogicLayer
{
    /** 
     * Defines the contract for appointment-related business logic.
     */
    public interface IAppointmentBusinessLogic
    {
        public bool IsValidAppointmentDate(DateTime startTime);
        public bool IsValidAppointmentTimes(DateTime startTime);
        public bool CreateAppointment(Appointment appointment);
        public List<string> GetUnavailableTimes(DateTime date);
        public List<Appointment> GetFutureAppointments();
        public List<Appointment> GetAppointmentsByDonorId(int donorId);
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime);
    }
}

using API.Models;

namespace API.BusinessLogicLayer
{
    public interface IAppointmentLogic
    {
        public List<Appointment> GetAppointments();
        public bool InsertAppointment(Appointment appointment, int donorId);
        public bool DeleteAppointment(Appointment appointment);
        Appointment GetAppointmentById(int id);
        public List<Appointment> GetAppointmentsByDonorId(int donorId);
        public bool DeleteAppointmentByStartTime(DateTime startTime);
    }
}

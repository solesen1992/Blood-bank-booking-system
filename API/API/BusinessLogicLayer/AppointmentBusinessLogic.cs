using API.Models;

namespace API.BusinessLogicLayer
{
    public class AppointmentBusinessLogic : IAppointmentBusinessLogic
    {
        public bool DeleteAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppointmentByStartTime(DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Appointment GetAppointmentById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            throw new NotImplementedException();
        }

        public bool InsertAppointment(Appointment appointment, int donorId)
        {
            throw new NotImplementedException();
        }
    }
}

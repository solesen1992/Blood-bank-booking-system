using WebApp.Models;

namespace WebApp.ServiceLayer
{
    /**
     * The IAppointmentService interface defines the contract for appointment-related operations.
     * It outlines the methods that must be implemented by any class handling appointment services.
     *
     * This interface ensures that the service layer provides functionality to create, retrieve, and delete appointments.
     */
    public interface IAppointmentService
    {
        public bool CreateAppointmentThroughApi(Appointment appointment);
        public List<Appointment> GetExistingAppointments();
        public List<Appointment> GetAppointmentsByDonorId(int donorId);
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime);
    }
}

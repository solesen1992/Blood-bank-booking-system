namespace API.DatabaseLayer
{
    /**
     * This interface defines the operations for managing appointment data within the database.
     * It includes methods for retrieving, inserting, updating, and deleting appointments.
     */
    public interface IAppointmentAccess
    {
        public List<Appointment> GetAppointments();
        public bool InsertAppointment(Appointment appointment, int donorId);
        public bool DeleteAppointment(Appointment appointment);
        Appointment GetAppointmentById(int id);
        public List<Appointment> GetAppointmentsByDonorId(int donorId);
        public bool DeleteAppointmentByStartTime(DateTime startTime);
    }
}

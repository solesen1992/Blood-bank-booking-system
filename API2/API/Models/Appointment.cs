namespace API.Models
{
    /* 
     * Appointment class represents an appointment entity in the system.
     * This class holds the details for a donor's appointment, including the 
     * appointment's start time, end time, and a reference to the donor associated 
     * with the appointment. 
     * 
     * Properties:
     * - `AppointmentId`: A unique identifier for the appointment.
     * `StartTime`: The date and time when the appointment starts.
     * - `EndTime`: The date and time when the appointment ends.
     * - `FK_donorId`: A foreign key reference to the donor associated with the appointment.
     */
    public class Appointment
    {
        // Properties
        public int AppointmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FK_donorId { get; set; }
    }
}

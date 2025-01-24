namespace API.DTOs
{
    /// <summary>
    /// CreateAppointmentDTO is a Data Transfer Object (DTO) used for creating an appointment.
    /// This class contains the necessary information for creating an appointment, including 
    /// the start and end time of the appointment, as well as the associated donor's ID.
    ///
    /// Properties:
    /// - `StartTime`: A DateTime value representing the start time of the appointment.
    /// - `EndTime`: A DateTime value representing the end time of the appointment.
    /// - `FK_donorId`: An integer representing the foreign key ID of the donor associated with the appointment.
    /// </summary>
    public class CreateAppointmentDTO
    {
        // Properties
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FK_donorId { get; set; }
    }
}

namespace API.DTOs
{
    /// <summary> 
    /// ReadAppointmentDTO is a Data Transfer Object (DTO) used for retrieving appointment data.
    /// This class combines the appointment details with donor information, such as the donor's name and CPR number.
    ///
    /// Properties:
    /// - `StartTime`: A DateTime value representing the start time of the appointment.
    /// - `EndTime`: A DateTime value representing the end time of the appointment.
    /// - `DonorFirstName`: A string representing the first name of the donor associated with the appointment.
    /// - `DonorLastName`: A string representing the last name of the donor associated with the appointment.
    /// - `CprNo`: A string representing the CPR number of the donor.
    /// </summary>
    public class ReadAppointmentDTO
    {
        // Appointment combined with donor information
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public string CprNo { get; set; }
    }
}

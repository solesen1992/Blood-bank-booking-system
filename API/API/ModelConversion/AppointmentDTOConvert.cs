using API.DTOs;
using API.Model;

namespace API.ModelConversion
{
    /// <summary>
    /// This class is responsible for converting Appointment model objects to their corresponding 
    /// Data Transfer Objects (DTOs) and vice versa. It handles both individual and list conversions 
    /// for different use cases such as desktop and web applications.
    /// </summary>
    public class AppointmentDTOConvert
    {
        /// <summary>
        /// Converts a single Appointment model object to a ReadAppointmentDTO, 
        /// which is used for displaying appointment details with donor information 
        /// for the desktop application.
        /// </summary>
        /// <param name="appointment">The Appointment model to convert.</param>
        /// <param name="donor">The Donor model used to populate donor information in the DTO.</param>
        /// <returns>A ReadAppointmentDTO containing appointment and donor details.</returns>
        public static ReadAppointmentDTO ToReadAppointmentDTO(Appointment appointment, Donor donor)
        {
            // Check if either appointment or donor is null.
            if (appointment == null || donor == null)
            {
                Console.WriteLine("Error: Appointment or Donor is null.");
                // Return null if any input is invalid.
                return null;
            }

            try
            {
                // Return a new ReadAppointmentDTO populated with appointment and donor details.
                return new ReadAppointmentDTO
                {

                    StartTime = appointment.StartTime, // Set StartTime from appointment model.
                    EndTime = appointment.EndTime,
                    DonorFirstName = donor.DonorFirstName, // Set DonorFirstName from donor model.
                    DonorLastName = donor.DonorLastName,
                    CprNo = donor.CprNo
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in converting AppointmentDTO: {ex.Message}");
                // Return null if an error occurs.
                return null;
            }
        }

        /// <summary>
        /// Converts a list of Appointment model objects into a list of ReadAppointmentDTOs.
        /// This method finds matching donor information for each appointment and returns 
        /// a list of DTOs for displaying appointments with donor details.
        /// </summary>
        /// <param name="appointments">A list of Appointment models to convert.</param>
        /// <param name="donors">A list of Donor models used to populate donor information in each DTO.</param>
        /// <returns>A list of ReadAppointmentDTOs containing appointment and donor details.</returns>
        public static List<ReadAppointmentDTO> ToReadAppointmentDTOList(List<Appointment> appointments, List<Donor> donors)
        {
            // Initialize a new list to store the DTOs of appointments
            var appointmentDTOList = new List<ReadAppointmentDTO>();

            foreach (var appointment in appointments)
            {
                // Find the donor related to this appointment (using FK_donorId)
                // It checks if the DonorId of the donor object d is equal to the FK_donorId of the current appointment object.
                var donor = donors.FirstOrDefault(d => d.DonorId == appointment.FK_donorId);

                // If the donor is found, map the appointment data and donor information into a DTO
                if (donor != null)
                {
                    var dto = new ReadAppointmentDTO
                    {
                        StartTime = appointment.StartTime,
                        EndTime = appointment.EndTime,
                        DonorFirstName = donor.DonorFirstName,
                        DonorLastName = donor.DonorLastName,
                        CprNo = donor.CprNo
                    };

                    appointmentDTOList.Add(dto);
                }
            }

            return appointmentDTOList;
        }

        /// <summary>
        /// Converts a CreateAppointmentDTO to an Appointment model object. 
        /// This method is used when creating a new appointment and associates it with a donor 
        /// through their ID.
        /// </summary>
        /// <param name="dto">The CreateAppointmentDTO containing the details of the new appointment.</param>
        /// <param name="donorId">The ID of the donor associated with the appointment.</param>
        /// <returns>An Appointment model object populated with the data from the DTO.</returns>
        public static Appointment ConvertToAppointment(CreateAppointmentDTO dto, int donorId)
        {
            // Return a new Appointment object.
            return new Appointment
            {
                StartTime = dto.StartTime,  // Set StartTime from CreateAppointmentDTO.
                EndTime = dto.EndTime,  // Set EndTime from CreateAppointmentDTO.
                FK_donorId = donorId  // Set FK_donorId to the given donorId.
            };
        }

        /// <summary>
        /// Optionally converts an Appointment model object to a CreateAppointmentDTO.
        /// This method is used for converting an existing Appointment model back into 
        /// a DTO format, useful for creating or editing appointments via APIs or UI forms.
        /// </summary>
        /// <param name="appointmentDTO">The CreateAppointmentDTO containing the details of the appointment.</param>
        /// <returns>An Appointment model object populated with the details of the CreateAppointmentDTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the CreateAppointmentDTO is null.</exception>
        public static Appointment ConvertToAppointment(CreateAppointmentDTO appointmentDTO)
        {
            if (appointmentDTO == null)
            {
                throw new ArgumentNullException(nameof(appointmentDTO), "CreateAppointmentDTO cannot be null.");
            }

            // Return a new Appointment object based on the CreateAppointmentDTO
            return new Appointment
            {
                StartTime = appointmentDTO.StartTime,    // Set StartTime from DTO
                EndTime = appointmentDTO.EndTime,        // Set EndTime from DTO
                FK_donorId = appointmentDTO.FK_donorId   // Set FK_donorId from DTO (this links to the donor)
            };
        }
    }
}

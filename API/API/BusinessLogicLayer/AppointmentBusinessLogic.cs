using API.Models;

namespace API.BusinessLogicLayer
{
    /// <summary>
    /// The <c>AppointmentLogic</c> class provides the business logic for managing appointments 
    /// and interacting with donors. It acts as an intermediary between the database access layer and the controller layer.
    /// 
    /// Key responsibilities include fetching appointment data along with donor details using data access interfaces, 
    /// mapping database entities to Data Transfer Objects (DTOs) for use in the application, and validating and inserting 
    /// new appointment data into the database.
    /// 
    /// The class depends on <c>IAppointmentAccess</c> for handling database operations 
    /// for appointments and <c>IDonorAccess</c> for providing access to donor-related data. Key methods include <c>GetAppointments</c>, 
    /// which retrieves a list of appointments and their associated donor data, converting them into DTOs for external use, and 
    /// 
    /// <c>InsertAppointment</c>, which validates and adds a new appointment to the database using data provided as a <c>CreateAppointmentDTO</c>. 
    /// This class relies on model conversion utilities to map between database entities and DTOs, ensuring clean separation of concerns.
    /// </summary>
    public class AppointmentBusinessLogic : IAppointmentBusinessLogic
    {
        private IAppointmentAccess _appointmentAccess;
        private IDonorAccess _donorAccess;

        /// <summary>
        /// Initializes a new instance of the <c>AppointmentLogic</c> class.
        /// Ensures the required dependency (<c>IAppointmentAccess</c>) is provided, and optionally accepts <c>IDonorAccess</c>.
        /// </summary>
        /// <param name="appointmentAccess">The appointment access layer.</param>
        /// <param name="donorAccess">The donor access layer (optional).</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="appointmentAccess"/> is null.</exception>
        public AppointmentLogic(IAppointmentAccess appointmentAccess, IDonorAccess donorAccess = null)
        {
            if (appointmentAccess == null)
            {
                // nameof(appointmentAccess) retrieves the name of the parameter ("appointmentAccess").
                throw new ArgumentNullException(nameof(appointmentAccess), "The appointmentAccess parameter cannot be null.");
            }
            _appointmentAccess = appointmentAccess;
            _donorAccess = donorAccess;
        }

        /// <summary>
        /// Retrieves all appointments along with their associated donor details.
        /// </summary>
        /// <returns>A list of <c>Appointment</c> objects containing appointment and donor information.</returns> 
        public List<Appointment> GetAppointments()
        {
            // Fetch the appointments along with the associated donor data
            var appointments = _appointmentAccess.GetAppointments();

            // Fetch the list of donors (e.g., from the database or a repository)
            var donors = _donorAccess.GetDonorsWithBloodTypeAndCity();

            return appointments;
        }

        /// <summary>
        /// Retrieves a specific appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to be retrieved.</param>
        /// <returns>The <c>Appointment</c> object if found, otherwise null.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="id"/> is invalid.</exception>
        public Appointment GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            // Fetch the appointment from the database using the data access layer
            var appointment = _appointmentAccess.GetAppointmentById(id);

            // Return the appointment if found, otherwise return null
            return appointment;
        }


        /// <summary>
        /// Inserts a new appointment into the database.
        /// </summary>
        /// <param name="appointment">The appointment object to insert.</param>
        /// <param name="donorId">The ID of the donor associated with the appointment.</param>
        /// <returns><c>true</c> if the appointment was successfully inserted; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="appointment"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="donorId"/> is invalid.</exception>
        public bool InsertAppointment(Appointment appointment, int donorId)
        {
            if (appointment == null)
            {
                // nameof(appointmentDTO) retrieves the name of the parameter("appointmentDTO").
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
            }
            // Validate the donor ID
            if (donorId <= 0)
            {
                throw new ArgumentException("Invalid donor ID.");
            }

            // Validate appointment data
            if (appointment.FK_donorId <= 0)
            {
                throw new ArgumentException("Invalid donor ID.");
            }

            // Pass the validated appointment to the database layer
            return _appointmentAccess.InsertAppointment(appointment, donorId);
        }

        /// <summary>
        /// Deletes an appointment from the database.
        /// </summary>
        /// <param name="appointment">The appointment object to delete.</param>
        /// <returns><c>true</c> if the appointment was successfully deleted; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="appointment"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="appointment"/> ID is invalid.</exception>
        public bool DeleteAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null.");
            }

            if (appointment.AppointmentId <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            // Call the data access layer to delete the appointment
            return _appointmentAccess.DeleteAppointment(appointment);
        }

        /// <summary>
        /// Retrieves a list of appointments for a specific donor.
        /// </summary>
        /// <param name="donorId">The ID of the donor for whom appointments are to be retrieved.</param>
        /// <returns>A list of <c>Appointment</c> objects associated with the donor.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="donorId"/> is invalid.</exception>
        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            // Validate the donor ID
            if (donorId <= 0)
            {
                throw new ArgumentException("Invalid donor ID.");
            }

            // Fetch the appointments for the donor from the database using the data access layer
            var appointments = _appointmentAccess.GetAppointmentsByDonorId(donorId);

            // Return the appointments if found, otherwise return an empty list
            return appointments ?? new List<Appointment>();
        }

        /// <summary>
        /// Deletes an appointment from the database based on the appointment start time.
        /// </summary>
        /// <param name="startTime">The start time of the appointment to be deleted.</param>
        /// <returns><c>true</c> if the appointment was successfully deleted; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="startTime"/> is null.</exception>
        public bool DeleteAppointmentByStartTime(DateTime startTime)
        {
            // Validate the start time
            if (startTime == null)
            {
                throw new ArgumentNullException(nameof(startTime), "Start time cannot be null.");
            }

            // Call the data access layer to delete the appointment
            return _appointmentAccess.DeleteAppointmentByStartTime(startTime);
        }
    }
}

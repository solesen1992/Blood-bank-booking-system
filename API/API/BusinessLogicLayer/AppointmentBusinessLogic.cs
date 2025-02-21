using API.DatabaseLayer;
using API.Models;

namespace API.BusinessLogicLayer
{
    /**
     * The `AppointmentLogic` class provides the business logic for managing appointments 
     * and interacting with donors. It acts as an intermediary between the database access layer and the controller layer.
     * 
     * Key responsibilities include fetching appointment data along with donor details using data access interfaces, 
     * mapping database entities to Data Transfer Objects (DTOs) for use in the application, and validating and inserting 
     * new appointment data into the database.
     * 
     * The class depends on `IAppointmentAccess` for handling database operations 
     * for appointments and `IDonorAccess` for providing access to donor-related data. Key methods include `GetAppointments`, 
     * which retrieves a list of appointments and their associated donor data, converting them into DTOs for external use, and 
     * 
     * `InsertAppointment`, which validates and adds a new appointment to the database using data provided as a `CreateAppointmentDTO`. 
     * This class relies on model conversion utilities to map between database entities and DTOs, ensuring clean separation of concerns.
     */
    public class AppointmentBusinessLogic : IAppointmentBusinessLogic
    {
        private IAppointmentAccess _appointmentAccess;
        private IDonorAccess _donorAccess;

        /**
         * Initializes a new instance of the `AppointmentLogic` class.
         * Ensures the required dependency (`IAppointmentAccess`) is provided, and optionally accepts `IDonorAccess`.
         * 
         * @param appointmentAccess The appointment access layer.
         * @param donorAccess The donor access layer (optional).
         * @throws ArgumentNullException Thrown when `appointmentAccess` is null.
         */
        public AppointmentBusinessLogic(IAppointmentAccess appointmentAccess, IDonorAccess donorAccess = null)
        {
            if (appointmentAccess == null)
            {
                // nameof(appointmentAccess) retrieves the name of the parameter ("appointmentAccess").
                throw new ArgumentNullException(nameof(appointmentAccess), "The appointmentAccess parameter cannot be null.");
            }
            _appointmentAccess = appointmentAccess;
            _donorAccess = donorAccess;
        }

        /**
         * Retrieves all appointments along with their associated donor details.
         * 
         * @return A list of `Appointment` objects containing appointment and donor information.
         */
        public List<Appointment> GetAppointments()
        {
            // Fetch the appointments along with the associated donor data
            var appointments = _appointmentAccess.GetAppointments();

            // Fetch the list of donors from the database
            var donors = _donorAccess.GetDonorsWithBloodTypeAndCity();

            return appointments;
        }

        /**
         * Retrieves a specific appointment by its ID.
         * 
         * @param id The ID of the appointment to be retrieved.
         * @return The `Appointment` object if found, otherwise null.
         * @throws ArgumentException Thrown when the `id` is invalid.
         */
        public Appointment GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            // Fetch the appointment from the database
            var appointment = _appointmentAccess.GetAppointmentById(id);

            // Return the appointment if found, otherwise return null
            return appointment;
        }


        /**
         * Inserts a new appointment into the database.
         * 
         * @param appointment The appointment object to insert.
         * @param donorId The ID of the donor associated with the appointment.
         * @return `true` if the appointment was successfully inserted; otherwise, `false`.
         * @throws ArgumentNullException Thrown when `appointment` is null.
         * @throws ArgumentException Thrown when `donorId` is invalid.
         */
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

        /**
         * Deletes an appointment from the database.
         * 
         * @param appointment The appointment object to delete.
         * @return `true` if the appointment was successfully deleted; otherwise, `false`.
         * @throws ArgumentNullException Thrown when `appointment` is null.
         * @throws ArgumentException Thrown when the `appointment` ID is invalid.
         */
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

        /**
         * Retrieves a list of appointments for a specific donor.
         * 
         * @param donorId The ID of the donor for whom appointments are to be retrieved.
         * @return A list of `Appointment` objects associated with the donor.
         * @throws ArgumentException Thrown when the `donorId` is invalid.
         */
        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            // Validate the donor ID
            if (donorId <= 0)
            {
                throw new ArgumentException("Invalid donor ID.");
            }

            // Fetch the appointments for the donor from the database
            var appointments = _appointmentAccess.GetAppointmentsByDonorId(donorId);

            // Return the appointments if found, otherwise return an empty list
            return appointments ?? new List<Appointment>();
        }

        /**
         * Deletes an appointment from the database based on the appointment start time.
         * 
         * @param startTime The start time of the appointment to be deleted.
         * @return `true` if the appointment was successfully deleted; otherwise, `false`.
         * @throws ArgumentNullException Thrown when `startTime` is null.
         */
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

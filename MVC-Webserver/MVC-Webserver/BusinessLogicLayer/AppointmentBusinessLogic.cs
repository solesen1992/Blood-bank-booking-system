using MVC_Webserver.Models;
using MVC_Webserver.Servicelayer;
using Newtonsoft.Json;


namespace MVC_Webserver.BusinessLogicLayer
{
    /// <summary>
    /// Implements the <see cref="IAppointmentBusinessLogic"/> interface and handles the business logic related to appointments.
    /// Responsible for validating appointment data, ensuring the integrity of appointment times, and interacting with the service layer 
    /// to create appointments and retrieve unavailable appointment times.
    /// Encapsulates the logic for validating time constraints (e.g., appointments cannot be in the past or too far in the future) 
    /// and communicates with the underlying service layer to perform CRUD operations on appointments.
    /// </summary>
    public class AppointmentBusinessLogic : IAppointmentBusinessLogic
    {
        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentBusinessLogic"/> class.
        /// </summary>
        /// <param name="appointmentService">The appointment service to interact with the service layer.</param>
        public AppointmentBusinessLogic(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Validates if the appointment's start time is within the allowable range (not more than 6 months
        /// in the future).
        /// </summary>
        /// <param name="startTime">The proposed start time of the appointment.</param>
        /// <returns>A boolean indicating whether the start time is valid.</returns>
        public bool IsValidAppointmentDate(DateTime startTime)
        {
            // Checks if the start time is within 6 months from the current date and time
            return startTime <= DateTime.Now.AddMonths(6);
        }

        /// <summary>
        /// Validates the start time of an appointment to ensure it meets business requirements.
        /// </summary>
        /// <param name="startTime">The start time of the appointment.</param>
        /// <returns>A boolean indicating whether the start time is valid.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the start time is not valid.</exception>
        public bool IsValidAppointmentTimes(DateTime startTime)
        {

            // Checks if the start time is set to the default value (which is not valid)
            if (startTime == default(DateTime) )
            {
                throw new InvalidOperationException("StartTime and EndTime must be set correctly.");
            }

            // Checks if the start time is in the past, which is not allowed
            if (startTime < DateTime.Now)
            {
                throw new InvalidOperationException("The time must be in the future.");
            }

            // Ensure the StartTime and EndTime are within SQL's datetime range
            if (startTime < new DateTime(1753, 1, 1))
            {
                throw new InvalidOperationException("StartTime and EndTime are out of SQL's valid date range.");
            }

            return true;
        }

        /// <summary>
        /// Creates an appointment by validating the provided details and using the service layer for API interaction.
        /// </summary>
        /// <param name="appointment">The appointment to be created.</param>
        /// <returns>A boolean indicating whether the appointment was successfully created.</returns>
        /// <exception cref="InvalidOperationException">Thrown if validation fails.</exception>
        public bool CreateAppointment(Appointment appointment)
        {
            bool result = false;
            // Validate StartTime and EndTime
            if (IsValidAppointmentTimes(appointment.StartTime))
            {
                // Validates if the appointment start time is within the allowable range
                if (IsValidAppointmentDate(appointment.StartTime)) {
                    // Sets the end time of the appointment (assuming a 30-minute duration for each appointment)
                    appointment.EndTime = appointment.StartTime.AddMinutes(30);

                    // Calls the service layer to create the appointment through the API and stores the result
                    result = _appointmentService.CreateAppointmentThroughApi(appointment);
                }
                else
                {
                    throw new InvalidOperationException("The start time can be at most 6 months in the future.");
                }
            }
            else
            {
                Console.WriteLine(appointment);
            }

            // Returns whether the appointment creation was successful or not
            return result;
        }

        /// <summary>
        /// Retrieves unavailable appointment times for a specific date.
        /// </summary>
        /// <param name="date">The date for which unavailable times are to be fetched.</param>
        /// <returns>A list of unavailable times in "HH:mm" format.</returns>
        public List<string> GetUnavailableTimes(DateTime date)
        {
            // Fetch all existing appointments from the service layer
            var appointments = _appointmentService.GetExistingAppointments();
            // If no appointments were fetched or the list is empty, return an empty list
            if (appointments == null || !appointments.Any())
            {
                Console.WriteLine("No appointments fetched.");
                return new List<string>();
            }

            // Filters the appointments for the given date and retrieves the start times in "HH:mm" format
            // Initialize a list to store unavailable time slots as strings.
            var unavailableTimes = new List<string>();

            // Loop through each appointment in the 'appointments' collection.
            foreach (var appointment in appointments)
            {
                // Check if the appointment's start date matches the specified 'date'.
                if (appointment.StartTime.Date == date.Date)
                {
                    // Convert the appointment's start time to a "HH:mm" format string (24-hour time).
                    var time = appointment.StartTime.ToString("HH:mm");

                    // Check if the 'time' is not already in the 'unavailableTimes' list to avoid duplicates.
                    if (!unavailableTimes.Contains(time))
                    {
                        // Add the time to the 'unavailableTimes' list.
                        unavailableTimes.Add(time);
                    }
                }
            }

            Console.WriteLine($"Unavailable times for {date.ToShortDateString()}: {string.Join(", ", unavailableTimes)}");
            return unavailableTimes;
        }

        /// <summary>
        /// Retrieves all future appointments.
        /// </summary>
        /// <returns>A list of <see cref="Appointment"/> objects that are scheduled for the future.</returns>
        public List<Appointment> GetFutureAppointments()
        {
            // Fetch all existing appointments from the service layer
            var appointments = _appointmentService.GetExistingAppointments();

            // Filter the appointments to include only those that are scheduled for the future
            var futureAppointments = new List<Appointment>();
            foreach (var appointment in appointments)
            {
                if (appointment.StartTime > DateTime.Now)
                {
                    futureAppointments.Add(appointment);
                }
            }
            // Log future appointments
            Console.WriteLine($"Future appointments: {JsonConvert.SerializeObject(futureAppointments)}");

            return futureAppointments;
        }


        /// <summary>
        /// Retrieves all appointments for a specific donor based on their donor ID.
        /// </summary>
        /// <param name="donorId">The ID of the donor whose appointments are to be fetched.</param>
        /// <returns>A list of <see cref="Appointment"/> objects associated with the specified donor ID.</returns>
        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            // Fetch appointments from the service layer based on the donor ID
            return _appointmentService.GetAppointmentsByDonorId(donorId);
        }


        /// <summary>
        /// Deletes an appointment by its start time if it is a future appointment.
        /// </summary>
        /// <param name="donorId">The ID of the donor associated with the appointment.</param>
        /// <param name="startTime">The start time of the appointment to delete.</param>
        /// <returns>A boolean indicating whether the appointment was successfully deleted.</returns>
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime)
        {
            // Call the service layer to delete the appointment by start time
            return _appointmentService.DeleteAppointmentByStartTime(donorId, startTime);
        }
    }
}

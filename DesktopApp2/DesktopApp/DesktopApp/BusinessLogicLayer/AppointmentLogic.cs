using DesktopApp.BusinessLogicLayer;
using DesktopApp.Model;
using DesktopApp.ServiceLayer;
using Microsoft.Extensions.Configuration;
using System.Globalization;

public class AppointmentLogic : IAppointmentLogic
{
    readonly IAppointmentServiceAccess _aCall;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentLogic"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public AppointmentLogic(IConfiguration configuration)
    {
        _aCall = new AppointmentServiceAccess(configuration);
    }

    /// <summary>
    /// Gets all appointments.
    /// </summary>
    /// <returns>A list of all appointments.</returns>
    public async Task<List<Appointment>?> GetAllAppointments()
    {
        // Initializes a variable to hold the retrieved appointments, starting as null.
        List<Appointment>? foundAppointments = null;

        // Checks if the _aCall object (service layer) is not null.
        if (_aCall != null)
        {
            // Calls the GetDonorAppointments method from the AppointmentServiceAccess class
            foundAppointments = await _aCall.GetDonorAppointments(); 
        }
        // Returns the retrieved appointments, or null if no appointments were found or _aCall was null.
        return foundAppointments;
    }

    /// <summary>
    /// Gets the upcoming and whole day appointments.
    /// </summary>
    /// <returns>A list of upcoming and whole day appointments.</returns>
    public async Task<List<Appointment>?> GetUpcomingAndWholeDayAppointments()
    {
        // Creates a new list of appointments
        List<Appointment>? upcomingAndWholeDayAppointments = new List<Appointment>();
        // Calls the GetAllAppointments method to get all appointments
        List<Appointment>? allAppointments = await GetAllAppointments();

        // If there are appointments
        if (allAppointments != null)
        {
            // Loops through all appointments
            foreach (var appointment in allAppointments)
            {
                // If the appointment start time is greater than or equal to the current date
                if (appointment.startTime.Date >= DateTime.Now.Date)
                {
                    // Adds the appointment to the list
                    upcomingAndWholeDayAppointments.Add(appointment);
                }
            }
            // Sorts the list of appointments based on their start time in ascending order.
            // The lambda expression '(a, b) => a.startTime.CompareTo(b.startTime)' is used as the comparison logic:
            // - 'a' and 'b' represent two Appointment objects being compared.
            // - 'a.startTime.CompareTo(b.startTime)' compares the 'startTime' of 'a' with that of 'b'.
            // - The result determines the order: negative value for 'a' before 'b', positive for 'b' before 'a', and 0 if they are equal.
            upcomingAndWholeDayAppointments.Sort((a, b) => a.startTime.CompareTo(b.startTime));
        }
        return upcomingAndWholeDayAppointments;
    }

    /// <summary>
    /// Searches the appointments based on the given search string.
    /// </summary>
    /// <param name="search">The search string.</param>
    /// <returns>A list of appointments that match the search criteria.</returns>
    public async Task<List<Appointment>?> SearchAppointments(string search)
    {
        // Creates a new list of appointments
        List<Appointment>? foundAppointments = new List<Appointment>();
        // Calls the GetAllAppointments method to get all appointments
        List<Appointment>? allAppointments = await GetAllAppointments();

        // If there are appointments
        if (allAppointments != null)
        {
            // Loops through all appointments
            foreach (var appointment in allAppointments)
            {
                // If the appointment contains the search string
                // Check if the donor's first name contains the search term.
                if (appointment.DonorFirstName.Contains(search, StringComparison.OrdinalIgnoreCase) || // StringComparison.OrdinalIgnoreCase makes the search case-insensitive
                    appointment.DonorLastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    // CultureInfo.InvariantCulture ensures that the date and time string representation is consistent
                    // and not affected by the user's locale settings
                    appointment.startTime.ToString(CultureInfo.InvariantCulture).Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    // Check if the appointment's end time, also formatted using InvariantCulture,
                    // contains the search term. StringComparison.OrdinalIgnoreCase ensures case insensitivity.
                    appointment.endTime.ToString(CultureInfo.InvariantCulture).Contains(search, StringComparison.OrdinalIgnoreCase))
                {
                    // If any of the above conditions are met, add the appointment to the foundAppointments list.
                    foundAppointments.Add(appointment);
                }
            }
        }
        return foundAppointments;
    }

    /// <summary>
    /// Gets the appointment by CPR number.
    /// </summary>
    /// <param name="cprNo">The CPR number.</param>
    /// <returns>The appointment that matches the CPR number.</returns>
    public async Task<Appointment?> GetAppointmentByCprNo(string cprNo)
    {
        // Initialize a variable to store the found appointment. Initially set to null.
        Appointment? foundAppointment = null;

        // Retrieve all appointments asynchronously using the GetAllAppointments method.
        List<Appointment>? allAppointments = await GetAllAppointments();

        // Check if the retrieved list of appointments is not null.
        if (allAppointments != null)
        {
            // Iterate through each appointment in the list.
            foreach (var appointment in allAppointments)
            {
                // Check if the current appointment's CPR number matches the given CPR number.
                if (appointment.CprNo == cprNo)
                {
                    // If a match is found, assign the current appointment to foundAppointment.
                    foundAppointment = appointment;
                    // Exit the loop early since the appointment has been found.
                    break;
                }
            }
        }
        // Returns the found appointment. If no match was found, this will return null.
        return foundAppointment;
    }

    /// <summary>
    /// Gets the appointment by CPR number only on upcoming appointments.
    /// </summary>
    /// <param name="cprNo">The CPR number.</param>
    /// <returns>The upcoming appointment that matches the CPR number.</returns>
    public async Task<Appointment?> GetAppointmentByCprNoOnlyOnUpcomingAppointsment(string cprNo)
    {
        // Initialize a variable to store the found appointment, initially set to null.
        Appointment? foundAppointment = null;

        // Retrieve the list of upcoming and whole-day appointments asynchronously.
        List<Appointment>? allAppointments = await GetUpcomingAndWholeDayAppointments();

        // Check if the retrieved list of appointments is not null.
        if (allAppointments != null)
        {
            // Iterate through each appointment in the list.
            foreach (var appointment in allAppointments)
            {
                // Check if the current appointment's CPR number matches the given CPR number.
                if (appointment.CprNo == cprNo)
                {
                    // If a match is found, assign the current appointment to foundAppointment.
                    foundAppointment = appointment;
                    // Exit the loop early since the desired appointment has been found.
                    break;
                }
            }
        }
        // Return the found appointment. If no match was found, this will return null.
        return foundAppointment;
    }

    /// <summary>
    /// Deletes an appointment by the donor's ID and the start time of the appointment.
    /// </summary>
    /// <param name="donorId">The ID of the donor.</param>
    /// <param name="startTime">The start time of the appointment to be deleted.</param>
    /// <returns><c>true</c> if the appointment was successfully deleted; otherwise, <c>false</c>.</returns>
    public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime)
    {
        // Call the service layer to delete the appointment by start time
        return _aCall.DeleteAppointmentByStartTime(donorId, startTime);
    }
}


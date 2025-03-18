using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DesktopApp.Model;

namespace DesktopApp.ServiceLayer
{
    /// <summary>
    /// AppointmentServiceAccess provides methods for interacting with the API to manage appointments.
    /// This class acts as a service layer between the desktop application and the API.
    /// It handles retrieving, deleting, and other operations related to appointments. 
    /// </summary>
    public class AppointmentServiceAccess : IAppointmentServiceAccess
    {
        readonly IDbnServiceConnection _dbnService;
        readonly string _baseUrl;

        /// <summary>
        /// Constructor that initializes the service connection using configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration object to fetch settings from.</param>
        public AppointmentServiceAccess(IConfiguration configuration)
        {
            _baseUrl = configuration["DonorService:BaseUrl"]; // Retrieve the base URL from configuration
            _dbnService = new DbnServiceConnection(_baseUrl); // Initialize the service connection
        }

        /// <summary>
        /// Fetches all appointments from the API.
        /// </summary>
        /// <returns>A list of appointments or null if an error occurs.</returns>
        public async Task<List<Appointment>?> GetDonorAppointments()
        {
            List<Appointment>? appointmentsFromService = null; // Initialize the list to store appointments

            // Check if _dbnService is not null to ensure that the DBN service connection is established
            if (_dbnService is not null)
            {
                // Set the API endpoint for fetching appointments
                _dbnService.UseUrl = $"{_dbnService.BaseUrl}/appointments";
                try
                {
                    // Call the API using GET method
                    var serviceResponse = await _dbnService.CallServiceGet();

                    // Check if the response is valid and successful
                    if (serviceResponse is not null && serviceResponse.IsSuccessStatusCode)
                    {
                        // If data is returned (200 OK)
                        if (serviceResponse.StatusCode == HttpStatusCode.OK)
                        {
                            // Read the JSON content from the response
                            string responseData = await serviceResponse.Content.ReadAsStringAsync();
                            // Deserialize the JSON into a list of Appointment objects
                            appointmentsFromService = JsonConvert.DeserializeObject<List<Appointment>>(responseData);
                        }
                        // If no content is returned (204 No Content)
                        else if (serviceResponse.StatusCode == HttpStatusCode.NoContent)
                        {
                            // Return an empty list if there are no appointments
                            appointmentsFromService = new List<Appointment>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions by setting the result to null
                    appointmentsFromService = null;
                }
            }
            // Return the list of appointments or null if an error occurred
            return appointmentsFromService;
        }

        /// <summary>
        /// Deletes an appointment for a specific donor based on their donor ID and the appointment's start time.
        /// </summary>
        /// <param name="donorId">The ID of the donor whose appointment is to be deleted.</param>
        /// <param name="startTime">The start time of the appointment to be deleted.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime)
        {
            // Creates a new HttpClient instance for sending requests.
            using (var client = new HttpClient())
            {
                try
                {
                    // Convert the starttime to a string in the desired format
                    string formattedStartTime = startTime.ToString("yyyy-MM-ddTHH:mm:ss");

                    // Send a DELETE request to delete the appointment by donor ID and starttime
                    var response = client.DeleteAsync($"{_dbnService.BaseUrl}/appointments/{donorId}/{formattedStartTime}").Result;

                    // Return a boolean indicating whether the deletion was successful
                    return response.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    // Log the error message
                    Console.WriteLine($"Error deleting appointment: {ex.Message}");
                    // Return false in case of an error
                    return false;
                }
            }
        }
    }
}
using System.Text;
using WebApp.Models;

namespace WebApp.ServiceLayer
{
    /// <summary>
    /// The AppointmentService class provides methods for interacting with the appointment-related endpoints 
    /// of an external API. 
    /// It acts as a service layer for handling appointment data and includes functionality for creating 
    /// and retrieving appointments.
    /// </summary>
    /// <remarks>
    /// - Uses the <see cref="HttpClient"/> class to make HTTP requests to the API.
    /// - Relies on <see cref="JsonConvert"/> for serializing and deserializing data between JSON and C# objects.
    /// - Retrieves the API base URL from configuration (e.g., appsettings.json) via dependency injection 
    /// using the <see cref="IConfiguration"/> interface.
    /// </remarks>
    public class AppointmentServicee : IAppointmentService
    {
        private readonly string apiUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentService"/> class.
        /// </summary>
        /// <param name="inConfiguration">The configuration interface to access app settings.</param>
        public AppointmentService(IConfiguration inConfiguration)
        {
            // Access the API URL from appsettings.json
            apiUrl = inConfiguration["ApiSettings:AppointmentApiUrl"];
        }

        /// <summary>
        /// Sends a new appointment to the API for creation.
        /// </summary>
        /// <param name="appointment">The <see cref="Appointment"/> object to be created.</param>
        /// <returns>A boolean indicating whether the API call was successful.</returns>
        public bool CreateAppointmentThroughApi(Appointment appointment)
        {
            // Using HttpClient to make the HTTP request to the API
            using (var client = new HttpClient())
            {
                try
                {
                    // Serializing the appointment object into JSON format (en tekst-streng)
                    var json = JsonConvert.SerializeObject(appointment);
                    Console.WriteLine(json);
                    // Creating content (body) for the POST request, Encoding.UTF8 bruges til at konvertere JSON-strengen til bytes (og tilbage)
                    // StringContent = The StringContent constructor accepts the JSON string, specifies the encoding
                    // and set the Content-type header to "application/json". That tells the API that the body of the request is in JSON format.
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Sending the POST request asynchronously to the API and waiting for the result from the server
                    // Sends it to the provided api endpoint with the provided data (content) as a byte stream over HTTPS via the network
                    var response = client.PostAsync(apiUrl, content).Result;

                    // Check if the response indicates success
                    return response.IsSuccessStatusCode;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Retrieves all existing appointments from the API.
        /// </summary>
        /// <returns>A list of <see cref="Appointment"/> objects fetched from the API. 
        /// Returns an empty list if an error occurs or the response is unsuccessful.</returns>
        public List<Appointment> GetExistingAppointments()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Sends a GET request to the specified API endpoint to fetch appointments
                    var response = client.GetAsync(apiUrl).Result;

                    // If the request is successful, deserialize the JSON response into a list of appointments
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the content of the HTTP response. The JSON response content is read as a string
                        // ReadAsStringAsync() is an asynchronous method, but the .Result is used here to block the thread and wait for it to complete
                        var json = response.Content.ReadAsStringAsync().Result;
                        // Deserializes the JSON string into a list of appointment objects
                        var appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);

                        // Log fetched appointments
                        // It converts the list of appointments objects into a JSON string.
                        Console.WriteLine($"Fetched appointments: {JsonConvert.SerializeObject(appointments)}");
                        return appointments;
                    }

                    // Return an empty list if not successful
                    return new List<Appointment>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching appointments: {ex.Message}");
                    // Return an empty list in case of an error
                    return new List<Appointment>();
                }
            }
        }

        /// <summary>
        /// Retrieves all appointments for a specific donor based on their donor ID by making an HTTP GET request to the API.
        /// </summary>
        /// <param name="donorId">The ID of the donor whose appointments are to be fetched.</param>
        /// <returns>A list of <see cref="Appointment"/> objects associated with the specified donor ID.</returns>
        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Send a GET request to fetch appointments by donor ID
                    var response = client.GetAsync($"{apiUrl}/donor/{donorId}").Result;

                    // If successful, deserialize the JSON response into a list of appointments
                    if (response.IsSuccessStatusCode)
                    {   // HttpClient modtager bytes (rå data) fra serverens HTTP-svar.
                        // ReadAsStringAsync konverterer bytestrømmen til en tekststreng (JSON-format)
                        var json = response.Content.ReadAsStringAsync().Result; // Responsen fra serveren er bytes (rå data), som skal læses og konverteres til en tekststreng (JSON)
                        // Deserialize the JSON string into a list of Appointment objects
                        var appointments = JsonConvert.DeserializeObject<List<Appointment>>(json); // Deserialisering af JSON kræver, at hele JSON-strengen er tilgængelig som én samlet tekststreng

                        // Log fetched appointments
                        Console.WriteLine($"Fetched appointments by donor ID: {JsonConvert.SerializeObject(appointments)}");
                        return appointments;
                    }

                    // Return an empty list if the request was not successful
                    return new List<Appointment>();
                }
                catch (Exception ex)
                {
                    // Log the error message
                    Console.WriteLine($"Error fetching appointments by donor ID: {ex.Message}");
                    // Return an empty list in case of an error
                    return new List<Appointment>();
                }
            }
        }


        /// <summary>
        /// Deletes an appointment for a specific donor based on their donor ID and the appointment's start time.
        /// </summary>
        /// <param name="donorId">The ID of the donor whose appointment is to be deleted.</param>
        /// <param name="startTime">The start time of the appointment to be deleted.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Convert the starttime to a string in the desired format
                    string formattedStartTime = startTime.ToString("yyyy-MM-ddTHH:mm:ss"); // To match server or database?

                    // Send a DELETE request to delete the appointment by donor ID and starttime
                    var response = client.DeleteAsync($"{apiUrl}/{donorId}/{formattedStartTime}").Result; // Shouldn't consider the byte stream here because all the content is in the URL and not in the body

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

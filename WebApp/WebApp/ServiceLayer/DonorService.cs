using System.Text;
using WebApp.Models;
using Newtonsoft.Json;

namespace WebApp.ServiceLayer
{
    /***
     * DonorService class is responsible for handling donor-related data operations, specifically
     * interacting with REST API to fetch and create a donor records. This service sends HTTP requests to the API,
     * serializes the donor data into JSON format, and processes the API response.
     ***/
    public class DonorService : IDonorService
    {
        private readonly string apiUrl;

        /***
         * Initializes a new instance of the DonorService class.
         * @param inConfiguration The configuration interface to access app settings.
         ***/
        public DonorService(IConfiguration inConfiguration)
        {
            // Access the API URL from appsettings.json
            apiUrl = inConfiguration["ApiSettings:DonorApiUrl"];
        }

        /***
         * Sends a POST request to the REST API to create a new donor.
         * It serializes the Donor object into a JSON string, sends it in the request body, and checks
         * the response from the API to determine if the operation was successful.
         * @param donor The Donor object containing the donor details to be created.
         * @returns The ID of the created donor if successful, or 0 if the operation fails or the API returns an error response.
         ***/
        public int CreateDonorThroughApi(Donor donor)
        {
            // Using HttpClient to make the HTTP request to the API
            using (var client = new HttpClient())
            {
                try
                {
                    // Serializing the donor object into JSON format (string)
                    var json = JsonConvert.SerializeObject(donor);
                    // application/json = the content is json, UTF-8 = the text should be coded as UTF-8
                    // Creating content for the POST request, with UTF-8 encoding and JSON media type so it can be sent in the body of a HTTP request
                    var content = new StringContent(json, Encoding.UTF8, "application/json"); // StringContent is used to send text based content as HTTP-content
                    // StringContent = It's not only a text string - it's a container that tells HttpClient how to send the text over HTTP.
                    // Sending the POST request asynchronously to the API and waiting for the result (not ascync). Response er af typen HttpResponseMessage
                    var response = client.PostAsync(apiUrl, content).Result; // PostAsync is async and doesnt block the main thread while it waits on an answer from the server

                    if (response.IsSuccessStatusCode) // Check if the response indicates success
                    {
                        // gets the id from the response
                        return GetIdFromCreatedDonor(response);
                    }
                    else
                    {
                        // Read the response content as a string asynchronously
                        var errorContent = response.Content.ReadAsStringAsync().Result;
                        // Throw an exception with a detailed error message
                        throw new Exception($"Failed to create donor. Status code: {response.StatusCode}, Error: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while creating the donor: {ex.Message}", ex);
                }
            }
        }


        /***
         * Extracts the ID of the newly created donor from the API response.
         * @param response The HttpResponseMessage object returned by the API after a successful POST request.
         * @returns The extracted donor ID if successful, or throws an exception with a descriptive error message if the response data cannot be parsed or an error occurs.
         ***/
        public int GetIdFromCreatedDonor(HttpResponseMessage response)
        {
            // Using HttpClient to make the HTTP request to the API
            using (var client = new HttpClient())
            {
                // Initialize the donor ID to 0 as the default value
                int id = 0;
                try
                {
                    // Check if the HTTP response indicates success
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseData = response.Content.ReadAsStringAsync().Result;
                        // Attempt to parse the response string into an integer to get the donor ID
                        id = int.Parse(responseData);

                    }
                    else
                    {
                        // If the response is not successful
                        // Read the response content as a string asynchronously
                        var errorContent = response.Content.ReadAsStringAsync().Result;
                        throw new Exception($"Failed to extract donor ID. Status code: {response.StatusCode}, Error: {errorContent}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while extracting the donor ID: {ex.Message}", ex);
                }
                // Return the parsed donor ID (0 if parsing failed)
                return id;
            }
        }

        /***
         * Retrieves a donor by its ID from the API.
         * @param id The ID of the donor to be fetched.
         * @returns The Donor object fetched from the API, or null if an error occurs or the response is unsuccessful.
         ***/
        public Donor GetDonorById(int id)
        {
            // Creates an instance of HttpClient to send the HTTP requests
            using (var client = new HttpClient())
            {
                try
                {
                    // Construct the full request URL for the donor by appending the ID to the base API URL
                    var requestUrl = $"{apiUrl}/{id}";
                    Console.WriteLine($"Requesting URL: {requestUrl}");

                    // Send a GET request to fetch the donor by ID
                    var response = client.GetAsync(requestUrl).Result;

                    // Checks if the HTTP response status code indicates success
                    // If successful, deserialize the JSON response into a donor object
                    if (response.IsSuccessStatusCode)
                    {
                        // Reads the response content as a string
                        var json = response.Content.ReadAsStringAsync().Result;
                        // Deserialize the JSON response/string into a Donor object
                        var donor = JsonConvert.DeserializeObject<Donor>(json);

                        // Log fetched donor information
                        Console.WriteLine($"Fetched donor: {JsonConvert.SerializeObject(donor)}");
                        return donor;
                    }

                    // Log the response status code if not successful
                    Console.WriteLine($"Failed to fetch donor. Status code: {response.StatusCode}");
                    return null;
                }
                catch (Exception ex)
                {
                    // If an exception occurs, log the error message
                    Console.WriteLine($"Error fetching donor by ID: {ex.Message}");
                    // Return null in case of an error
                    return null;
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Model;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace DesktopApp.ServiceLayer
{
    /// <summary>
    /// The DonorServiceAccess class serves as a service layer to connect to the donor service endpoint and retrieve
    /// donor data. It uses the DbnServiceConnection class to make HTTP requests to the donor service API.
    /// </summary>
    public class DonorServiceAccess : IDonorServiceAccess
    {
        readonly IDbnServiceConnection _dbnService; // The service connection object used to make HTTP requests.
       

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorServiceAccess"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object to retrieve settings from.</param>
        public DonorServiceAccess() 
        {
            _dbnService = new DbnServiceConnection(); 
        }

        /// <summary>
        /// Retrieves a list of donors from the donor service asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a nullable list of <see cref="Donor"/> objects.</returns>
        /*public async Task<List<Donor>?> GetDonors() // A nullable list of Donor objects wrapped in a Task for
                                                    // asynchronous execution
        {
            List<Donor>? DonorsFromService = null; // Initialize the list of donors to null.

            // Check if _dbnService is not null to ensure that the service connection is established
            if (_dbnService is not null) 
            {
                _dbnService.UseUrl = $"{_dbnService.BaseUrl}/donors"; // Set the URL to the base URL of the donor service.

                try
                {
                    var serviceResponse = await _dbnService.CallServiceGet(); // Make an asynchronous GET request to the donor service.

                    // Check if the service response is not null and the status code indicates success (200-299)
                    // if success (200-299)
                    if (serviceResponse is not null && serviceResponse.IsSuccessStatusCode) // If the response is successful
                    {
                        // If the status code is 200 OK
                        // 200 with data returned
                        if (serviceResponse.StatusCode == HttpStatusCode.OK)
                        {
                            string responseData = await serviceResponse.Content.ReadAsStringAsync(); // Reads the content of the string
                            DonorsFromService = JsonConvert.DeserializeObject<List<Donor>>(responseData); // Deserializes the JSON data into a list of donors
                        }
                        // If the response status code is 204 No Content
                        else if (serviceResponse.StatusCode == HttpStatusCode.NoContent)
                        {  
                            DonorsFromService = new List<Donor>(); // If no content is returned, create an empty list of donors.
                        }
                    }
                }
                catch (Exception ex)
                {
                    DonorsFromService = null; // If an exception occurs, set the list of donors to null.
                }
            }
            // Return the list of donors, which could be null if the request failed or no data was retrieved
            return DonorsFromService;
        }*/

        public async Task<List<Donor>?> GetDonors()
        {
            try
            {
                _dbnService.UseUrl = $"{_dbnService.BaseUrl}/donors";
                var serviceResponse = await _dbnService.CallServiceGet();

                if (serviceResponse?.IsSuccessStatusCode == true)
                {
                    string responseData = await serviceResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Donor>>(responseData);
                }

                if (serviceResponse?.StatusCode == HttpStatusCode.NoContent)
                {
                    // Hvis statuskoden er "NoContent", returner en tom liste
                    return new List<Donor>();
                }
                else
                {
                    // Hvis ikke, returner null
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves a donor by their CPR number from the donor service asynchronously.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a nullable <see cref="Donor"/> object.</returns>
        public async Task<Donor?> GetDonorByCprNo(string cprNo)
        {
            // Initializes the variable to hold the donor object from the service. It is nullable.
            Donor? donorFromService = null;

            // Check if _dbnService is not null to ensure the service connection is active and ready
            if (_dbnService is not null)
            {
                // Set the URL to the specific endpoint for fetching a donor by their CPR number
                _dbnService.UseUrl = $"{_dbnService.BaseUrl}/donors/cpr/{cprNo}";

                try
                {
                    // Make an asynchronous GET request to the donor service using the specified URL
                    var serviceResponse = await _dbnService.CallServiceGet();

                    // Check if the response is not null and the status code indicates a successful response (200-299)
                    if (serviceResponse is not null && serviceResponse.IsSuccessStatusCode)
                    {
                        // 200 with data returned
                        if (serviceResponse.StatusCode == HttpStatusCode.OK)
                        {
                            string responseData = await serviceResponse.Content.ReadAsStringAsync(); // Reads the content of the string
                            donorFromService = JsonConvert.DeserializeObject<Donor>(responseData);  // Deserializes the JSON data into a donor object
                        }
                    }
                }
                catch
                {
                    // If an exception occurs, set the donor object to null
                    donorFromService = null;
                }
            }
            // Return the retrieved donor object (can be null if not found or an error occurred)
            return donorFromService;
        }

        /// <summary>
        /// Updates a donor's information in the donor service asynchronously.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to update.</param>
        /// <param name="updatedDonor">The updated donor object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the 
        /// update was successful.</returns>
        public async Task<bool> UpdateDonor(string cprNo, Donor updatedDonor)
        {
            // Check if _dbnService is not null to ensure the service connection is active and ready
            if (_dbnService is not null)
            {
                // Set the URL for the PUT request
                _dbnService.UseUrl = $"{_dbnService.BaseUrl}/donors/{cprNo}";

                try
                {
                    // Serialize the updated donor object into JSON format
                    string jsonData = JsonConvert.SerializeObject(updatedDonor);
                    // Create a new instance of `StringContent` to prepare the data to be sent in the HTTP request body
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    // Call the CallServicePut method to make the PUT request
                    var serviceResponse = await _dbnService.CallServicePut(content);

                    // Return true if the response is successful (status code 200-299)
                    return serviceResponse != null && serviceResponse.IsSuccessStatusCode;
                }
                catch
                {
                    // Handle exceptions gracefully (log if needed)
                    return false;
                }
            }
            return false; // Return false if _dbnService is null
        }
    }
}

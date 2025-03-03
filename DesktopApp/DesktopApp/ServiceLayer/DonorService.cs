using DesktopApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ServiceLayer
{
    public class DonorService : IDonorService
    {
        // Hardcoded API URL directly in the Service Layer
        private readonly string apiUrl = "https://localhost:7050/api/donors";  // Directly specify the URL here


        public DonorService()
        {
        }

        public List<Donor> GetAllDonors()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<Donor>>(json);
                    }

                    Console.WriteLine($"Failed to fetch donors. Status code: {response.StatusCode}");
                    return new List<Donor>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching all donors: {ex.Message}");
                    return new List<Donor>();
                }
            }
        }

        // Fetch donor by CPR number
        public Donor GetDonorByCprNo(string cprNo)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync($"{apiUrl}/cpr/{cprNo}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        // Deserialize the response into a Donor object
                        var donor = JsonConvert.DeserializeObject<Donor>(json);

                        return donor;  // Return the donor object
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch donor. Status code: {response.StatusCode}");
                        return null;  // Return null if no donor is found
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching donor by CPR number: {ex.Message}");
                    return null;  // Return null if there is an error
                }
            }
        }
    }
}

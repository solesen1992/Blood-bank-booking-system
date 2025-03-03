using DesktopApp.Helpers;
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
        private readonly string apiUrl;

        public DonorService()
        {
            apiUrl = ConfigHelper.GetDonorApiUrl(); // Get URL from config
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
    }
}

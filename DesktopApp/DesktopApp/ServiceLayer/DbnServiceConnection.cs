using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopApp.ServiceLayer
{
    /// <summary>
    /// The DbnServiceConnection is a wrapper for making HTTP requests using HttpClient. It provides methods for
    /// making GET, POST, PUT, and DELETE operations on a specified URL, with UseUrl defining the target URL for each call.
    /// The class also encapsulates the connection logic to interact with the RESTful API.
    /// </summary>
    public class DbnServiceConnection : IDbnServiceConnection 
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="DbnServiceConnection"/> class.
        /// </summary>
        /// <param name="inBaseUrl">The base URL for the service connection.</param>
        public DbnServiceConnection()
        {
            //_httpClient = new HttpClient();
            // Load configuration directly in the constructor
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Set base path to the directory where the app runs.
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Load appsettings.json

            var configuration = builder.Build();
            BaseUrl = configuration["DonorService:BaseUrl"];

            UseUrl = BaseUrl;
        }
        //private HttpClient _httpClient { get; init; }
        public string? BaseUrl { get; init; } // Can only be set during object initialization (using init)

        public string? UseUrl { get; set; } // Holds the specific URL that will be used for each request.

        /// <summary>
        /// Makes an asynchronous GET request to the URL specified in <see cref="UseUrl"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="HttpResponseMessage"/> 
        /// representing the response.</returns>
        public async Task<HttpResponseMessage?> CallServiceGet()
        {
            HttpResponseMessage? hrm = null; // If the URL is not set, the method returns null.
            if (UseUrl != null) // If the URL is set, the method sends a GET request to the URL.
            {
                // Asynchronously sends an HTTP GET request to the URL stored in UseUrl using the HttpClient instance.
                hrm = await _httpClient.GetAsync(UseUrl);
            }
            return hrm; // returns an HttpResponseMessage representing the response.
        }

        /// <summary>
        /// Makes an asynchronous POST request, sending the provided content to the URL in <see cref="UseUrl"/>.
        /// </summary>
        /// <param name="postJson">The content to send in the POST request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="HttpResponseMessage"/> representing the response.</returns>
        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) // If UserUrl is set
            {
                hrm = await _httpClient.PostAsync(UseUrl, postJson); // Send a POST request to the URL in UseUrl
            }
            return hrm; // returns the response.
        }

        /// <summary>
        /// Sends data using an asynchronous HTTP PUT request.
        /// </summary>
        /// <param name="postJson">The content to send in the PUT request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="HttpResponseMessage"/> 
        /// representing the response.</returns>
        public async Task<HttpResponseMessage?> CallServicePut(StringContent postJson)
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) // If UseUrl is set
            {
                hrm = await _httpClient.PutAsync(UseUrl, postJson); // Send a PUT request to the URL in UseUrl
            }
            return hrm; // returns the response.
        }


        /// <summary>
        /// Sends an asynchronous HTTP DELETE request to the URL in <see cref="UseUrl"/>.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="HttpResponseMessage"/> representing the response.</returns>
        public async Task<HttpResponseMessage?> CallServiceDelete()
        {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null)
            {
                hrm = await _httpClient.DeleteAsync(UseUrl); // Send a DELETE request to the URL in UseUrl
            }
            return hrm; // returns the response.
        }
    }
}

namespace DesktopApp.ServiceLayer
{
    /// <summary>
    /// The DbnServiceConnection is a wrapper for making HTTP requests using HttpClient. It provides methods for
    /// making GET, POST, PUT, and DELETE operations on a specified URL, with UseUrl defining the target URL for each call.
    /// The class also encapsulates the connection logic to interact with the RESTful API.
    /// </summary>
    public class DbnServiceConnection : IDbnServiceConnection 
    {
        private readonly HttpClient _httpEnabler; // Is used to send and receive HTTP requests and responses.

        /// <summary>
        /// Initializes a new instance of the <see cref="DbnServiceConnection"/> class.
        /// </summary>
        /// <param name="inBaseUrl">The base URL for the service connection.</param>
        public DbnServiceConnection(string inBaseUrl)
        {
            _httpEnabler = new HttpClient(); 
            BaseUrl = inBaseUrl; 
            UseUrl = BaseUrl; // BaseUrl is set to this input URL, and UseUrl is initialized to the same value.
                              // This allows DBNConnection to use a default base URL, which can later be modified
                              // if needed.
        }

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
                hrm = await _httpEnabler.GetAsync(UseUrl);
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
                hrm = await _httpEnabler.PostAsync(UseUrl, postJson); // Send a POST request to the URL in UseUrl
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
                hrm = await _httpEnabler.PutAsync(UseUrl, postJson); // Send a PUT request to the URL in UseUrl
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
                hrm = await _httpEnabler.DeleteAsync(UseUrl); // Send a DELETE request to the URL in UseUrl
            }
            return hrm; // returns the response.
        }
    }
}

// Models/Login.cs
namespace MVC_Webserver.Models
{
    /// <summary>
    /// Represents the model for user login.
    /// </summary>
    public class Login
    {
        public string? Username { get; set; } // Can be null
        public string? Password { get; set; }
    }
}

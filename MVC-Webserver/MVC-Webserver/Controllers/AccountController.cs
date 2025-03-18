using Microsoft.AspNetCore.Mvc;
using MVC_Webserver.Models;

namespace MVC_Webserver.Controllers
{
    /// <summary>
    /// Handles user authentication-related actions such as login.
    /// Provides methods to display the login form and process login submissions.
    /// This controller currently uses hardcoded credentials for demonstration purposes.
    /// Note: This is purely for mock purposes.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Displays the login form for the user to enter their credentials.
        /// </summary>
        /// <returns>The login view.</returns>
        public IActionResult Login()
        {
            // Returns the 'Login' view, which contains the form for the user to enter their username and password.
            return View();
        }

        /// <summary>
        /// Handles the form submission for user login.
        /// This is a mock implementation that uses hardcoded credentials for demonstration purposes.
        /// </summary>
        /// <param name="model">The login view model containing the username and password.</param>
        /// <returns>Redirects to the DonorDetails view if login is successful, otherwise returns the login view with an error message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery (CSRF) attacks
        public IActionResult Login(Login model)
        {
            // Check if the model state is valid, which means all required fields are filled and meet validation criteria.
            if (ModelState.IsValid)
            {
                // Simulate login success by checking if the provided username and password match the hardcoded values.
                if (model.Username == "testuser" && model.Password == "password")
                {   // If the login is correct, the server responds with a cookie with the name cookieId and the value 2.
                    // If the credentials are correct, set a cookie with a hardcoded donor ID. Append adds the cookie to the HTTP response.
                    // This is a placeholder for actual authentication logic, which would typically involve checking a database.
                    Response.Cookies.Append("donorId", "7"); // Set the donor ID in a cookie. Cookie name and value. Response = servers response on clients request
                    
                    // Store a success message in TempData, which is a temporary storage that lasts until the next request.
                    TempData["LoginMessage"] = "Login successful!";

                    // Redirect to the DonorDetails action of the DonorController, passing the hardcoded donor ID.
                    // This simulates a successful login and navigation to the donor's details page.
                    return RedirectToAction("DonorDetails", "Donor", new { id = 2 });
                }
                else
                {
                    // If the credentials are incorrect, add an error message to the model state.
                    // This will be displayed on the login view to inform the user of the invalid login attempt.
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If the model state is not valid or the login attempt failed, return the login view with the current model.
            // This allows the user to correct any errors and try logging in again.
            return View(model);
        }
    }
}




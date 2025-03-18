using Microsoft.AspNetCore.Mvc;
using MVC_Webserver.BusinessLogicLayer;
using MVC_Webserver.Models;

namespace MVC_Webserver.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to donor operations (Create, Edit, Delete, etc.).
    /// Acts as an intermediary between the view (UI) and the business logic layer (DonorBusinessLogic).
    /// Validates the data and calls the appropriate methods in the business logic layer 
    /// to perform operations such as creating a donor, editing donor information, and handling responses.
    /// </summary>
    public class DonorController : Controller
    { //readonly when objects shouldn't change
        private readonly IDonorBusinessLogic _donorBusinessLogic;


        /// <summary>
        /// Initializes a new instance of the <see cref="DonorController"/> class and injects the business logic dependency.
        /// </summary>
        /// <param name="donorBusinessLogic">The business logic layer for donor operations, provided via dependency injection.</param>
        public DonorController(IDonorBusinessLogic donorBusinessLogic)
        {
            _donorBusinessLogic = donorBusinessLogic;
        }

        // GET: DonorController/Create
        /// <summary>
        /// Renders the view to create a new donor.
        /// </summary>
        /// <returns>The 'Create' view.</returns>
        public ActionResult Create()
        {
            // Returns the 'Create' view, where the user can fill out donor details.
            return View();
        }

        // POST: DonorController/Create
        /// <summary>
        /// Processes the form submission for creating a donor.
        /// Validates the data and calls the business logic to add the donor to the system.
        /// </summary>
        /// <param name="donor">The donor data submitted from the form.</param>
        /// <returns>A redirect to the Confirmation page if successful, otherwise the same view with error messages.</returns>
        [HttpPost]// This action handles HTTP POST requests
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery (CSRF) attacks
        public ActionResult Create(Donor donor)
        {
            // Check if the model state is valid (i.e., the form data passed all the validation rules)
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Donor Data: {donor.CprNo}, {donor.DonorFirstName}, {donor.DonorLastName}");
                // Ensure the CityZipCode object is not null (if not provided in the form, create a new one)
                if (donor.CityZipCode == null)
                {
                    donor.CityZipCode = new CityZipCode();
                }
                // Call the CreateDonor method in the business logic layer to add the donor to the system
                // The method returns the generated donor ID and an error message
                int id = _donorBusinessLogic.CreateDonor(donor, out string errorMessage);

                // Temporarily store the DonorId in TempData for use in the next request
                TempData["id"] = id;

                // Check if the donor creation was successful based on the returned ID
                bool isAdded = id > 0;

                // If the donor is successfully added, redirect to the Confirmation page
                if (isAdded)
                {
                    return RedirectToAction("Confirmation" );
                }
                else
                {
                    // If creation fails, add the error message to ModelState for display in the view
                    ModelState.AddModelError("", errorMessage);

                    // If insertion failed, return the same view with error message
                    return View(donor);
                }
            }
            // If the form data is invalid, redisplay the Create view with the current donor data
            return View(donor);
        }

        // GET: DonorController/Confirmation
        /// <summary>
        /// Renders the confirmation page once the donor has been successfully added.
        /// </summary>
        /// <returns>The 'Confirmation' view.</returns>
        public ActionResult Confirmation()
        {
            // Get the DonorId from TempData. It's saved as an object so we need to convert it to an int. If it's empty, it's null
            var donorId = TempData["id"] as int?;

            // Preserve TempData for subsequent requests
            TempData.Keep();
            // Check if donorId is null or 0, and handle it accordingly (e.g., show an error or redirect)
            if (donorId == null || donorId == 0)
            {
                return RedirectToAction("Index");
            }

            // Pass the DonorId to the view using Viewbag. Passes the donorId to the view so it can be for example displayed or used there.
            ViewBag.DonorId = donorId;

            // Renders the 'Confirmation' view
            return View();
        }


        // TO-DO
        // GET: DonorController/Index
        /// <summary>
        /// Renders the index page of the DonorController.
        /// </summary>
        /// <returns>The 'Index' view.</returns>
        public ActionResult Index() // ActionResult can return different kinds of things like HTML - it returns it to the client/browser
        {
            // Returns the 'Index' view
            return View();
        }

        // GET: DonorController/DonorDetails
        /// <summary>
        /// Retrieves and displays the details of a specific donor along with their appointments.
        /// </summary>
        /// <returns>The 'DonorDetails' view.</returns> 
        public ActionResult DonorDetails() // ActionResult returns a view that decides what the client sees in the browser
        {
            // Retrieve the donor ID from the cookie. The server reads cookies from Request.Cookies. The value is saved as a string. 
            var donorIdCookie = Request.Cookies["donorId"];

            // Check if the donor ID cookie is null, empty, or cannot be parsed into an integer. If it can be parsed to an integer, add it to "donorId"
            if (string.IsNullOrEmpty(donorIdCookie) || !int.TryParse(donorIdCookie, out int donorId)) //output parameter
            {
                // If the donor ID is not found in the cookie or is invalid, return an error or redirect
                return RedirectToAction("Index"); //Index page under Donor
            }

            // Retrieve the donor details and appointments from the business logic layer
            // Returns multiple values from a method
            var (donor, appointments) = _donorBusinessLogic.GetDonorDetailsWithAppointments(donorId);

            // Check if the donor exists
            if (donor == null)
            {
                // If the donor is not found, return a NotFound result
                return NotFound();
            }

            // Assign donor and appointments to ViewBag properties. From Controller to view.
            ViewBag.Donor = donor;
            ViewBag.Appointments = appointments;

            // Initialize ViewData
            ViewData["Title"] = "Donor Details";

            // Renders the 'DonorDetails' view
            return View();
        }

        // TO-DO
        // GET: DonorController/Edit/5
        /// <summary>
        /// Renders the view to edit a donor.
        /// </summary>
        /// <param name="id">The ID of the donor to edit.</param>
        /// <returns>The 'Edit' view.</returns>
        public ActionResult Edit(int id)
        {
            return View();
        }


        // TO-DO
        // POST: DonorController/Edit/5
        /// <summary>
        /// Processes the form submission for editing a donor.
        /// </summary>
        /// <param name="id">The ID of the donor to edit.</param>
        /// <param name="collection">The form collection with the updated donor data.</param>
        /// <returns>A redirect to the index page if successful, otherwise the same view with error messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // TO-DO
        /// <summary>
        /// Renders the view to delete a donor.
        /// </summary>
        /// <param name="id">The ID of the donor to delete.</param>
        /// <returns>The 'Delete' view.</returns>
        public ActionResult Delete(int id)
        {
            return View();
        }

        // TO-DO
        // POST: DonorController/Delete/5
        /// <summary>
        /// Processes the form submission for deleting a donor.
        /// </summary>
        /// <param name="id">The ID of the donor to delete.</param>
        /// <param name="collection">The form collection with the confirmation to delete.</param>
        /// <returns>A redirect to the index page if successful, otherwise the same view with error messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

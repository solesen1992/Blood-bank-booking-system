using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.BusinessLogicLayer;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Manages appointment-related actions in the MVC application.
    /// Handles the entire appointment creation process, including selecting a date, time, and confirming the appointment.
    /// Interacts with the business logic layer through the <see cref="IAppointmentBusinessLogic"/> interface for appointment data processing.
    /// </summary>
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBusinessLogic _businessLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class and injects the business logic dependency needed for appointment operations.
        /// The `IAppointmentBusinessLogic` interface is used to access the business logic layer without directly instantiating it.
        /// </summary>
        /// <param name="appointmentBusinessLogic">The business logic layer for appointment operations.</param>
        /// 

        public AppointmentController(IAppointmentBusinessLogic appointmentBusinessLogic)
        {
            _businessLogic = appointmentBusinessLogic;
        }

        // TO-DO
        // GET: AppointmentController
        /// <summary>
        /// Renders the index page of the AppointmentController.
        /// </summary>
        /// <returns>The 'Index' view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        // TO-DO
        // GET: AppointmentController/Details/5
        /// <summary>
        /// Retrieves and displays the details of a specific appointment by its ID
        /// and displays them in the 'Details' view.
        /// </summary>
        /// <param name="id">The ID of the appointment to display details for.</param>
        /// <returns>The 'Details' view.</returns>
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentController/CreateAppointment
        /// <summary>
        /// Prepares the page for creating a new appointment and fetches donor ID from TempData.
        /// </summary>
        /// <returns>The 'CreateAppointment' view.</returns>
        public ActionResult CreateAppointment()
        {
            // Retrieves donor ID from TempData
            // Attempts to retrieve the donor ID from TempData, casting it to an integer if it exists.
            int? donorId = TempData["id"] as int?;
            Console.WriteLine($"CreateAppointment + {donorId} "); // Log the donorId

            // Retrieves any error message from TempData, casting it to a string to pass to the view.
            ViewBag.Error = TempData["ErrorMessage"] as string;
            // Remove the error message from TempData after it's been used to prevent it from persisting unnecessarily
            TempData.Remove("ErrorMessage");

            // Keeps TempData for the next request
            TempData.Keep();
            // Returns the CreateAppointment view, rendering the view with the required data and handling any associated error message.
            return View();
        }

        // POST: AppointmentController/CreateAppointment
        /// <summary>
        /// Handles the form submission for creating an appointment and validates the selected date.
        /// </summary>
        /// <param name="Date">The selected date for the appointment.</param>
        /// <returns>A redirect to the CreateAppointmentTime page if successful, otherwise the same view with error messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment(string Date)
        {
            // Log the received date string to track user input
            Console.WriteLine($"CreateAppointment POST - Date: {Date}");

            // Tries to parse the selected date and store it in TempData if valid
            if (DateTime.TryParse(Date, out var selectedDate)) // Attempts to convert the provided Date string to a DateTime object
            {
                // Check if the parsed date is within the valid range
                if (_businessLogic.IsValidAppointmentDate(selectedDate))
                {

                    // Log the valid date for debugging purposes
                    Console.WriteLine($"Valid Date selected: {selectedDate}");

                    // Stores the selected date in TempData for retrieval in the next request
                    TempData["SelectedDate"] = selectedDate;
                    // Keeps TempData for the next request
                    TempData.Keep();

                    // Redirects to the next step in creating an appointment (selecting a time)
                    return RedirectToAction("CreateAppointmentTime");
                }

                // Set an error message in ViewBag to display on the view
                ViewBag.Error = "Datoen er ikke inden for 6 måneder";
            }
            else
            {
                // Log that an invalid date was provided
                Console.WriteLine("Invalid date selected.");
            }

            // Adds model validation error if the date is invalid and returns to the view. Modelstate is often used to check if the data from the user is valid.
            ModelState.AddModelError("Date", "Invalid date selected.");
            // Set the model state errors for display in the view
            ViewBag.Error = ModelState.ToString(); // Viewbag sends small parts of into from the controller to the view

            // Returns the CreateAppointment view if the date is invalid
            return View();
        }

        /// <summary>
        /// Prepares the page to select an appointment time after a valid date has been chosen.
        /// </summary>
        /// <returns>The 'CreateAppointmentTime' view.</returns>
        public ActionResult CreateAppointmentTime()
        {
            // Checks if TempData contains the required "SelectedDate"
            if (!TempData.ContainsKey("SelectedDate"))
            {
                TempData["ErrorMessage"] = "Required data is missing. Please start over.";
                // Redirects to the Index page if data is missing
                return RedirectToAction("Index");
            }
            // Keep TempData for the next request
            TempData.Keep();

            // Retrieves the selected date from TempData
            var selectedDate = (DateTime)TempData["SelectedDate"]; // casting to datetime since it's an object

            // Retrieves donor ID from TempData. Tries to take the data stored in "id". TempData stores data as object, the value is retrieved as an object.
            var donorId = TempData["id"] as int?; // tries to cast it to an int. If not, it's null.

            Console.WriteLine($"CreateAppointmentTime - SelectedDate: {selectedDate}, ");
            // Passes the selected date to the view
            ViewBag.SelectedDate = selectedDate;

            // Use business logic to retrieve unavailable times for the selected date
            var unavailableTimes = _businessLogic.GetUnavailableTimes(selectedDate);
            // Passes the unavailable times for the selected date to the view
            ViewBag.UnavailableTimes = unavailableTimes;

            // Returns the CreateAppointmentTime view
            return View();
        }

        /// <summary>
        /// Handles form submission for selecting an appointment time and validates it.
        /// </summary>
        /// <param name="Time">The selected time for the appointment.</param>
        /// <returns>A redirect to the Confirmation page if successful, otherwise the same view with error messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // Ensures the form submission is not a result of CSRF attack
        public ActionResult CreateAppointmentTime(string Time)
        {
            // Tries to retrieve the selected date from TempData
            if (TempData["SelectedDate"] is DateTime selectedDate)
            {
                Console.WriteLine($"CreateAppointmentTime POST - SelectedTime: {Time}");

                // Tries to parse the selected time and checks for its availability
                if (TimeSpan.TryParse(Time, out var selectedTime))
                {
                    // Combines date and time into a DateTime object
                    var appointmentDateTime = selectedDate.Add(selectedTime);

                    Console.WriteLine($"AppointmentDateTime: {appointmentDateTime}");

                    // Fetches unavailable times for the selected date
                    var unavailableTimes = _businessLogic.GetUnavailableTimes(selectedDate);

                    // Checks if the formatted appointment time (in hours and minutes) is in the list of unavailable times
                    if (unavailableTimes.Contains(appointmentDateTime.ToString("HH:mm")))
                    {
                        // Keeps TempData for the next request
                        TempData.Keep();

                        // Adds a message to TempData indicating the selected time is already taken
                        TempData["TakenTime"] = "Tiden er desværre taget, prøv med en ny tid";
                        // Redirects to the CreateAppointmentTime page for re-selection
                        return RedirectToAction("CreateAppointmentTime");
                    }
                    // Stores the appointment date and time in TempData
                    TempData["AppointmentDateTime"] = appointmentDateTime;

                    // Retrieves the donor ID from TempData to associate with the appointment
                    var donorId = TempData["id"] as int?;

                    // Redirects to the Confirmation action if the selected time is available
                    return RedirectToAction("Confirmation");
                }

                // If the selected time is invalid
                Console.WriteLine("Invalid time selected.");
                // Adds an error message to ModelState to be displayed on the CreateAppointment view
                ModelState.AddModelError("Time", "Invalid time selected.");
            }
            // If required data is missing or invalid, set an error message in TempData and redirect back to CreateAppointment
            TempData["ErrorMessage"] = "Invalid or missing data. Please try again.";
            // Redirects back to the CreateAppointment view if there is an error with the submitted time
            return RedirectToAction("CreateAppointment");
        }

        // GET: AppointmentController/Confirmation
        /// <summary>
        /// Displays a confirmation page with the final appointment details.
        /// </summary>
        /// <returns>The 'Confirmation' view.</returns>
        public ActionResult Confirmation()
        {
            // Retrieves the appointment date/time from TempData
            if (TempData["AppointmentDateTime"] is DateTime appointmentDateTime)
            {
                Console.WriteLine($"Confirmation - AppointmentDateTime: {appointmentDateTime},");
                // Retrieves the donor ID from TempData.
                var donorId = TempData["id"] as int?;
                // Creates a new Appointment object and initializes it with the donor ID and the
                // selected appointment time
                var appointment = new Appointment
                {
                    // Sets the foreign key (FK_donorId) to the retrieved donorId value
                    FK_donorId = (int)donorId,
                    // Sets the appointment start time to the retrieved date/time
                    StartTime = appointmentDateTime
                };

                Console.WriteLine($"Appointment Created: FK_donorId: {appointment.FK_donorId}, StartTime: {appointment.StartTime}");

                // Ensures donor ID is valid before proceeding
                if (donorId == 0)
                {
                    TempData["ErrorMessage"] = "Invalid Donor ID. Please try again.";
                    // Redirects if donor ID is invalid
                    return RedirectToAction("CreateAppointment");
                }

                // Attempts to save the appointment and returns to the confirmation page on success
                var success = _businessLogic.CreateAppointment(appointment);

                if (!success)
                {
                    // If the appointment creation failed,
                    // store an error message in TempData and redirect back to CreateAppointment view with the donorId
                    TempData["TakenTime"] = "Der opstod en fejl, prøv igen";
                    return RedirectToAction("CreateAppointment", new { donorId });
                }

                Console.WriteLine("Appointment successfully created.");
                // Success message in Danish
                ViewBag.SuccessMessage = $"Tak, du har booket følgende tid: {appointment.StartTime:yyyy-MM-dd HH:mm}. Vi glæder os til at se dig!";

                // Return the confirmation view with the appointment object
                return View(appointment);
            }
            // If the appointment date/time is not found in TempData, return an error message
            TempData["ErrorMessage"] = "Required data is missing. Please start over.";
            // Redirect to CreateAppointment if required data is missing
            return RedirectToAction("CreateAppointment");
        }

        // TO-DO
        /// <summary>
        /// Displays the form for editing an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to edit.</param>
        /// <returns>The 'Edit' view.</returns>
        public ActionResult Edit(int id)
        {
            return View();
        }

        // TO-DO
        // POST: AppointmentController/Edit/5
        /// <summary>
        /// Handles the submission of changes to an appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment being edited.</param>
        /// <param name="collection">The form data submitted by the user.</param>
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

        // GET: AppointmentController/Delete/5
        /// <summary>
        /// Displays the form for deleting an appointment.
        /// </summary>
        /// <param name="donorId">The ID of the donor associated with the appointment.</param>
        /// <param name="startTime">The start time of the appointment to delete.</param>
        /// <returns>The 'Delete' view.</returns>
        public ActionResult Delete(int donorId, DateTime startTime)
        {
            // Pass the donor ID and start time to the view using ViewBag
            ViewBag.DonorId = donorId;
            ViewBag.StartTime = startTime;

            // Returns the 'Delete' view, where the user can confirm the deletion of the appointment.
            return View();
        }

        // AppointmentController/DeleteAppointment
        /// <summary>
        /// Handles the form submission for deleting an appointment.
        /// Processes the deletion request, validates it, and calls the business logic to remove the appointment from the system.
        /// </summary>
        /// <param name="donorId">The ID of the donor associated with the appointment.</param>
        /// <param name="startTime">The start time of the appointment to delete.</param>
        /// <returns>A redirect to the DonorDetails view after processing the deletion.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against cross-site request forgery (CSRF) attacks
        public IActionResult DeleteAppointment(int donorId, DateTime startTime)
        {
            try
            {
                // Call the DeleteAppointmentByStartTime method from the business logic layer
                bool isDeleted = _businessLogic.DeleteAppointmentByStartTime(donorId, startTime);

                if (isDeleted)
                {
                    // If the deletion is successful, set a success message in TempData
                    TempData["SuccessMessage"] = "Appointment successfully deleted.";
                }
                else
                {
                    // If the appointment is not found, set an error message in TempData
                    TempData["ErrorMessage"] = "Appointment not found.";
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, set an error message in TempData
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            // Redirect back to the DonorDetails view
            return RedirectToAction("DonorDetails", "Donor", new { id = donorId });
        }

        // GET: AppointmentController
        /*public ActionResult Index()
        {
            return View();
        }

        // GET: AppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AppointmentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppointmentController/Edit/5
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

        // GET: AppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentController/Delete/5
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
        }*/
    }
}

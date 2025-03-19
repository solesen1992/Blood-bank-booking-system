using MVC_Webserver.Models;
using MVC_Webserver.Servicelayer;
using System.Text.RegularExpressions; // RegularExpression for CPR validation

namespace MVC_Webserver.BusinessLogicLayer
{
    /// <summary>
    /// Handles the core business logic related to donor operations.
    /// Acts as an intermediary between the controller and the service layer that interacts with the API.
    /// Ensures that all business rules or validations are applied before interacting with the database.
    /// </summary>
    public class DonorBusinessLogic : IDonorBusinessLogic
    {
        private readonly IDonorService _donorService;
        private readonly IAppointmentBusinessLogic _appointmentBusinessLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorBusinessLogic"/> class.
        /// </summary>
        /// <param name="inDonorService">The donor service to interact with the API.</param>
        /// <param name="appointmentBusinessLogic">The appointment business logic to interact with appointments.</param>
        public DonorBusinessLogic(IDonorService inDonorService, IAppointmentBusinessLogic appointmentBusinessLogic) 
        {
            _donorService = inDonorService; // Initialize the DonorService to interact with API.
            _appointmentBusinessLogic = appointmentBusinessLogic; // Initialize the AppointmentBusinessLogic to interact with appointments.
        }

        /// <summary>
        /// Creates a new donor. Business rules about CPR number, where CPR number must be filled out and must
        /// be valid. To check if the CPR number is valid, this method will call the <see cref="IsValidCpr()"/>. 
        /// Must be filled out before you can create a donor.
        /// </summary>
        /// <param name="donor">The donor to create.</param>
        /// <param name="errorMessage">The error message if the creation fails.</param>
        /// <returns>The ID of the created donor.</returns>
        public int CreateDonor(Donor donor, out string errorMessage) 
        {
            errorMessage = string.Empty;
            // Call the IsValidCpr method to validate the CPR number
            if ( !IsValidCpr(donor.CprNo)) 
            {
                errorMessage = "Invalid CPR number.";
                // Invalid CPR number
                return 0; // Exit if CPR validation fails
            }
                     
            // Call the CreateDonorThroughApi method of _donorService to attempt to add the donor.
            // The method returns a boolean indicating whether the donor was successfully added.
            int result = _donorService.CreateDonorThroughApi(donor);

            // If creation fails, return the generic error
            errorMessage = "An unexpected error occurred while creating the donor.";
            return result;           
        }

        /// <summary>
        /// Validates the CPR number, ensuring it does not contain a dash and follows the format DDMMYYXXXX.
        /// </summary>
        /// <param name="cpr">The CPR number to validate.</param>
        /// <returns>True if the CPR number is valid, otherwise false.</returns>
        static bool IsValidCpr(string cpr)
        {
            // Regex pattern to match exactly 10 digits (DDMMYYXXXX)
            string pattern = @"^\d{10}$";

            // Check if the 'cpr' string does not match the specified regular expression pattern.
            if (!Regex.IsMatch(cpr, pattern))
            {
                return false; // Invalid format
            }

            return true;
        }

        /// <summary>
        /// Retrieves a donor by their ID.
        /// This method interacts with the service layer to fetch the donor details from the API.
        /// </summary>
        /// <param name="id">The ID of the donor to retrieve.</param>
        /// <returns>The donor with the specified ID.</returns>
        public Donor GetDonorById(int id)
        {
            // Call the GetDonorByIdThroughApi method of _donorService to fetch the donor details
            var donor = _donorService.GetDonorById(id);

            // Return the donor object
            return donor;
        }

        /// <summary>
        /// Retrieves a donor and their associated appointments by donor ID.
        /// Interacts with the service layer to fetch the donor details from the API.
        /// If the donor is found, it also fetches the appointments related to the donor.
        /// </summary>
        /// <param name="donorId">The ID of the donor to retrieve.</param>
        /// <returns>A tuple containing the donor object and a list of their appointments. 
        /// If the donor is not found, both elements of the tuple will be null.</returns>
        public (Donor donor, List<Appointment> appointments) GetDonorDetailsWithAppointments(int donorId)
        {
            // Fetch the donor details using the donor ID
            var donor = GetDonorById(donorId);

            if (donor == null)
            {
                // Return null if the donor is not found
                return (null, null);
            }

            // Fetch the appointments related to the donor
            var appointments = _appointmentBusinessLogic.GetAppointmentsByDonorId(donorId);

            // Return a donor and their appointments
            return (donor, appointments);
        }
    }
}

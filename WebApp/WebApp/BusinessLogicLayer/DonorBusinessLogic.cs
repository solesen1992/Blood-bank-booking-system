using System.Text.RegularExpressions;
using WebApp.Models;
using WebApp.ServiceLayer;

namespace WebApp.BusinessLogicLayer
{
    /**
     * Handles the core business logic related to donor operations.
     * Acts as an intermediary between the controller and the service layer that interacts with the API.
     * Ensures that all business rules or validations are applied before interacting with the database.
     */
    public class DonorBusinessLogic : IDonorBusinessLogic
    {
        private readonly IDonorService _donorService;
        private readonly IAppointmentBusinessLogic _appointmentBusinessLogic;

        /**
         * Initializes a new instance of the DonorBusinessLogic class.
         * 
         * @param inDonorService The donor service to interact with the API.
         * @param appointmentBusinessLogic The appointment business logic to interact with appointments.
         */
        public DonorBusinessLogic(IDonorService inDonorService, IAppointmentBusinessLogic appointmentBusinessLogic)
        {
            _donorService = inDonorService; // Initialize the DonorService to interact with API.
            _appointmentBusinessLogic = appointmentBusinessLogic; // Initialize the AppointmentBusinessLogic to interact with appointments.
        }

        /**
         * Creates a new donor. Business rules about CPR number, where CPR number must be filled out and must
         * be valid. To check if the CPR number is valid, this method will call the IsValidCpr().
         * Must be filled out before you can create a donor.
         * 
         * @param donor The donor to create.
         * @param errorMessage The error message if the creation fails.
         * @return The ID of the created donor.
         */
        public int CreateDonor(Donor donor, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Call the IsValidCpr method to validate the CPR number
            if (!IsValidCpr(donor.CprNo))
            {
                errorMessage = "Invalid CPR number.";
                // Invalid CPR number
                return 0; // Exit if CPR validation fails
            }

            // Call the CreateDonorThroughApi method of _donorService to attempt to add the donor.
            // The method returns a donorId.
            int result = _donorService.CreateDonorThroughApi(donor);

            // If creation fails, return the generic error
            errorMessage = "An unexpected error occurred while creating the donor.";
            return result;
        }

        /**
         * Validates the CPR number, ensuring it does not contain a dash and follows the format DDMMYYXXXX.
         * 
         * @param cpr The CPR number to validate.
         * @return True if the CPR number is valid, otherwise false.
         */
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

        /**
         * Retrieves a donor by their ID.
         * This method interacts with the service layer to fetch the donor details from the API.
         * 
         * @param id The ID of the donor to retrieve.
         * @return The donor with the specified ID.
         */
        public Donor GetDonorById(int id)
        {
            // Call the GetDonorByIdThroughApi method of _donorService to fetch the donor details
            var donor = _donorService.GetDonorById(id);

            // Return the donor object
            return donor;
        }

        /**
         * Retrieves a donor and their associated appointments by donor ID.
         * Interacts with the service layer to fetch the donor details from the API.
         * If the donor is found, it also fetches the appointments related to the donor.
         * 
         * @param donorId The ID of the donor to retrieve.
         * @return A tuple containing the donor object and a list of their appointments.
         *         If the donor is not found, both elements of the tuple will be null.
         */
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

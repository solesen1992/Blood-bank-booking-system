using MVC_Webserver.Models;

namespace MVC_Webserver.BusinessLogicLayer
{

    /// <summary>
    /// Defines the contract for donor-related business logic.
    /// Ensures that all required business operations, such as creating a donor, are implemented.
    /// </summary>
    public interface IDonorBusinessLogic
    {
        /// <summary>
        /// Creates a new donor. Business rules about CPR number, where CPR number must be filled out and must
        /// be valid. To check if the CPR number is valid, this method will call the <see cref="IsValidCpr()"/>. 
        /// Must be filled out before you can create a donor.
        /// </summary>
        /// <param name="donor">The donor to create.</param>
        /// <param name="errorMessage">The error message if the creation fails.</param>
        /// <returns>The ID of the created donor.</returns>
        public int CreateDonor(Donor donor, out string errorMessage);

        /// <summary>
        /// Retrieves a donor by their ID.
        /// This method interacts with the service layer to fetch the donor details from the API.
        /// </summary>
        /// <param name="id">The ID of the donor to retrieve.</param>
        /// <returns>The donor with the specified ID.</returns>
        public Donor GetDonorById(int id);

        /// <summary>
        /// Retrieves a donor and their associated appointments by donor ID.
        /// Interacts with the service layer to fetch the donor details from the API.
        /// If the donor is found, it also fetches the appointments related to the donor.
        /// </summary>
        /// <param name="donorId">The ID of the donor to retrieve.</param>
        /// <returns>A tuple containing the donor object and a list of their appointments. If the donor is not found, both elements of the tuple will be null.</returns>
        public (Donor donor, List<Appointment> appointments) GetDonorDetailsWithAppointments(int donorId);


       
    }
}

using API.DatabaseLayer;
using API.Model;

namespace API.BusinessLogicLayer
{
    /// <summary>
    /// The <c>DonorLogic</c> class is responsible for handling the business logic related to donor operations.
    /// It provides methods for getting, creating, updating, and deleting donor information.
    /// This class acts as a bridge between the API controller and the data access layer (represented by the <c>DonorAccess</c> class).
    /// It encapsulates the logic for interacting with donor data and ensures that any operations performed on donor records are properly executed and validated.
    /// The class implements the <c>IDonorLogic</c> interface, which defines the contract for donor-related operations.
    /// </summary>
    public class DonorLogic : IDonorLogic
    {
        private  IDonorAccess _donorAccess;

        /// <summary>
        /// Initializes a new instance of the <c>DonorLogic</c> class.
        /// Initializes the data access layer dependency for donor operations.
        /// </summary>
        /// <param name="donorAccess">The data access interface for donor-related operations.</param>
        public DonorLogic(IDonorAccess donorAccess)
        {
            _donorAccess = donorAccess;
        }

        /// <summary>
        /// Retrieves a list of donors along with their blood type and city information.
        /// Converts the donor data into a list of DTOs for use in the application.
        /// </summary>
        /// <returns>A list of <c>Donor</c> objects containing donor details.</returns>
        public List<Donor> GetDonors()
        {
            var donors = _donorAccess.GetDonorsWithBloodTypeAndCity();
            return donors;
        }

        /// <summary>
        /// Inserts a new donor into the database.
        /// Validates the donor object and forwards it to the data access layer for insertion.
        /// </summary>
        /// <param name="donor">The donor object to insert.</param>
        /// <returns><c>true</c> if the donor was successfully inserted; otherwise, <c>false</c>.</returns>
        public bool InsertDonor(Donor donor)
        {
            // No donor object was found - Donor cannot be inserted
            if (donor == null) return false;

            // If there's a donor object, try to insert the donor
            return _donorAccess.InsertDonor(donor);
        }

        /// <summary>
        /// Checks if a CPR number is already registered in the system.
        /// </summary>
        /// <param name="cprNo">The CPR number to check.</param>
        /// <returns><c>true</c> if the CPR number exists; otherwise, <c>false</c>.</returns>
        public bool IsCprNoAlreadyRegistered(string cprNo)
        {
            return _donorAccess.DoesCprNoExist(cprNo);
        }

        /// <summary>
        /// Retrieves the donor ID associated with a given CPR number.
        /// Throws an exception if no donor is found with the provided CPR number.
        /// </summary>
        /// <param name="cprNo">The CPR number to search for.</param>
        /// <returns>The donor ID corresponding to the CPR number.</returns>
        /// <exception cref="Exception">Thrown when no donor is found with the provided CPR number.</exception>
        public int GetDonorIdByCprNo(string cprNo)
        {
            // Call the _donorAccess service to retrieve a Donor object by their CPR number.
            Donor donor = _donorAccess.GetDonorByCprNo(cprNo);

            if (donor != null)
            {
                // If the donor is found, return their DonorId. 
                // The cast to (int) ensures the DonorId is returned as an integer type.
                return (int)donor.DonorId;
            }
            else
            {
                // If no donor was found, throw an exception
                throw new Exception("Donor not found with the provided CPR number.");
            }
        }

        /// <summary>
        /// Updates an existing donor's details in the database.
        /// Converts the updated donor object into a DTO for external use.
        /// </summary>
        /// <param name="donor">The donor object with updated details.</param>
        /// <returns>The updated <c>Donor</c> object.</returns>
        public Donor UpdateDonor(Donor donor)
        {
            // Call the data access layer to update the donor and return the updated donor.
            return _donorAccess.UpdateDonor(donor);
        }

        /// <summary>
        /// Deletes a donor from the database.
        /// </summary>
        /// <param name="donor">The donor object to be deleted.</param>
        /// <returns><c>true</c> if the donor was successfully deleted; otherwise, <c>false</c>.</returns>
        public bool DeleteDonor(Donor donor)
        {
            // Initialize a boolean variable to track whether the donor was successfully deleted.
            bool wasDeleted = false;

            // Call the DeleteDonor method on the _donorAccess object, passing the donor to be deleted.
            // If the method returns true (indicating the deletion was successful), set wasDeleted to true.
            if (_donorAccess.DeleteDonor(donor))
            {
                wasDeleted = true;
            }
            // Return the value of wasDeleted, indicating whether the donor was successfully deleted or not.
            return wasDeleted;
        }

        /// <summary>
        /// Retrieves a donor's details using their CPR number.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor.</param>
        /// <returns>The <c>Donor</c> object containing the donor's details.</returns>
        public object GetDonorByCprNo(string cprNo)
        {
            return _donorAccess.GetDonorByCprNo(cprNo);
        }

        /// <summary>
        /// Retrieves a donor's details using their donor ID.
        /// </summary>
        /// <param name="donorId">The ID of the donor.</param>
        /// <returns>The <c>Donor</c> object containing the donor's details.</returns>
        public Donor GetDonorById(int donorId)
        {
            return _donorAccess.GetDonorById(donorId);
        }
    }
}

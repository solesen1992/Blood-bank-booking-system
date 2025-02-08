namespace API.BusinessLogicLayer
{
    public class DonorBusinessLogic : IDonorBusinessLogic
    {
        /**
         * The `DonorLogic` class is responsible for handling the business logic related to donor operations.
         * It provides methods for getting, creating, updating, and deleting donor information.
         * 
         * This class acts as a bridge between the API controller and the data access layer (represented by the `DonorAccess` class).
         * It encapsulates the logic for interacting with donor data and ensures that any operations performed on donor records 
         * are properly executed and validated.
         * 
         * The class implements the `IDonorLogic` interface, which defines the contract for donor-related operations.
         */
        private IDonorAccess _donorAccess;

        /**
         * Initializes a new instance of the `DonorLogic` class.
         * Initializes the data access layer dependency for donor operations.
         * 
         * @param donorAccess The data access interface for donor-related operations.
         */
        public DonorLogic(IDonorAccess donorAccess)
        {
            _donorAccess = donorAccess;
        }

        /**
         * Retrieves a list of donors along with their blood type and city information.
         * Converts the donor data into a list of DTOs for use in the application.
         * 
         * @return A list of `Donor` objects containing donor details.
         */
        public List<Donor> GetDonors()
        {
            var donors = _donorAccess.GetDonorsWithBloodTypeAndCity();
            return donors;
        }

        /**
         * Inserts a new donor into the database.
         * Validates the donor object and forwards it to the data access layer for insertion.
         * 
         * @param donor The donor object to insert.
         * @return `true` if the donor was successfully inserted; otherwise, `false`.
         */
        public bool InsertDonor(Donor donor)
        {
            // No donor object was found - Donor cannot be inserted
            if (donor == null) return false;

            // If there's a donor object, try to insert the donor
            return _donorAccess.InsertDonor(donor);
        }

        /**
         * Checks if a CPR number is already registered in the system.
         * 
         * @param cprNo The CPR number to check.
         * @return `true` if the CPR number exists; otherwise, `false`.
         */
        public bool IsCprNoAlreadyRegistered(string cprNo)
        {
            return _donorAccess.DoesCprNoExist(cprNo);
        }

        /**
         * Retrieves the donor ID associated with a given CPR number.
         * Throws an exception if no donor is found with the provided CPR number.
         * 
         * @param cprNo The CPR number to search for.
         * @return The donor ID corresponding to the CPR number.
         * @throws Exception Thrown when no donor is found with the provided CPR number.
         */
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

        /**
         * Updates an existing donor's details in the database.
         * 
         * @param donor The donor object with updated details.
         * @return The updated `Donor` object.
         */
        public Donor UpdateDonor(Donor donor)
        {
            // Call the data access layer to update the donor and return the updated donor.
            return _donorAccess.UpdateDonor(donor);
        }

        /**
         * Deletes a donor from the database.
         * 
         * @param donor The donor object to be deleted.
         * @return `true` if the donor was successfully deleted; otherwise, `false`.
         */
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

        /**
         * Retrieves a donor's details using their CPR number.
         * 
         * @param cprNo The CPR number of the donor.
         * @return The `Donor` object containing the donor's details.
         */
        public object GetDonorByCprNo(string cprNo)
        {
            return _donorAccess.GetDonorByCprNo(cprNo);
        }

        /**
         * Retrieves a donor's details using their donor ID.
         * 
         * @param donorId The ID of the donor.
         * @return The `Donor` object containing the donor's details.
         */
        public Donor GetDonorById(int donorId)
        {
            return _donorAccess.GetDonorById(donorId);
        }

    }
}

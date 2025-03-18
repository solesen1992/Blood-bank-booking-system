using API.Model;

namespace API.DatabaseLayer
{

    /// <summary>
    /// The IDonorAccess interface defines the contract for accessing and manipulating donor data in the database.
    /// It includes methods for retrieving donors based on specific criteria (blood type and city), inserting, updating, 
    /// and deleting donor records. Implementing classes will provide the actual logic for interacting with the database 
    /// to perform these operations.
    /// </summary>
    public interface IDonorAccess
    {
        public List<Donor> GetDonorsWithBloodTypeAndCity();
        public Donor GetDonorByCprNo(string cprNo);
        public bool InsertDonor(Donor donor);

        bool DoesCprNoExist(string cprNo);
        public Donor UpdateDonor(Donor donor);

        public bool DeleteDonor(Donor donor); 
        public Donor GetDonorById(int donorId);
    }
}

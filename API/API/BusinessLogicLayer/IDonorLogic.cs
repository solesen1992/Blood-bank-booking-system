using API.Model;

namespace API.BusinessLogicLayer
{
    /// <summary>
    /// The <c>IDonorLogic</c> interface defines the contract for the business logic layer related to donor operations.
    /// It specifies the methods that need to be implemented by any class that handles donor data management.
    /// </summary>
    public interface IDonorLogic
    {
        public List<Donor> GetDonors();
        public int GetDonorIdByCprNo(string cprNo);
        bool IsCprNoAlreadyRegistered(string cprNo);

        public Donor UpdateDonor(Donor donor);
        public bool DeleteDonor(Donor donor);

        public bool InsertDonor(Donor donor);
        object GetDonorByCprNo(string cprNo);
        public Donor GetDonorById(int donorId);
    }

}

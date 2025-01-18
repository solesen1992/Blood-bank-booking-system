using API.Models;

namespace API.BusinessLogicLayer
{
    public interface IDonorBusinessLogic
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

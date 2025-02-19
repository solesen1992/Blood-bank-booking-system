using WebApp.Models;

namespace WebApp.ServiceLayer
{
    /**
     * The IDonorService interface defines the contract for donor-related operations in the service layer.
     * It ensures that any implementing class provides functionality for interacting with donor data,
     * currently focusing on donor creation and retrieval.
     */
    public interface IDonorService
    {
        public int CreateDonorThroughApi(Donor donor);
        public int GetIdFromCreatedDonor(HttpResponseMessage response);
        public Donor GetDonorById(int id);
    }
}

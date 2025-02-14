using WebApp.Models;

namespace WebApp.BusinessLogicLayer
{
    /** 
     * Defines the contract for donor-related business logic.
     * Ensures that all required business operations, such as creating a donor, are implemented.
     */
    public interface IDonorBusinessLogic
    {
        public int CreateDonor(Donor donor, out string errorMessage);

        public Donor GetDonorById(int id);

        public (Donor donor, List<Appointment> appointments) GetDonorDetailsWithAppointments(int donorId);
    }
}

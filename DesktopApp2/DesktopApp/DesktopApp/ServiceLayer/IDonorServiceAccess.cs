using DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ServiceLayer
{
    public interface IDonorServiceAccess
    {
        Task<List<Donor>?> GetDonors();
        Task<Donor?> GetDonorByCprNo(string cprNo);
        Task<bool> UpdateDonor(string cprNo, Donor updatedDonor);
    }
}

using DesktopApp.Models;
using DesktopApp.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.BusinessLogicLayer
{
    public class DonorLogic : IDonorLogic
    {
        private readonly IDonorService _donorService;

        public DonorLogic()
        {
            _donorService = new DonorService();
        }

        public Donor GetDonorDetails(string cprNo)
        {
            throw new NotImplementedException();
        }

        public List<Donor> GetDonors()
        {
            return _donorService.GetAllDonors();
        }

        List<Donor> IDonorLogic.GetDonors()
        {
            throw new NotImplementedException();
        }

        /*public Donor GetDonorDetails(string cprNo)
        {
            return _donorService.GetDonorByCprNo(cprNo);
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.BusinessLogicLayer
{
    public interface IDonorLogic
    {
        public List<Donor> GetDonors()

        public Donor GetDonorDetails(string cprNo)
    }
}

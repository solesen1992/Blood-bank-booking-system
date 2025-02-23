using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class Donor
    {
        public int DonorId { get; set; }
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int DonorPhoneNo { get; set; }
        public string DonorEmail { get; set; }
        public string DonorStreet { get; set; }
        //public int FK_CityZipCodeId { get; set; }
        //public int FK_BloodTypeId { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string BloodType { get; set; }
    }
}

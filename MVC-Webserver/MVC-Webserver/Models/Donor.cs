namespace MVC_Webserver.Models
{
    /// <summary>
    /// Represents a donor with personal and contact information.
    /// </summary>
    public class Donor
    {

        //Properties
        private CityZipCode cityZipCode_City;
        public int DonorId { get; set; }
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int DonorPhoneNo { get; set; }
        public string DonorEmail { get; set; }
        public string DonorStreet { get; set; }
        public int FK_CityZipCodeId { get; set; }
        public int? FK_BloodTypeId { get; set; } // 
        public BloodTypeEnum? BloodType { get; set; } // ? makes the enum nullable
        public CityZipCode CityZipCode { get; set; }

        // Helper properties for City and ZipCode
        public string City
        {
            get
            {
                return CityZipCode != null ? CityZipCode.City : null; //return CityZipCode.City or null
            }
        }

        public int ZipCode
        {
            get
            {
                return CityZipCode != null ? CityZipCode.ZipCode : 0; //return CityZipCode.ZipCode or null
            }
        }


    }
}

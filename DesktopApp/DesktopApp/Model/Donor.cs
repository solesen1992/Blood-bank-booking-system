using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
    public class Donor
    {
        private CityZipCode cityZipCode_City;
        public int DonorId { get; set; }
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int DonorPhoneNo { get; set; }
        public string DonorEmail { get; set; }
        public string DonorStreet { get; set; }
        public int FK_CityZipCodeId { get; set; }
        public int? FK_BloodTypeId { get; set; } 
        public BloodTypeEnum? BloodType { get; set; } // ? makes the enum nullable
        public CityZipCode? CityZipCode { get; set; }

        // Helper properties for City and ZipCode

        // This property retrieves the name of the city.
        // It uses a null-conditional operator to access the `City` property of the `CityZipCode` object.
        // If `CityZipCode` is not null, it returns the `City` property, otherwise, it returns null.
        // This allows for handling cases where `CityZipCode` might not be initialized or set.
        public string? City
        {
            get
            {
                return CityZipCode != null ? CityZipCode.City : null;
            }
        }

        // This property retrieves the zip code.
        // It uses a null-conditional operator to access the `ZipCode` property of the `CityZipCode` object.
        // If `CityZipCode` is not null, it returns the `ZipCode` property, otherwise, it returns 0.
        // The value 0 serves as a default, which is useful in scenarios where `CityZipCode` is not initialized or set.
        public int ZipCode
        {
            get
            {
                return CityZipCode != null ? CityZipCode.ZipCode : 0;
            }
        }
    }
}



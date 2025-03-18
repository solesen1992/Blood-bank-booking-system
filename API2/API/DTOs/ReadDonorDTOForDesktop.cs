using API.Models;

namespace API.DTOs
{
    /**
     * ReadDonorDTOForDesktop is a Data Transfer Object (DTO) used for representing donor information specifically 
     * designed for the desktop application. This class contains essential details about a donor, including their 
     * personal information, contact details, city and zip code information, and blood type. 
     *
     * This DTO is intended to be used in scenarios where the donor information needs to be displayed on the desktop 
     * application, such as when viewing or updating donor profiles.
     *
     * Properties:
     * - `CprNo`: A string representing the CPR number of the donor.
     * - `DonorFirstName`: A string representing the first name of the donor.
     * - `DonorLastName`: A string representing the last name of the donor.
     * - `DonorPhoneNo`: An integer representing the donor's phone number.
     * - `DonorEmail`: A string representing the donor's email address.
     * - `DonorStreet`: A string representing the street address of the donor.
     * - `CityZipCode`: An instance of the CityZipCodeDTO class that holds the city and zip code information.
     * - `BloodType`: An enum value representing the donor's blood type, which can be nullable.
     */
    public class ReadDonorDTOForDesktop
    {
        // Properties
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int DonorPhoneNo { get; set; }
        public string DonorEmail { get; set; }
        public string DonorStreet { get; set; }

        // City and zip code information for the donor
        public CityZipCodeDTO CityZipCode { get; set; }

        // Donor's blood type, which is nullable
        public BloodTypeEnum? BloodType { get; set; }
    }
}

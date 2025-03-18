using API.Model;

namespace API.DTOs
{
    /// <summary>
    /// ReadDonorDTOForWeb is a Data Transfer Object (DTO) used for representing donor information specifically 
    /// designed for web applications. This class contains key details about a donor, such as their personal information, 
    /// contact details, city and zip code information, and blood type. 
    ///
    /// This DTO is intended for use in web-based applications where donor data is required for display, such as for donor 
    /// profiles, registration, or updates.
    ///
    /// Properties:
    /// - `DonorFirstName`: A string representing the first name of the donor.
    /// - `DonorLastName`: A string representing the last name of the donor.
    /// - `DonorPhoneNo`: An integer representing the donor's phone number.
    /// - `DonorEmail`: A string representing the donor's email address.
    /// - `DonorStreet`: A string representing the street address of the donor.
    /// - `CityZipCode`: An instance of the CityZipCodeDTO class that holds the city and zip code information.
    /// - `BloodType`: An enum value representing the donor's blood type, which can be nullable.
    /// </summary>
    public class ReadDonorDTOForWeb
    {

        // Properties
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

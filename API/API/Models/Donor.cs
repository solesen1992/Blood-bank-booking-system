namespace API.Models
{
    /*
     * Represents a donor in the system. 
     * This class holds the donor's personal information, including contact details, city, 
     * and blood type. It also contains a foreign key to the CityZipCode and BloodType entities 
     * that represent the donor's location and blood type.
     */
    public class Donor
    {
        // Properties
        public int? DonorId { get; set; } // Unique identifier for the donor (nullable)
        public string CprNo { get; set; }
        public string DonorFirstName { get; set; }
        public string DonorLastName { get; set; }
        public int DonorPhoneNo { get; set; }
        public string DonorEmail { get; set; }
        public string DonorStreet { get; set; }
        public int FK_CityZipCodeId { get; set; }
        public int? FK_BloodTypeId { get; set; } // Foreign key to the BloodType entity (nullable)
        public BloodTypeEnum? BloodType { get; set; } // Enum representing the donor's blood type
        public CityZipCode CityZipCode { get; set; }
    }
}

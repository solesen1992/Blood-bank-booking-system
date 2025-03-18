using API.DTOs;
using API.Models;

namespace API.ModelConversion
{
    /**
     * This class is responsible for converting Donor model objects to their corresponding 
     * Data Transfer Objects (DTOs) and vice versa. It provides methods for conversion 
     * for different use cases such as web and desktop applications.
     */
    public class DonorDTOConvert
    {
        /**
         * Converts a single Donor model object to a ReadDonorDTOForWeb, 
         * which is used for displaying donor details on a web interface.
         *
         * @param donor The Donor model to convert.
         * @return A ReadDonorDTOForWeb containing donor details.
         */
        public static ReadDonorDTOForWeb ToDonorDTOForWeb(Donor donor)
        {
            // If the donor is null, return null to avoid errors.
            if (donor == null) return null;

            // Create a new instance of ReadDonorDTOForWeb
            var donorDTO = new ReadDonorDTOForWeb();

            // Populate the properties of the donorDTO using the donor data
            donorDTO.DonorFirstName = donor.DonorFirstName;  // Set the first name
            donorDTO.DonorLastName = donor.DonorLastName;    // Set the last name
            donorDTO.DonorPhoneNo = donor.DonorPhoneNo;      // Set the phone number
            donorDTO.DonorEmail = donor.DonorEmail;          // Set the email
            donorDTO.DonorStreet = donor.DonorStreet;        // Set the street address

            // Create a new CityZipCodeDTO for the donor's city and zip code and assign to donorDTO
            donorDTO.CityZipCode = new CityZipCodeDTO
            {
                City = donor.CityZipCode.City,    // Set the city
                ZipCode = donor.CityZipCode.ZipCode // Set the zip code
            };

            donorDTO.BloodType = donor.BloodType;  // Set the blood type of the donor.

            return donorDTO;  // Return the populated DTO
        }

        /**
         * Converts a single Donor model object to a ReadDonorDTOForDesktop, 
         * which is used for displaying donor details on a desktop interface.
         *
         * @param donor The Donor model to convert.
         * @return A ReadDonorDTOForDesktop containing donor details.
         */
        public static ReadDonorDTOForDesktop ToDonorDTOForDesktop(Donor donor)
        {
            // If the donor is null, return null to avoid errors.
            if (donor == null) return null;

            // Create a new ReadDonorDTOForDesktop and set its properties manually
            var donorDTO = new ReadDonorDTOForDesktop
            {
                CprNo = donor.CprNo,
                DonorFirstName = donor.DonorFirstName,
                DonorLastName = donor.DonorLastName,
                DonorPhoneNo = donor.DonorPhoneNo,
                DonorEmail = donor.DonorEmail,
                DonorStreet = donor.DonorStreet,
                CityZipCode = new CityZipCodeDTO { City = donor.CityZipCode.City, ZipCode = donor.CityZipCode.ZipCode },
                BloodType = donor.BloodType
            };

            return donorDTO;
        }

        /**
         * Converts a list of Donor model objects into a list of ReadDonorDTOForDesktop.
         * This method is used for displaying multiple donors with their details on a desktop interface.
         *
         * @param donors A list of Donor models to convert.
         * @return A list of ReadDonorDTOForDesktop containing donor details.
         */
        public static List<ReadDonorDTOForDesktop> ToDonorDTOForDesktopList(List<Donor> donors)
        {
            // Create a new list to hold the converted DTOs.
            var donorDTOList = new List<ReadDonorDTOForDesktop>();

            // Loop through each donor in the donors list.
            foreach (var donor in donors)
            {
                // Convert each donor and add the DTO to the list.
                donorDTOList.Add(ToDonorDTOForDesktop(donor));
            }
            // Return the list of DTOs.
            return donorDTOList;
        }

        /**
         * Converts a DonorDTOForWeb to a Donor model object, which is used for inserting or updating 
         * donor information in the database.
         *
         * @param donorDTO The DonorDTOForWeb containing the donor details.
         * @return A Donor model populated with the data from the DTO.
         */
        public static Donor ToDonor(ReadDonorDTOForWeb donorDTO)
        {
            // If the donorDTO is null, return null to avoid errors.
            if (donorDTO == null) return null;

            // Create and return a new Donor model populated with the data from the DTO.
            return new Donor
            {
                DonorFirstName = donorDTO.DonorFirstName, // Set the first name of the donor.
                DonorLastName = donorDTO.DonorLastName,
                DonorPhoneNo = donorDTO.DonorPhoneNo,
                DonorEmail = donorDTO.DonorEmail,
                DonorStreet = donorDTO.DonorStreet,
                // Create a new CityZipCode object for the donor.
                CityZipCode = new CityZipCode { City = donorDTO.CityZipCode.City, ZipCode = donorDTO.CityZipCode.ZipCode },
                BloodType = donorDTO.BloodType
            };
        }
    }
}

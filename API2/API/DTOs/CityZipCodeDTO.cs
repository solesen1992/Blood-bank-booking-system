namespace API.DTOs
{
    /**
     * CityZipCodeDTO is a Data Transfer Object (DTO) used to represent a city and its corresponding zip code.
     * This class is designed to be used for communication between different layers.
     *
     * Properties:
     * - `City`: A string representing the name of the city.
     * - `ZipCode`: An integer representing the zip code associated with the city.
     *
     * The class provides two constructors:
     * - A parameterized constructor to initialize both the `City` and `ZipCode` properties.
     * - A parameterless constructor for scenarios where the values are set after instantiation.
     */
    public class CityZipCodeDTO
    {
        // Constructor
        public CityZipCodeDTO() { }

        // Properties
        public string City { get; set; }  // Only the relevant information for the client
        public int ZipCode { get; set; }
    }
}

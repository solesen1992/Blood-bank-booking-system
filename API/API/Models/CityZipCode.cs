namespace API.Models
{
    /*
     * Represents a city and its associated zip code.
     * This class is used to store and manage the relationship between a city's name and its postal code.
     */
    public class CityZipCode
    {
        // Properties
        public int CityZipCodeId { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}

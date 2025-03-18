namespace API.Models
{
    /*
     * BloodTypeEnum represents the different blood types in the system. 
     * This enumeration is used to categorize and identify the blood type of a donor or patient.
     * Each value in this enum corresponds to a specific blood type, with positive and negative Rh factors included.
     *
     * The enum values are mapped to integers to facilitate storage and comparisons in databases and systems.
     * The `None` value represents a default or unspecified blood type, which is used when a new donor is created 
     * since a donor doesn't know his/her own blood type.
     */
    public enum BloodTypeEnum
    {
        APositive = 1,
        ANegative = 2,
        BPositive = 3,
        BNegative = 4,
        ABPositive = 5,
        ABNegative = 6,
        OPositive = 7,
        ONegative = 8,

        /*
         * Represents a default or unspecified blood type.
         * Used when a new donor is created and the donor does not know their blood type.
         */
        None = 0
    }
}

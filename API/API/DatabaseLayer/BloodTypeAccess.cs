using API.Model;
using System.Data.SqlClient;
using Dapper;
/*SHOULD HAVE BEEN DELETED*/

namespace API.DatabaseLayer
{
    /// <summary>
    /// The BloodTypeAccess class is responsible for interacting with the database to retrieve blood type information.
    /// It uses Dapper to query the database and map the results into a list of BloodTypeEnum values.
    /// The class manages the database connection string, which is configured via the app's settings.
    /// </summary>
    public class BloodTypeAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BloodTypeAccess"/> class.
        /// The connection string is loaded from the application's configuration using the provided <see cref="IConfiguration"/> object.
        /// </summary>
        /// <param name="inConfiguration">The application's configuration that provides access to the connection string.</param>
        public BloodTypeAccess(IConfiguration inConfiguration)
        {
            // Retrieve the name of the connection string to use from the configuration
            string? useConnectionString = inConfiguration["ConnectionStringToUse"];
            // If a valid connection string name is provided in the configuration
            // Retrieve the actual connection string from the configuration 
            // If no connection string name is provided, ConnectionString is set to null
            ConnectionString = useConnectionString is not null ? inConfiguration.GetConnectionString(useConnectionString) : null;
        }

        /// <summary>
        /// Gets or sets the connection string to the database.
        /// The connection string is used to connect to the database for querying.
        /// </summary>
           public string? ConnectionString { get; set; }

        /// <summary>
        /// Gets all blood types from the database.
        /// It queries the 'BloodTypes' table and attempts to convert the result into <see cref="BloodTypeEnum"/> values.
        /// It opens a connection to the database, executes a SQL query, and parses the results into a list of <see cref="BloodTypeEnum"/>.
        /// </summary>
        /// <returns>A list of <see cref="BloodTypeEnum"/> values representing all available blood types in the database.</returns>
        public List<BloodTypeEnum> GetAllBloodTypes()
        {
            // SQL query to select all blood type names
            string query = "SELECT BloodTypeName FROM BloodTypes";

            // List to hold the results
            List<BloodTypeEnum> bloodTypes = new List<BloodTypeEnum>();

            // Using Dapper to open connection and query
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Query the database using Dapper
                var result = connection.Query<string>(query);

                foreach (var bloodTypeName in result)
                {
                    // Try to parse the string into a BloodTypeEnum
                    if (Enum.TryParse(bloodTypeName, out BloodTypeEnum bloodType))
                    {
                        // If parsing is successful, add the enum to the bloodTypes list
                        bloodTypes.Add(bloodType);
                    }
                    else
                    {
                        // Log the error
                        Console.WriteLine($"Warning: '{bloodTypeName}' is not a valid blood type.");
                        // Add a default value 
                        bloodTypes.Add(BloodTypeEnum.None);
                    }
                }
            }
            return bloodTypes;
        }

    }  
}


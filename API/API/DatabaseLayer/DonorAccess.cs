namespace API.DatabaseLayer
{
    /**
     * The DonorAccess class in the DatabaseLayer provides methods to interact with the database for 
     * donor-related operations.
     * 
     * The class uses Dapper for querying and manipulating the SQL database and retrieves or modifies 
     * information from multiple tables such as Donor, BloodType, and CityZipCode. It also handles 
     * the insertion or getting of the CityZipCode based on donor details.
     */
    public class DonorAccess : IDonorAccess
    {
        public string? ConnectionString { get; set; }

        /**
         * Constructor to initialize the DonorAccess class with a configuration object.
         * It retrieves the connection string from the configuration file.
         * Throws an exception if the connection string is not found.
         * 
         * @param configuration The configuration object to retrieve the connection string.
         */
        public DonorAccess(IConfiguration configuration)
        {
            //Access the connection string
            ConnectionString = configuration.GetConnectionString("ConnectMsSqlString");
            if (ConnectionString == null)
            {
                throw new Exception("Connection string is not set.");
            }
        }

        /**
         * Retrieves a list of donors along with their associated blood type and city information.
         * This method executes a SQL query that joins the Donor, BloodType, and CityZipCode tables
         * to gather comprehensive donor details.
         * 
         * @return A list of Donor objects, each containing donor information, blood type, and city details.
         */
        public List<Donor> GetDonorsWithBloodTypeAndCity()
        {
            // Initialize a list to hold the donor objects
            var donors = new List<Donor>();

            // Establish a connection to the database using the provided connection string
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Open the database connection
                connection.Open();

                // Define the SQL query to retrieve donor information along with their blood type and city details
                string query = @"
                    SELECT BloodType.bloodType, CityZipCode.city, CityZipCode.zipcode, Donor.*
                    FROM   BloodType RIGHT JOIN
                                 Donor ON BloodType.bloodTypeId = Donor.FK_bloodTypeId LEFT JOIN
                                 CityZipCode ON Donor.FK_cityZipCodeId = CityZipCode.cityZipCodeId";

                // Create a new SQL connection and command to execute the query
                using (SqlConnection conn = new SqlConnection(ConnectionString)) // Using statement to ensure that the SQL connection is properly disposed of after use
                using (SqlCommand readCommand = new SqlCommand(query, conn)) // Create a new SQL command object with the specified query and connection
                {
                    // Check if the connection object is not null
                    if (conn != null)
                    {
                        // Open the connection
                        conn.Open();

                        // Execute the query and get a SqlDataReader to read the results
                        SqlDataReader lineReader = readCommand.ExecuteReader();

                        // Convert the SqlDataReader results into a list of Donor objects
                        donors = GetDonorObjects(lineReader);
                    }
                }
                // Output the connection string to the console for debugging purposes
                Console.WriteLine($"Connection String: {ConnectionString}");
                return donors;
            }
        }

        /**
         * Converts the data from a SqlDataReader into a list of Donor objects.
         * This method reads through each row of the SQL query results and populates a Donor object accordingly.
         * 
         * @param reader The SqlDataReader containing the donor data from the database.
         * @return A list of Donor objects populated with data from the SqlDataReader.
         */
        public List<Donor> GetDonorObjects(SqlDataReader reader)
        {
            // Initialize a list to hold the donor objects
            List<Donor> donors = new List<Donor>();

            // Loop through the SqlDataReader to read each record
            while (reader.Read()) // One row at a time
            {
                // Create a new Donor object and populate its properties from the SqlDataReader
                var donor = new Donor
                {
                    // Retrieve the integer value from the "donorId" column in the data reader
                    // It returns the value of the donorId as an integer. Each column is accessed by its name via GetOrdinal
                    DonorId = reader.GetInt32(reader.GetOrdinal("donorId")),
                    CprNo = reader.GetString(reader.GetOrdinal("cprNo")),
                    DonorFirstName = reader.GetString(reader.GetOrdinal("donorFirstName")),
                    DonorLastName = reader.GetString(reader.GetOrdinal("donorLastName")),
                    DonorPhoneNo = reader.GetInt32(reader.GetOrdinal("donorPhoneNo")),
                    DonorEmail = reader.GetString(reader.GetOrdinal("donorEmail")),
                    DonorStreet = reader.GetString(reader.GetOrdinal("donorStreet")),
                    FK_CityZipCodeId = reader.GetInt32(reader.GetOrdinal("FK_cityZipCodeId")),
                    // Check if FK_BloodTypeId is null and handle accordingly. If the Fk_BloodTypeId is null, make it null. Else read the integer value.
                    FK_BloodTypeId = reader.IsDBNull(reader.GetOrdinal("FK_bloodTypeId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("FK_bloodTypeId")),
                    // Check if FK_BloodTypeId is null and handle accordingly for BloodType enum
                    BloodType = reader.IsDBNull(reader.GetOrdinal("FK_bloodTypeId")) ? (BloodTypeEnum?)null : (BloodTypeEnum)reader.GetInt32(reader.GetOrdinal("FK_bloodTypeId")),
                    // Create and populate the CityZipCode object
                    CityZipCode = new CityZipCode
                    {
                        CityZipCodeId = reader.GetInt32(reader.GetOrdinal("FK_cityZipCodeId")),
                        City = reader.GetString(reader.GetOrdinal("city")),
                        ZipCode = reader.GetInt32(reader.GetOrdinal("zipcode"))
                    }
                };
                // Add the populated donor object to the list
                donors.Add(donor);
            }
            return donors;
        }

        /**
         * Retrieves a donor by their CPR number.
         * This method checks if the CPR number exists in the database and returns the corresponding donor information.
         * 
         * @param cprNo The CPR number of the donor to retrieve.
         * @return The Donor object corresponding to the provided CPR number, or null if no donor is found.
         */
        public Donor GetDonorByCprNo(string cprNo)
        {
            // Initialize a new Donor object
            Donor donor = new Donor();
            // Ensure a valid CPR number is provided
            if (string.IsNullOrEmpty(cprNo))
            {
                throw new ArgumentException("CPR number cannot be null or empty.");
            }

            // Establish a connection to the database using the provided connection string
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Open the database connection
                connection.Open();

                // Define the SQL query to retrieve donor information along with their blood type and city details
                string query = @"
                    SELECT BloodType.*, 
                           CityZipCode.cityZipCodeId, CityZipCode.city, CityZipCode.zipcode, 
                           Donor.*
                    FROM BloodType 
                    RIGHT JOIN Donor ON BloodType.bloodTypeId = Donor.FK_bloodTypeId    
                    LEFT JOIN CityZipCode ON Donor.FK_cityZipCodeId = CityZipCode.cityZipCodeId
                    WHERE cprNo = @CprNo";

                // Use an anonymous type to hold the result and map CityZipCode separately, Executes the SQL query and maps the result directly into a Donor object using Dapper's QueryFirstOrDefault.
                var result = connection.QueryFirstOrDefault<Donor>(query, new { CprNo = cprNo });

                if (result != null)
                {
                    // Set the BloodType property based on the FK_BloodTypeId value. Maps the FK_BloodTypeId foreign key to the BloodType property, converting it to a nullable enum.
                    result.BloodType = result.FK_BloodTypeId.HasValue ? (BloodTypeEnum?)result.FK_BloodTypeId : null;

                    // Create and populate the CityZipCode object
                    // Query the database for the city and zip code based on FK_CityZipCodeId
                    result.CityZipCode = new CityZipCode
                    {
                        CityZipCodeId = result.FK_CityZipCodeId,
                        City = connection.QueryFirstOrDefault<string>("SELECT City FROM CityZipCode WHERE cityZipCodeId = @CityZipCodeId", new { CityZipCodeId = result.FK_CityZipCodeId }),
                        ZipCode = connection.QueryFirstOrDefault<int>("SELECT ZipCode FROM CityZipCode WHERE cityZipCodeId = @CityZipCodeId", new { CityZipCodeId = result.FK_CityZipCodeId })
                    };
                }
                return result;
            }
        }

        /**
         * Retrieves a donor by their ID.
         * This method checks if the donor ID exists in the database and returns the corresponding donor information.
         * 
         * @param donorId The ID of the donor to retrieve.
         * @return The Donor object corresponding to the provided ID, or null if no donor is found.
         */
        public Donor GetDonorById(int donorId)
        {
            // Initialize a new Donor object
            Donor donor = new Donor();

            // Ensure a valid donor ID is provided
            if (donorId <= 0)
            {
                throw new ArgumentException("Donor ID must be greater than zero.");
            }

            // Establish a connection to the database using the provided connection string
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Open the database connection
                connection.Open();

                // Define the SQL query to retrieve donor information along with their blood type and city details
                string query = @"
                    SELECT BloodType.*, 
                           CityZipCode.cityZipCodeId, CityZipCode.city, CityZipCode.zipcode, 
                           Donor.*
                    FROM BloodType 
                    RIGHT JOIN Donor ON BloodType.bloodTypeId = Donor.FK_bloodTypeId    
                    LEFT JOIN CityZipCode ON Donor.FK_cityZipCodeId = CityZipCode.cityZipCodeId
                    WHERE donorId = @DonorId";

                // Use an anonymous type to hold the result and map CityZipCode separately
                var result = connection.QueryFirstOrDefault<Donor>(query, new { DonorId = donorId });

                if (result != null)
                {
                    // Set the BloodType property based on the FK_BloodTypeId value
                    result.BloodType = result.FK_BloodTypeId.HasValue ? (BloodTypeEnum?)result.FK_BloodTypeId : null;
                    // Create and populate the CityZipCode object
                    result.CityZipCode = new CityZipCode
                    {
                        CityZipCodeId = result.FK_CityZipCodeId,
                        City = connection.QueryFirstOrDefault<string>("SELECT City FROM CityZipCode WHERE cityZipCodeId = @CityZipCodeId", new { CityZipCodeId = result.FK_CityZipCodeId }),
                        ZipCode = connection.QueryFirstOrDefault<int>("SELECT ZipCode FROM CityZipCode WHERE cityZipCodeId = @CityZipCodeId", new { CityZipCodeId = result.FK_CityZipCodeId })
                    };
                }
                return result;
            }
        }

        /**
         * Checks whether a city and its corresponding zip code already exist in the CityZipCode table.
         * If they do, it retrieves the CityZipCodeId. If they do not exist, it inserts a new record for the city and zip code
         * and returns the newly inserted CityZipCodeId.
         * 
         * @param donor The Donor object containing the city and zip code information.
         * @return The CityZipCodeId if found or newly inserted; otherwise, null.
         */
        public int? InsertOrGetExistingCityZipCode(Donor donor)
        {
            // Nullable integer variable to hold the CityZipCodeId (can be null if not found or inserted)
            int? donorsCityZipCodeId;

            // Establish a connection to the database using the provided connection string
            using (var Connection = new SqlConnection(ConnectionString))
            {
                // Define the SQL query to search for a CityZipCodeId in the CityZipCode table 
                // based on the provided City and ZipCode from the donor object
                string querySelectCirtZipCode = @"SELECT cityZipCodeId FROM CityZipCode WHERE City = @City AND ZipCode = @ZipCode";

                // Try to get the CityZipCodeId from the CityZipCode table using the donor's city and zip code
                donorsCityZipCodeId = Connection.QueryFirstOrDefault<int?>(querySelectCirtZipCode, new
                {
                    // Bind the City value from the donor object
                    City = donor.CityZipCode.City,
                    // Bind the ZipCode value from the donor object
                    ZipCode = donor.CityZipCode.ZipCode
                });

                // If no CityZipCodeId is found (i.e., it's null or doesn't exist in the database)
                if (!donorsCityZipCodeId.HasValue)
                {
                    // Extract the city and zipcode from the donor object to insert a new record
                    string city = donor.CityZipCode.City;
                    int zipcode = donor.CityZipCode.ZipCode;

                    // Define the SQL query to insert a new CityZipCode record into the CityZipCode table
                    string queryInsertCityZipCode = @"INSERT INTO CityZipCode(city,zipcode) OUTPUT INSERTED.CityZipCodeID VALUES(@city,@zipcode)";

                    // Execute the insert query and get the newly inserted CityZipCodeId
                    donorsCityZipCodeId = Connection.ExecuteScalar<int>(queryInsertCityZipCode, new { city, zipcode });
                }
            }
            // Return the found or newly inserted CityZipCodeId
            return donorsCityZipCodeId;
        }

        /**
         * Inserts a new donor into the database.
         * This method first checks for a valid donor object, retrieves or inserts the CityZipCode,
         * and then inserts the donor’s information into the Donor table.
         * 
         * @param donor The Donor object containing the donor's information.
         * @return True if the donor was successfully inserted; otherwise, false.
         */
        public bool InsertDonor(Donor donor)
        {
            if (donor == null)
            {
                // Throws an exception if the donor parameter is null.
                throw new ArgumentNullException(nameof(donor), "Donor cannot be null");
            }

            // Declares a variable to track if the donor was created successfully.
            bool created = false;

            // Uses a 'using' block to open a connection to the database. The connection is automatically closed when done.
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    // Retrieve or insert CityZipCode first
                    // Calls InsertOrGetExistingCityZipCode method to either retrieve or insert the CityZipCode.
                    int? cityZipCodeId = InsertOrGetExistingCityZipCode(donor);

                    // Defines the SQL query to insert the donor’s details into the Donor table.
                    string query = @"INSERT INTO Donor (cprNo, donorFirstName, donorLastName, donorPhoneNo, donorEmail, donorStreet, FK_cityZipCodeId)
                             VALUES (@CprNo, @DonorFirstName, @DonorLastName, @DonorPhoneNo, @DonorEmail, @DonorStreet, @FK_CityZipCodeId)";

                    // Executes the SQL query and pass the donor's properties as parameters to the query.
                    int rowsAffected = connection.Execute(query, new
                    {
                        donor.CprNo,
                        donor.DonorFirstName,
                        donor.DonorLastName,
                        donor.DonorPhoneNo,
                        donor.DonorEmail,
                        donor.DonorStreet,
                        // Foreign key linking to the CityZipCodeId.
                        FK_CityZipCodeId = cityZipCodeId
                    });

                    // Checks if any rows were affected by the insert. If rowsAffected > 0, the donor was successfully created.
                    created = rowsAffected > 0;
                }
                // Catch any exceptions that occur during the insertion process.
                catch (Exception ex)
                {
                    Console.WriteLine($"InsertDonor Error: {ex.Message}");
                }
            }
            // Return the value of 'created', which indicates if the donor was successfully inserted.
            return created;
        }

        /**
         * Checks if a CPR number exists in the Donor table.
         * This method queries the database to determine if the provided CPR number is already associated with an existing donor.
         * 
         * @param cprNo The CPR number to check.
         * @return True if the CPR number exists; otherwise, false.
         */
        public bool DoesCprNoExist(string cprNo)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Define the SQL query to check if the CPR number exists. This counts the number of rows in the Donor table where the condition CprNo = @CprNo is true.
                string query = "SELECT COUNT(1) FROM Donor WHERE CprNo = @CprNo";

                using (var command = new SqlCommand(query, connection))
                {
                    // Add a parameter to prevent SQL injection. @CprNo is added to safely include the cprNo value in the query, preventing SQL injection attacks.
                    command.Parameters.AddWithValue("@CprNo", cprNo);

                    // Open the connection
                    connection.Open();

                    // Execute the query and check the result. ExecuteScalar is used when a query returns a single value (like the count in this case)
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Return true if the CPR number exists
                }
            }
        }

        /**
         * Updates the information of an existing donor in the database.
         * This method modifies the donor's details based on the provided Donor object.
         * 
         * @param donor The Donor object containing the updated donor information.
         * @return The updated Donor object if the update was successful; otherwise, null.
         */
        public Donor UpdateDonor(Donor donor)
        {
            // Check if the donor object is not null
            if (donor is not null)
            {
                // Establish a new SQL connection to the database using the connection string
                using (var Connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        // Insert or get the city and zip code ID, ensuring it exists in the database
                        int? donorsCityZipCodeId = InsertOrGetExistingCityZipCode(donor);

                        // Define the SQL query to update donor information in the database
                        string query = @"
                            UPDATE Donor 
                            SET 
                                cprNo = @CprNo, 
                                donorFirstName = @DonorFirstName, 
                                donorLastName = @DonorLastName, 
                                donorPhoneNo = @DonorPhoneNo, 
                                donorEmail = @DonorEmail, 
                                donorStreet = @DonorStreet, 
                                FK_cityZipCodeId = @FK_CityZipCodeId, 
                                FK_BloodTypeId = @FK_BloodTypeId
                            WHERE cprNo = @CprNo";
                        // Get the blood type ID as an integer, or null if the blood type is not set
                        int? bloodTypeId = donor.BloodType != null ? (int?)donor.BloodType : null;

                        // Execute the query to update the donor's information, passing the parameters to the query
                        int rowsAffected = Connection.Execute(query, new
                        {
                            CprNo = donor.CprNo,
                            DonorFirstName = donor.DonorFirstName,
                            DonorLastName = donor.DonorLastName,
                            DonorPhoneNo = donor.DonorPhoneNo,
                            DonorEmail = donor.DonorEmail,
                            DonorStreet = donor.DonorStreet,
                            //FK_CityZipCodeId = (int)donorsCityZipCodeId,
                            //FK_BloodTypeId = (int)donor.BloodType
                            FK_CityZipCodeId = donorsCityZipCodeId,
                            FK_BloodTypeId = bloodTypeId // Nullable FK_BloodTypeId
                        });

                        // If the update was successful (at least one row affected), fetch the updated donor object
                        if (rowsAffected > 0)
                        {
                            // Output donor details to the console for verification
                            Console.WriteLine($"{donor.CprNo} + {donor.DonorLastName} + {donor.DonorPhoneNo} + {donor.DonorEmail}+ {donor.DonorStreet} + {(int)donorsCityZipCodeId} + {(int)donor.BloodType}");

                            Console.WriteLine($"{donor.CityZipCode.CityZipCodeId} and {donor.CityZipCode.ZipCode} and {donor.CityZipCode.City} ");

                            // Fetch the updated donor object from the database by CPR number
                            donor = GetDonorByCprNo(donor.CprNo);
                        }
                        else
                        {
                            // If no rows were affected, throw an exception indicating the update failed
                            throw new InvalidOperationException("Donor update failed.");
                        }
                    }
                    catch (Exception e)
                    {
                        // Catch any exceptions during the execution of the SQL commands and print the error message
                        Console.WriteLine($"error : {e.Message}");
                    }
                }
            }
            // Return the updated donor object
            return donor;
        }

        /**
         * Deletes an existing donor from the database.
         * This method removes the donor's record based on the provided Donor object.
         * 
         * @param donor The Donor object representing the donor to be deleted.
         * @return True if the donor was successfully deleted; otherwise, false.
         */
        public bool DeleteDonor(Donor donor)
        {
            // Declare a variable to track whether the donor was deleted
            bool deleted = false;

            // Check if the donor is not null and the CPR number is valid
            if (donor is not null || donor.CprNo is not null)
            {
                // Establish a new SQL connection to the database using the connection string
                using (var Connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        // Update foreign keys to prevent integrity issues when deleting the donor
                        // Define the SQL query to update foreign keys, setting them to NULL before deleting the donor. @ so you can make more lines
                        string updateForeignKeysQuery = @"
                            UPDATE Donor
                            SET FK_cityZipCodeId = NULL, FK_bloodTypeId = NULL
                            WHERE cprNo = @CprNo"; // parameter placeholder to prevent sql injections

                        // Execute the query to update the foreign keys in the database
                        Connection.Execute(updateForeignKeysQuery, new { CprNo = donor.CprNo });

                        // Define the SQL query to delete the donor record from the database
                        string deleteDonorQuery = @"DELETE FROM Donor WHERE cprNo = @CprNo";
                        // Execute the query to delete the donor from the database. Execute returns the number of rows that was deleted
                        int rowsAffected = Connection.Execute(deleteDonorQuery, new { CprNo = donor.CprNo }); // DELETE FROM Donor WHERE cprNo = @CprNo

                        // If rows were affected (donor was deleted), set deleted to true
                        if (rowsAffected > 0)
                        {
                            deleted = rowsAffected > 0;
                        }
                        else
                        {
                            // If no rows were affected, throw an exception indicating the deletion failed
                            throw new InvalidOperationException("Donor update failed.");
                        }
                    }
                    catch (Exception e)
                    {
                        // Catch any exceptions during the execution of the SQL commands and print the error message
                        Console.WriteLine($"error : {e.Message}");
                    }
                }
            }
            // Return whether the donor was successfully deleted
            return deleted;
        }
    }
}

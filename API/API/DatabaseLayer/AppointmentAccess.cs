namespace API.DatabaseLayer
{
    /// <summary>
    /// The <c>AppointmentAccess</c> class interacts with the database to manage appointment data.
    /// It includes methods to fetch, insert, update, and delete appointments from the database.
    /// The class uses Dapper for SQL query execution and manages database connections through <c>SqlConnection</c>.
    /// This class implements the <c>IAppointmentAccess</c> interface.
    /// </summary>
    public class AppointmentAccess : IAppointmentAccess
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public string? ConnectionString { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <c>AppointmentAccess</c> class.
        /// It retrieves the connection string from the application configuration file and throws an exception
        /// if the connection string is not found.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <exception cref="Exception">Thrown when the connection string is not set.</exception>
        public AppointmentAccess(IConfiguration configuration)
        {
            // Access the connection string from the configuration
            ConnectionString = configuration.GetConnectionString("ConnectMsSqlString");

            // Throw exception if connection string is not found
            if (ConnectionString == null)
            {
                throw new Exception("Connection string is not set.");
            }
        }

        /// <summary>
        /// Gets all appointments from the database.
        /// </summary>
        /// <returns>A list of <c>Appointment</c> objects.</returns>
        /// <exception cref="Exception">Thrown when a database error occurs while fetching appointments.</exception>
        public List<Appointment> GetAppointments()
        {
            // List to hold the fetched appointments
            var appointments = new List<Appointment>();

            try
            {
                // Creates a new instance of SqlConnection with the specified ConnectionString.
                // This connection will be used to interact with the database.
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // SQL query to get appointments along with associated donor data
                    var query = @"
                        SELECT 
                                Appointment.appointmentId, Appointment.startTime, Appointment.endTime,
                                Donor.donorId
                            FROM Appointment
                            INNER JOIN Donor ON Appointment.FK_donorId = Donor.donorId";

                    // Execute SQL query to get the appointments
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Execute the reader to fetch data
                        using (var reader = command.ExecuteReader())
                        {
                            // Loop through the results
                            while (reader.Read())
                            {
                                // Add each appointment into the list
                                appointments.Add(new Appointment
                                {
                                    AppointmentId = reader.GetInt32(0), // Retrieve the integer value of the first column (index 0) from the current row in the SqlDataReader.
                                    StartTime = reader.GetDateTime(1),
                                    EndTime = reader.GetDateTime(2),
                                    FK_donorId = reader.GetInt32(3),
                                });
                            }
                        }
                    }
                }
            }

            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., issues with the SQL server, query execution)
                Console.WriteLine($"SQL error: {sqlEx.Message}");
                throw new Exception("Database error occurred while fetching appointments.", sqlEx); // Rethrow with context
            }
            catch (Exception ex)
            {
                // Log any other general exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception("An error occurred while fetching appointments.", ex);
            }
            // Return the list of appointments
            return appointments;
        }

        /// <summary>
        /// Gets a specific appointment by its ID from the database.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>An <c>Appointment</c> object or null if not found.</returns>
        /// <exception cref="ArgumentException">Thrown when the appointment ID is invalid.</exception>
        /// <exception cref="Exception">Thrown when a database error occurs while fetching the appointment.</exception>
        public Appointment GetAppointmentById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid appointment ID.");
            }

            // Variable to hold the fetched appointment
            Appointment appointment = null;

            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // SQL query to get the appointment by ID
                    var query = @"
                        SELECT 
                            appointmentId, startTime, endTime, FK_donorId
                        FROM Appointment
                        WHERE appointmentId = @AppointmentId";

                    // Execute the query and fetch the appointment. Returns one result or null. Should be mapped as an instance of Appointment.
                    appointment = connection.QuerySingleOrDefault<Appointment>(query, new { AppointmentId = id }); // Takes the query above
                } // Laver et anonymt objekt med en egenskab, der hedder AppointmentId. Egenskaben får værdien af variablen id fra koden.
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., issues with the SQL server, query execution)
                Console.WriteLine($"SQL error: {sqlEx.Message}");
                throw new Exception("Database error occurred while fetching the appointment.", sqlEx); // Rethrow with context
            }
            catch (Exception ex)
            {
                // Log any other general exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception("An error occurred while fetching the appointment.", ex);
            }

            // Return the fetched appointment or null if not found
            return appointment;
        }


        /// <summary>
        /// Checks if there are any existing appointments that overlap with the desired time.
        /// </summary>
        /// <param name="startTime">The start time of the appointment.</param>
        /// <param name="endTime">The end time of the appointment.</param>
        /// <param name="connection">The SQL connection.</param>
        /// <param name="transaction">The SQL transaction (optional).</param>
        /// <returns>The number of overlapping appointments.</returns>
        private int OverLapChecker(DateTime startTime, DateTime endTime, SqlConnection connection, SqlTransaction transaction = null)
        {
            int result = 0;

            //It runs a query to find appointments that:Start before the given endTime AND
            //End after the given startTime.
            // The result set includes all appointments that could potentially overlap with the new appointment time range.

            string query = @" SELECT *  FROM Appointment 
                        WHERE Appointment.startTime < @EndTime
                        AND Appointment.endTime > @StartTime";

            // Execute the query using Dapper to get a list of appointments that overlap
            var appointments = connection.Query<Appointment>(query, new { StartTime = startTime, EndTime = endTime }, transaction);

            //loops through the retrieved appointments and further verifies overlaps using conditions like:
            foreach (Appointment appointment in appointments)
            {
                // Check if the new appointment start time overlaps with an existing appointment start time
                // or if the new appointment end time overlaps with an existing appointment end time
                if ((appointment.StartTime >= startTime && appointment.StartTime <= endTime) ||
        (appointment.EndTime >= startTime && appointment.EndTime <= endTime))
                {
                    // If there is an overlap, increment the result count
                    result++;
                }
            }
            // Return the count of overlapping appointments
            return result;
        }

        /// <summary>
        /// Inserts a new appointment into the database.
        /// </summary>
        /// <param name="appointment">The appointment details.</param>
        /// <param name="donorId">The associated donor ID.</param>
        /// <returns>True if the appointment was successfully inserted, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the appointment is null.</exception>
        /// <exception cref="Exception">Thrown when a database error occurs while inserting the appointment.</exception>
        public bool InsertAppointment(Appointment appointment, int donorId)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");
            }

            // To track if insertion was successful
            bool isInserted = false;

            // Define a retry counter. it controls how many times the backend logic will retry internally
            // when it encounters a concurrency issue or failure during the same request.
            int retryCount = 3;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                //Calls OverLapChecker to check if there are any existing overlapping appointments before proceeding:
                if (OverLapChecker(appointment.StartTime, appointment.EndTime, connection) != 0)
                {

                    return false; // Overlap detected, do not insert
                }

                while (retryCount-- > 0) // Retry loop
                {
                    // Begin a transaction to ensure atomicity
                    using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        try
                        {

                            // Insert appointment query. Collect the autogenerated primary key value for appointmentId.
                            string query = @"
                            INSERT INTO Appointment (startTime, endTime, FK_donorId)
                            OUTPUT INSERTED.appointmentId
                            VALUES (@StartTime, @EndTime, @FK_DonorId)";

                            // Set the donorId in the appointment object
                            appointment.FK_donorId = donorId; // Assign the donor ID to the FK_donorId property of the Appointment object
                            // Execute the query and get the result
                            // Use Dapper to execute a SQL query and retrieve a single result
                            // connection.QuerySingleOrDefault is used here to fetch a single appointment based on the query
                            var result = connection.QuerySingleOrDefault(
                                query // The SQL query to be executed
                                , appointment // The parameter to be passed into the query
                                , transaction // SQL transaction

                            );

                            //After attempting to insert the appointment, the code runs OverLapChecker again. == 0 means
                            //inserting failed.
                            // Check if there are overlapping appointments with the provided start and end times
                            if (OverLapChecker(appointment.StartTime, appointment.EndTime, connection, transaction) == 0)
                            {
                                isInserted = false; // If no overlaps are found, set the insertion flag to false
                                transaction.Rollback(); // Rollback the current transaction to undo any changes made so far

                                throw new InvalidOperationException("no appointment was inserted within the given time");
                            }

                            //After inserting, it uses OverLapChecker again to ensure no overlap occurred due to
                            //concurrent operations:
                            // Check if there is exactly one overlapping appointment with the given start and end times
                            if (OverLapChecker(appointment.StartTime, appointment.EndTime, connection, transaction) == 1)
                            {

                                transaction.Commit(); // Commit the transaction if there is no overlap

                                // If the query returned a non-null result and the `appointmentId` is greater than 0,
                                // it means the insertion was successful and we should assign the ID to the appointment
                                if (result != null && result.appointmentId > 0)
                                {
                                    appointment.AppointmentId = result.appointmentId;
                                    // Set the flag indicating the appointment was successfully inserted
                                    isInserted = true;
                                    return isInserted;
                                }


                            }
                            //If a conflict occurs or the transaction fails, it rolls back the transaction, and
                            //waits for a random period (1 to 2.5 seconds) before retrying, reducing the chance
                            //of repeated conflicts
                            transaction.Rollback();
                            // Generates a random sleep time between 1000 and 2500 milliseconds
                            int sleepTime = new Random().Next(1000, 2500);
                            // Pause execution for the randomly generated sleep time to avoid a busy-wait loop
                            System.Threading.Thread.Sleep(sleepTime);

                        }
                        // Catch any exception that occurs during the process
                        catch (Exception ex)
                        {
                            // Mark that the insertion was not successful
                            isInserted = false;
                            // Rollback the transaction on error
                            transaction.Rollback();
                            // Output the error message to the console
                            Console.WriteLine($"InsertAppointment Error: {ex.Message}");

                            // retryCount--; // Decrement the retry counter

                            // If there are no more retry attempts left, throw a new exception
                            if (retryCount == 0)
                            {
                                throw new Exception("Maximum retry attempts reached. Could not insert appointment.", ex);
                            }
                        }
                    }
                }
            }

            return isInserted;
        }


        /// <summary>
        /// Deletes an existing appointment from the database.
        /// </summary>
        /// <param name="appointment">The appointment details.</param>
        /// <returns>True if the appointment was successfully deleted, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the appointment is null.</exception>
        public bool DeleteAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");
            }

            // To track if deletion was successful
            bool isDeleted = false;

            using (var connection = new SqlConnection(ConnectionString))
            {
                // Open the connection to the database
                connection.Open();
                try
                {   // The query uses a parameterized placeholder, @AppointmentId, instead of hardcoding the value, which prevents SQL injection.
                    // SQL query to delete the appointment with the given appointmentId
                    string query = "DELETE FROM Appointment WHERE appointmentId = @AppointmentId";

                    // Execute the query and get the number of affected rows. AppointmentId is the @AppointmentId above. So our @ gets replaced with appointment.AppointmentId
                    int affectedRows = connection.Execute(query, new { AppointmentId = appointment.AppointmentId });
                    // The Execute method returns the number of rows affected by the query. If affectedRows > 0, the deletion was successful.
                    // Check if deletion was successful
                    // If the number of affected rows is greater than 0, the deletion is successful
                    isDeleted = affectedRows > 0;
                }
                catch (SqlException sqlEx)
                {
                    // Log the SQL exception (e.g., issues with the SQL server, query execution)
                    Console.WriteLine($"SQL error: {sqlEx.Message}");
                    throw new Exception("Database error occurred while deleting the appointment.", sqlEx); // Rethrow with context
                }
                catch (Exception ex)
                {
                    // Log any error during deletion
                    Console.WriteLine($"DeleteAppointment Error: {ex.Message}");
                    // Set to false if an error occurs
                    isDeleted = false;
                }
            }
            // Return whether the deletion was successful
            return isDeleted;
        }

        /// <summary>
        /// Retrieves a list of appointments associated with a specific donor.
        /// </summary>
        /// <param name="donorId">The donor ID.</param>
        /// <returns>A list of <c>Appointment</c> objects.</returns>
        /// <exception cref="ArgumentException">Thrown when the donor ID is invalid.</exception>
        /// <exception cref="Exception">Thrown when a database error occurs while fetching appointments.</exception>
        public List<Appointment> GetAppointmentsByDonorId(int donorId)
        {
            // Validate the donorId input to ensure it's a positive integer.
            if (donorId <= 0)
            {
                throw new ArgumentException("Invalid donor ID.");
            }

            // Initialize a list to hold the fetched appointments.
            var appointments = new List<Appointment>();

            try
            {
                // Establish a connection to the database using the connection string.
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // Open the database connection
                    connection.Open();

                    // SQL query to get appointments of a specific donor
                    var query = @"
                         SELECT 
                                 Appointment.appointmentId, Appointment.startTime, Appointment.endTime,
                                 Donor.donorId
                             FROM Appointment
                             INNER JOIN Donor ON Appointment.FK_donorId = Donor.donorId
                             WHERE Donor.donorId = @DonorId";

                    // Execute SQL query to get the appointments
                    using (var command = new SqlCommand(query, connection))
                    {
                        // Set the donorId parameter value
                        command.Parameters.AddWithValue("@DonorId", donorId);

                        // Execute the reader to fetch data. The SqlDataReader provides a way to read rows from the result set one at a time.
                        using (var reader = command.ExecuteReader())
                        {
                            // Loop through the results
                            while (reader.Read())
                            {
                                // Map the fields from the database to an Appointment object and add it to the list.
                                appointments.Add(new Appointment
                                {
                                    AppointmentId = reader.GetInt32(0), // Retrieves the value of the first column as an integer and assigns it to AppointmentId
                                    StartTime = reader.GetDateTime(1),
                                    EndTime = reader.GetDateTime(2),
                                    FK_donorId = reader.GetInt32(3),
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., issues with the SQL server, query execution)
                Console.WriteLine($"SQL error: {sqlEx.Message}");
                throw new Exception("Database error occurred while fetching appointments.", sqlEx); // Rethrow with context
            }
            catch (Exception ex)
            {
                // Log any other general exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw new Exception("An error occurred while fetching appointments.", ex);
            }
            // Return the list of appointments
            return appointments;
        }

        /// <summary>
        /// Deletes an appointment from the database based on its start time.
        /// </summary>
        /// <param name="startTime">The start time of the appointment.</param>
        /// <returns>True if the appointment was successfully deleted, otherwise false.</returns>
        public bool DeleteAppointmentByStartTime(DateTime startTime)
        {
            // Establish a connection to the database using the connection string
            using (var connection = new SqlConnection(ConnectionString))
            {
                // Open the connection to the database
                connection.Open();
                try
                {
                    // SQL query to delete the appointment with the given startTime
                    string query = "DELETE FROM Appointment WHERE startTime = @StartTime";

                    // Execute the query and get the number of affected rows
                    int affectedRows = connection.Execute(query, new { StartTime = startTime });

                    // Check if deletion was successful
                    // If the number of affected rows is greater than 0, the deletion is successful
                    return affectedRows > 0;
                }
                catch (SqlException sqlEx)
                {
                    // Log the SQL exception (e.g., issues with the SQL server, query execution)
                    Console.WriteLine($"SQL error: {sqlEx.Message}");
                    throw new Exception("Database error occurred while deleting the appointment.", sqlEx); // Rethrow with context
                }
                catch (Exception ex)
                {
                    // Log any error that occurs during deletion
                    Console.WriteLine($"DeleteAppointmentByStartTime Error: {ex.Message}");
                    // Return false if an error occurs
                    return false;
                }
            }
        }
    }
}

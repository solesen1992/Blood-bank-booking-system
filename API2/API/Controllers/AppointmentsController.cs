using API.BusinessLogicLayer;
using API.DTOs;
using API.ModelConversion;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /**
     * The `AppointmentsController` class is part of the API layer of the application, responsible for handling HTTP requests 
     * related to appointments. It defines endpoints for interacting with appointment data, including retrieving, inserting, 
     * updating, and deleting appointments.
     * The class uses dependency injection to receive the `IAppointmentLogic` interface, which provides the business logic 
     * necessary for handling the appointment data operations.
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentLogic _appointmentLogic;
        private readonly IDonorLogic _donorLogic;

        /**
         * Initializes a new instance of the `AppointmentsController` class.
         * This constructor uses dependency injection to inject the `IAppointmentLogic` and `IDonorLogic` services.
         * The injected services provide business logic for handling appointment-related operations.
         * 
         * @param appointmentLogic The appointment logic service.
         * @param donorLogic The donor logic service.
         */
        public AppointmentsController(IAppointmentLogic appointmentLogic, IDonorLogic donorLogic)
        {
            _appointmentLogic = appointmentLogic;
            _donorLogic = donorLogic;
        }


        /**
         * Handles GET requests to retrieve all appointments.
         * Calls the `GetAppointments` method from the appointment logic layer and 
         * returns either a list of appointments or a 404 Not Found response if no appointments exist.
         * 
         * @return A list of `ReadAppointmentDTO` objects.
         */
        [HttpGet]
        public ActionResult<List<ReadAppointmentDTO>> GetAppointments()
        {
            try
            {
                // The retrieved data is stored in the appointmentList variable.
                var appointmentList = _appointmentLogic.GetAppointments();
                var donorList = _donorLogic.GetDonors();
                // Converts the provided lists of Appointment objects and Donor objects into a list of ReadAppointmentDTO objects.
                var appointmentDTOList = AppointmentDTOConvert.ToReadAppointmentDTOList(appointmentList, donorList);

                // Check if the appointmentList contains any appointments (i.e., the count is greater than 0).
                // If there are appointments, return them.
                if (appointmentDTOList.Count > 0)
                {
                    // If appointments are found, return an HTTP 200 OK status code with the appointment list as the response body.
                    return Ok(appointmentDTOList);
                }
                else
                {
                    // If the appointment list is empty (i.e., no appointments are found), return an HTTP 404 Not Found status code 
                    // along with a message indicating no appointments were found.
                    return NotFound("No appointments found.");
                }
            }
            catch
            {
                // If any exception occurs while retrieving the appointments (e.g., database error or network issue), 
                // catch the exception and return an HTTP 500 Internal Server Error status code with a relevant error message.
                return StatusCode(500, "An error occurred while retrieving appointments.");
            }
        }

        /**
         * Handles POST requests to insert a new appointment into the system.
         * It validates the input appointment data (`CreateAppointmentDTO`), and if valid, 
         * calls the `InsertAppointment` method from the business logic layer.
         * 
         * @param appointmentDTO The appointment data transfer object.
         * @return An `ActionResult` indicating the result of the operation.
         */
        [HttpPost]
        // The method definition, which takes an object of type CreateAppointmentDTO as input. 
        // The [FromBody] attribute indicates that the data should be deserialized from the request body.
        public ActionResult InsertAppointment([FromBody] CreateAppointmentDTO appointmentDTO)
        {
            // Check if the input DTO is null. If it is, the appointment data is invalid and cannot be processed.
            if (appointmentDTO == null)
            {
                // Return a 400 BadRequest response with an error message indicating invalid data.
                return BadRequest("Invalid appointment data.");
            }

            try
            {
                // Extract the donor ID (FK_donorId) from the CreateAppointmentDTO (appointmentDTO) object.
                // The donor ID is used to associate the appointment with the correct donor in the database.
                int donorId = appointmentDTO.FK_donorId;

                // Convert the DTO to the Appointment domain model
                Appointment appointment = AppointmentDTOConvert.ConvertToAppointment(appointmentDTO);

                // Call the InsertAppointment method from the business logic layer
                // It passes the appointment data (appointmentDTO) and the extracted donor ID (donorId) to insert the new appointment into the system.
                // The method returns a boolean value indicating whether the insertion was successful (true) or not (false).
                bool isInserted = _appointmentLogic.InsertAppointment(appointment, donorId);

                // Check if the appointment was successfully inserted.
                if (isInserted)
                {
                    // If the insertion is successful, return a 200 OK response with a success message.
                    return Ok("Appointment successfully inserted.");
                }
                else
                {
                    // If the insertion fails (e.g., a database error), return a 500 Internal Server Error response with an error message.
                    return StatusCode(500, "The selected time slot is already taken. Please choose another one.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /**
         * Handles DELETE requests to remove an appointment by ID.
         * Calls the `DeleteAppointment` method from the appointment logic layer and 
         * returns a success message if the appointment is deleted successfully,
         * or a 404 status code if the appointment is not found.
         * 
         * @param id The appointment ID.
         * @return An `ActionResult` indicating the result of the operation.
         */
        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Retrieve the appointment by ID to check if it exists
                var appointment = _appointmentLogic.GetAppointmentById(id);

                // If the appointment is not found, return a 404 Not Found response
                if (appointment == null)
                {
                    return NotFound("Appointment not found.");
                }

                // Call the DeleteAppointment method from the business logic layer
                bool isDeleted = _appointmentLogic.DeleteAppointment(appointment);

                // If the deletion is successful, return a 200 OK response with a success message
                if (isDeleted)
                {
                    return Ok("Appointment successfully deleted.");
                }
                else
                {
                    // If the deletion fails, return a 500 Internal Server Error response with an error message
                    return StatusCode(500, "Failed to delete appointment.");
                }
            }
            catch (Exception ex)
            {
                // If any exception occurs, return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        /**
         * Handles GET requests to retrieve all appointments for a specific donor.
         * Calls the `GetAppointmentsByDonorId` method from the appointment logic layer and 
         * returns either a list of appointments or a 404 Not Found response if no appointments exist for the donor.
         * 
         * @param donorId The donor ID.
         * @return A list of `ReadAppointmentDTO` objects.
         */
        // GET api/<AppointmentsController>/donor/{donorId}
        [HttpGet("donor/{donorId}")]
        public ActionResult<List<ReadAppointmentDTO>> GetAppointmentsByDonorId(int donorId)
        {
            try
            {
                // The retrieved data is stored in the appointmentList variable.
                var appointmentList = _appointmentLogic.GetAppointmentsByDonorId(donorId);
                var donorList = _donorLogic.GetDonors();
                // Convert the list of Appointment objects and the list of Donor objects into a list of ReadAppointmentDTO objects.
                var appointmentDTOList = AppointmentDTOConvert.ToReadAppointmentDTOList(appointmentList, donorList);

                // Checks if the appointmentList contains any appointments (i.e., the count is greater than 0).
                // If there are appointments, it proceeds to return them.
                if (appointmentDTOList.Count > 0)
                {
                    // If appointments are found, it returns an HTTP 200 OK status code with the appointment list as the response body.
                    return Ok(appointmentDTOList);
                }
                else
                {
                    // If the appointment list is empty (i.e., no appointments are found), it returns an HTTP 404 Not Found status code 
                    // along with a message indicating no appointments were found.
                    return NotFound("No appointments found.");
                }
            }
            catch
            {
                // If any exception occurs while retrieving the appointments (e.g., database error or network issue), 
                // it catches the exception and returns an HTTP 500 Internal Server Error status code with a relevant error message.
                return StatusCode(500, "An error occurred while retrieving appointments.");
            }
        }


        /**
         * Handles DELETE requests to remove an appointment by donor ID and start time.
         * Calls the `DeleteAppointmentByStartTime` method from the appointment logic layer and 
         * returns a success message if the appointment is deleted successfully,
         * or a 404 status code if the appointment is not found.
         * 
         * @param donorId The donor ID.
         * @param startTime The start time of the appointment.
         * @return An `ActionResult` indicating the result of the operation.
         */
        // DELETE api/<AppointmentsController>/donor/{donorId}/{startTime}
        [HttpDelete("{donorId}/{startTime}")]
        public ActionResult Delete(int donorId, DateTime startTime)
        {
            try
            {
                // Attempt to delete the appointment by its start time
                bool isDeleted = _appointmentLogic.DeleteAppointmentByStartTime(startTime);

                // If the deletion is successful, return a 200 OK response with a success message
                if (isDeleted)
                {
                    return Ok("Appointment deleted successfully.");
                }
                else
                {
                    // If the appointment is not found or does not belong to the specified donor, return a 404 Not Found response
                    return NotFound("Appointment not found or does not belong to the specified donor.");
                }
            }
            catch (Exception ex)
            {
                // If any exception occurs, return a 500 Internal Server Error response with the exception message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

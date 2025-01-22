using API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    /// <summary>
    /// The <c>DonorsController</c> class is a Web API controller that manages HTTP requests related to donor operations.
    /// It provides API endpoints for handling donor data, including retrieval, insertion, update, and deletion of donors.
    /// </summary>
    [Route("api/[controller]")] // Placeholder. Generates a route based on the controllers name
    [ApiController] // Parameter binding
    public class DonorsController : ControllerBase
    {
        private readonly IDonorLogic _donorLogic;

        /// <summary>
        /// Initializes a new instance of the <c>DonorsController</c> class.
        /// Constructor injection is used to pass the <c>IDonorLogic</c> service into the controller.
        /// This allows the controller to interact with the business logic layer to manage donor data.
        /// </summary>
        /// <param name="donorLogic">The donor logic service.</param>
        public DonorsController(IDonorLogic donorLogic)
        {
            _donorLogic = donorLogic;
        }

        /// <summary>
        /// Handles GET requests to fetch all donors.
        /// It queries the business logic layer for the list of donors.
        /// If donors are found, it returns an HTTP 200 OK response with the donor list.
        /// If no donors are found, it returns an HTTP 404 Not Found response.
        /// </summary>
        /// <returns>A list of <c>ReadDonorDTOForDesktop</c> objects.</returns>
        [HttpGet]
        public ActionResult<List<ReadDonorDTOForDesktop>> Get()
        {
            var donorList = _donorLogic.GetDonors();

            if (donorList.Count > 0)
            {
                // Convert domain models to DTOs (ReadDonorDTOForDesktop objects)
                var donorDTOs = DonorDTOConvert.ToDonorDTOForDesktopList(donorList);
                // Return the converted list of DTOs using the Ok() method.
                // The Ok() method is a helper in the ActionResult class used to return a successful HTTP response with the specified data.
                return Ok(donorDTOs); // serializes to JSON through Ok
            }
            else
            {
                return NotFound("No donor found");
            }
        }
        /// <summary>
        /// Handles GET requests to fetch a specific donor by their ID.
        /// It queries the business logic layer to find the donor with the given ID.
        /// If the donor is found, it returns the donor details with an HTTP 200 OK status.
        /// If no donor is found, it returns an HTTP 404 Not Found status.
        /// </summary>
        /// <param name="id">The donor ID.</param>
        /// <returns>A <c>Donor</c> object.</returns>
        // GET api/<DonorsController>/id/{id}
        [HttpGet("{id}")]
        public ActionResult<Donor> GetDonorById(int id)
        {
            // Call the GetDonorById method from the business logic layer to fetch the donor by ID
            var donor = _donorLogic.GetDonorById(id);

            // If the donor is not found (null), return a NotFound (404) response with an appropriate error message
            if (donor == null)
            {
                return NotFound("No donor found with the given ID");
            }
            // If the donor is found, return an OK (200) response with the donor details
            return Ok(donor);
        }

        /// <summary>
        /// Handles HTTP POST requests to create a new donor in the system.
        /// It expects a donor object to be sent in the body of the request.
        /// If the donor is valid and does not already exist, it attempts to insert the donor into the database.
        /// If the insertion is successful, it returns an HTTP 201 Created status with the donor's ID.
        /// If the donor already exists, it returns an HTTP 409 Conflict status.
        /// </summary>
        /// <param name="donor">The donor object.</param>
        /// <returns>An <c>IActionResult</c> indicating the result of the operation.</returns>
           // POST api/<DonorController>
        [HttpPost]
        public IActionResult InsertDonor([FromBody] Donor donor)
        {
            // Check if the donor information is provided in the request body.
            if (donor == null)
            {
                // If the donor is null, it returns a BadRequest (400) response with an error message.
                return BadRequest("No Donor Information provided");
            }

            // Checks if the donors cpr number is already registered
            if (_donorLogic.IsCprNoAlreadyRegistered(donor.CprNo))
            {
                return Conflict("A donor with this CPR number already exists.");
            }

            // Sets the DonorId to null so that the database can auto-generate the ID for the new donor.
            donor.DonorId = null;

            try
            {
                // Inserts the donor using the InsertDonor method from DonorLogic.
                bool wasInserted = _donorLogic.InsertDonor(donor);

                if (wasInserted)
                {
                    // If the donor was successfully inserted, return a Created (201) status with a success message.
                    return StatusCode(StatusCodes.Status201Created, _donorLogic.GetDonorIdByCprNo(donor.CprNo));
                }
                else
                {
                    // If the insertion failed, return a Server Error (500) status with an error message.
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting donor");
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs during the insertion process, return a Server Error (500) status with the exception message.
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles PUT requests to update an existing donor's information.
        /// It accepts a donor object in the request body and updates the donor's details based on their CPR number.
        /// If the donor is updated successfully, it returns the updated donor data with an HTTP 200 OK status.
        /// Otherwise, it returns an HTTP 500 Internal Server Error if the update fails.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to update.</param>
        /// <param name="inDonor">The donor object with updated information.</param>
        /// <returns>An <c>IActionResult</c> indicating the result of the operation.</returns>
        // PUT api/<DonorController>/5
        [HttpPut("{cprNo}")]
        public IActionResult UpdateDonor(string cprNo, [FromBody] Donor inDonor)
        {
            // Holds the result of an action - allowing it to return different types of responses such as Ok, NotFound, BadRequest etc.
            IActionResult actionResult;

            if (inDonor == null)
            {
                // If inDonor is null, return a BadRequest (400) with an error message
                actionResult = BadRequest("No Donor Information entered");
            }
            else
            {
                // Set DonorId to null to ensure that the database can handle auto-generating the donor ID
                inDonor.DonorId = null;

                // Call the UpdateDonor method in the business logic layer to update the donor
                var updatedDonor = _donorLogic.UpdateDonor(inDonor);

                // Check if the update was successful (updatedDonor should not be null)
                if (updatedDonor == null)
                {
                    // If the update failed, return a 500 Internal Server Error with an error message
                    actionResult = StatusCode(StatusCodes.Status500InternalServerError, "Error updating donor.");
                }
                else
                {
                    // Convert the updated donor to a DTO and return it as part of the response
                    var updatedDonorDTO = DonorDTOConvert.ToDonorDTOForDesktop(updatedDonor);
                    actionResult = Ok(updatedDonorDTO);
                }
            }
            return actionResult;
        }

        /// <summary>
        /// Handles DELETE requests to remove a donor from the system by their CPR number.
        /// If a valid CPR number is provided, it calls the business logic layer to delete the donor.
        /// If the donor is deleted successfully, it returns an HTTP 200 OK status.
        /// If the CPR number is invalid, it returns a BadRequest status.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to delete.</param>
        /// <returns>An <c>IActionResult</c> indicating the result of the operation.</returns>
        // DELETE api/<DonorController>/5
        [HttpDelete("{cprNo}")]
        public IActionResult DeleteDonor(string cprNo)
        {
            // Create a new Donor object to store the provided CPR number
            var donorCpr = new Donor();
            // Assign the provided CPR number to the Donor object
            donorCpr.CprNo = cprNo;

            // Declares a variable to store the action result that will be returned to the client
            IActionResult actionResult;

            // Check if the CPR number is empty or null by counting the length
            if (donorCpr.CprNo.Count() > 0)
            {
                // If no CPR number is provided, return a BadRequest (400) response with an error message
                actionResult = BadRequest("No CprNo Information intered");
            }
            // Call the DeleteDonor method in the business logic layer to delete the donor from the system
            // Returns an OK (200) response if the donor is deleted successfully
            return Ok(_donorLogic.DeleteDonor(donorCpr));
        }

        /// <summary>
        /// Handles GET requests to retrieve a specific donor by their CPR number.
        /// It queries the business logic layer to find the donor with the given CPR number.
        /// If the donor is found, it returns the donor details with an HTTP 200 OK status.
        /// If no donor is found, it returns an HTTP 404 Not Found status.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to retrieve.</param>
        /// <returns>An <c>IActionResult</c> indicating the result of the operation.</returns>
        //GET api/<DonorController>/cpr/{cprNo}
        [HttpGet("cpr/{cprNo}")]
        public IActionResult GetDonorByCprNo(string cprNo)
        {
            // Call the GetDonorByCprNo method from the business logic layer to fetch the donor by CPR number
            var donor = _donorLogic.GetDonorByCprNo(cprNo);

            // If the donor is not found (null), return a NotFound (404) response with an appropriate error message
            if (donor == null)
            {
                return NotFound("No donor found with the given CPR number");
            }
            // If the donor is found, return an OK (200) response with the donor details
            return Ok(donor); // serializes to JSON through Ok
        }
    }
}

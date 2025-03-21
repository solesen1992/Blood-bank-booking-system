﻿using DesktopApp.Model;
using DesktopApp.ServiceLayer;
using Microsoft.Extensions.Configuration;

namespace DesktopApp.BusinessLogicLayer
{
    /// <summary>
    /// Provides business logic for donor-related operations.
    /// </summary>
    public class DonorLogic : IDonorLogic
    {
        readonly IDonorServiceAccess _donorServiceAccess;
        readonly IAppointmentLogic _appointmentLogic;

        public DonorLogic()
        {
            _donorServiceAccess = new DonorServiceAccess();
            _appointmentLogic = new AppointmentLogic();
        }

        /// <summary>
        /// Gets all donors.
        /// </summary>
        /// <returns>A list of all donors.</returns>
        public async Task<List<Donor>?> GetAllDonors()
        {
            List<Donor>? foundDonors = null; 

            if (_donorServiceAccess != null) 
            {
                foundDonors = await _donorServiceAccess.GetDonors(); 
            }
            return foundDonors; 
        }

        /// <summary>
        /// Searches for donors based on the search string and returns a list of donors whose properties match the search string.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>A list of donors that match the search criteria.</returns>
        public async Task<List<Donor>?> SearchDonor(string search)
        {
            // Initialize a list to hold the found donors
            List<Donor>? foundDonors = null;

            // Check if the donor service access object is not null
            if (_donorServiceAccess != null)
            {
                // Fetch all donors from the service
                foundDonors = await _donorServiceAccess.GetDonors();

                // Check if the fetched donors list is not null
                if (foundDonors != null)
                {
                    // Initialize a list to hold the filtered donors based on the search string
                    List<Donor> filteredDonors = new List<Donor>();

                    // Iterate through each donor in the fetched donors list
                    foreach (var donor in foundDonors)
                    {
                        if (donor.DonorFirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.DonorLastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.CprNo.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.DonorPhoneNo.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.DonorEmail.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.DonorStreet.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.City.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            donor.ZipCode.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            // Handle nullable BloodType property
                            donor.BloodType?.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) == true) 
                        {
                            // If any property matches the search string, add the donor to the filtered list
                            filteredDonors.Add(donor);
                        }
                    }
                    // Assign the filtered donors list to the found donors list
                    foundDonors = filteredDonors;
                }
            }
            // Return the list of found donors that match the search criteria
            return foundDonors;
        }

        /// <summary>
        /// Retrieves a donor by their CPR number asynchronously from the service.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor.</param>
        /// <returns>The donor that matches the CPR number, or null if no donor is found.</returns>
        public async Task<Donor?> GetDonorByCprNo(string cprNo)
        {
            // Initialize 'foundDonor' as null. This will hold the donor data if found.
            Donor? foundDonor = null;

            // Check if the donor service access object is not null
            if (_donorServiceAccess != null)
            {
                // Fetches the donor by CPR number from the '_donorServiceAccess' service using the 'GetDonorByCprNo' method
                // and assigns the result to 'foundDonor'
                foundDonor = await _donorServiceAccess.GetDonorByCprNo(cprNo);
            }
            // Return the found donor, or null if no donor was found
            return foundDonor;
        }

        /// <summary>
        /// Updates the donor information.
        /// </summary>
        /// <param name="cprNo">The CPR number of the donor to update.</param>
        /// <param name="updatedDonor">The updated donor information.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        /// <exception cref="ArgumentException">Thrown when the CPR number is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the updated donor information is null.</exception>
        public async Task<bool> UpdateDonor(string cprNo, Donor updatedDonor)
        {
            // Validate the input
            if (string.IsNullOrEmpty(cprNo)) // Check if the provided CPR number is null or empty.
            {
                throw new ArgumentException("CPR number cannot be null or empty.", nameof(cprNo));
            }
            if (updatedDonor == null) // Check if the provided updated donor information is null.
            {
                throw new ArgumentNullException(nameof(updatedDonor), "Updated donor information cannot be null.");
            }

            // Ensure the CPR number in the donor matches the input
            if (updatedDonor.CprNo != cprNo) // Check if the CPR number in the updated donor object matches the input CPR number.
            {
                updatedDonor.CprNo = cprNo; // If they don't match, set the donor's CPR number to the input value to maintain consistency.
            }

            // Call the service layer to update the donor
            if (_donorServiceAccess != null) // Check if the donor service access object is initialized.
            {
                // If initialized, asynchronously call the UpdateDonor method on the service layer with the updated donor information.
                return await _donorServiceAccess.UpdateDonor(cprNo, updatedDonor); 
            }

            // Return false if the service layer is not initialized
            return false;
        }

        /*
         * These methods use the original methods from AppointmentLogic, so the DonorLogic class acts as a
         * mediator between the UI and the AppointmentLogic class.
         */

        // Asynchronously fetches all appointments by delegating the call to the appointment logic layer.
        public async Task<List<Appointment>?> GetAllAppointments()
        {
            // Wait for and return the result from the logic layer's GetAllAppointments method.
            return await _appointmentLogic.GetAllAppointments();
        }

        // Asynchronously fetches upcoming and whole-day appointments.
        public async Task<List<Appointment>?> GetUpcomingAndWholeDayAppointments()
        {
            return await _appointmentLogic.GetUpcomingAndWholeDayAppointments();
        }

        // Asynchronously searches for appointments matching the specified search string.
        public async Task<List<Appointment>?> SearchAppointments(string search)
        {
            return await _appointmentLogic.SearchAppointments(search);
        }

        // Asynchronously fetches an appointment by a specific CPR number.
        public async Task<Appointment?> GetAppointmentByCprNo(string cprNo)
        {
            return await _appointmentLogic.GetAppointmentByCprNo(cprNo);
        }

        // Asynchronously fetches an appointment by CPR number, filtering only for upcoming appointments.
        public async Task<Appointment?> GetAppointmentByCprNoOnlyOnUpcomingAppointsment(string cprNo)
        {
            return await _appointmentLogic.GetAppointmentByCprNoOnlyOnUpcomingAppointsment(cprNo);
        }

        // Deletes an appointment by its donor ID and start time without awaiting a result.
        public bool DeleteAppointmentByStartTime(int donorId, DateTime startTime)
        {
            // Call the business layer to delete the appointment by start time
            return _appointmentLogic.DeleteAppointmentByStartTime(donorId, startTime);
        }
    }
}

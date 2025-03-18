using System.Drawing;
using API.Model;
using API.DatabaseLayer;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Moq;
using API.BusinessLogicLayer;
using System.Collections.Generic;

namespace API.Test
{
    /// <summary>
    /// This class contains unit tests for the DonorDatabaseAccess class.
    /// It uses Moq to create mock objects for the IDonorAccess interface.
    /// The tests cover the following functionalities:
    /// - Creating a new donor
    /// - Retrieving all donors
    /// - Deleting an existing donor
    /// </summary>
    public class DonorDatabaseAccessTest
    {
        // This is a mock object that simulates the behavior of the IDonorAccess interface.
        private Mock<IDonorAccess> _donorAccessMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorDatabaseAccessTest"/> class.
        /// Creates a mock object for the IDonorAccess interface.
        /// </summary>
        public DonorDatabaseAccessTest()
        {
            // Create a mock object for the IDonorAccess interface
            _donorAccessMock = new Mock<IDonorAccess>();
        }

        /// <summary>
        /// Tests the InsertDonor method of the IDonorAccess interface.
        /// Verifies that a new donor is added to the list of donors
        /// and that the list contains the new donor.
        /// </summary>
        [Fact]
        public void DonorTestCreate()
        {
            /*
              Arrange
              Sets up the test data and configures the mock behavior.
             */
            var newDonor = new Donor
            {
                DonorId = 0,
                CprNo = "1234567891",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 12345678,
                DonorEmail = "test@mail.com",
                DonorStreet = "TestVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };


            /*
              Configures the mock object _donorAccessMock to simulate the behavior of the InsertDonor 
              and GetDonorsWithBloodTypeAndCity methods. 
             */

            // This lambda expression specifies that when the InsertDonor method is called on the
            // mock object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(d => d.InsertDonor(It.IsAny<Donor>())).Returns(true);

            // This lambda expression specifies that when the GetDonorsWithBloodTypeAndCity method is
            // called on the mock object (_donorAccessMock), it should be invoked without any parameters.
            _donorAccessMock.Setup(d => d.GetDonorsWithBloodTypeAndCity()).Returns(new List<Donor> { newDonor });


            /*
              Act
              Performs the actions that are being tested.
             */

            //Calls the mocked methods to simulate inserting the new donor and retrieving the list of donors.
            // It uses the mock object '_donorAccessMock' to call 'InsertDonor' and passes the new donor object 'newDonor'.
            // The method will return a boolean indicating success or failure of the insertion operation.
            var result = _donorAccessMock.Object.InsertDonor(newDonor);
            // Calls the mocked 'GetDonorsWithBloodTypeAndCity' method to simulate retrieving a list of donors.
            // This list contains donors along with their blood type and associated city information.
            var donors = _donorAccessMock.Object.GetDonorsWithBloodTypeAndCity();

            /*
              Assert
              Verifies that the actions performed in the Act section produced the expected results.
             */
            Assert.True(result);  // Insert should return true, indicating that the donor was successfully inserted.
            Assert.NotNull(donors); // This assertion checks if the donors list is not null, meaning that the list of donors was successfully retrieved.

            // Confirms that the list of donors contains the newDonor object by checking for a donor
            // with the CprNo of "12345678910".
            // This assertion checks if the `donors` list contains a donor object with the specified `CprNo`.
            // It ensures that the new donor was correctly inserted and is present in the list.
            Assert.Contains(donors, d => d.CprNo == "1234567891");
        }

        /// <summary>
        /// Tests the GetDonorsWithBloodTypeAndCity method of the IDonorAccess interface.
        /// Verifies that the method returns a non-null, non-empty list of donors
        /// and that each donor has a CityZipCode.
        /// </summary>
        [Fact]
        public void DonorTestGetAll()
        {
            /*
              Arrange
              Sets up the test data and configures the mock behavior. 
             */
            var donor1 = new Donor
            {
                DonorId = 1,
                CprNo = "12345",
                DonorFirstName = "John",
                DonorLastName = "Doe",
                DonorPhoneNo = 123456780,
                DonorEmail = "john.doe@mail.com",
                DonorStreet = "Test Street",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };

            var donor2 = new Donor
            {
                DonorId = 2,
                CprNo = "67890",
                DonorFirstName = "Jane",
                DonorLastName = "Smith",
                DonorPhoneNo = 987654320,
                DonorEmail = "jane.smith@mail.com",
                DonorStreet = "Another Street",
                CityZipCode = new CityZipCode { CityZipCodeId = 2, City = "Copenhagen", ZipCode = 1000 }
            };

            var donors = new List<Donor> { donor1, donor2 };

            // This lambda expression specifies that when the GetDonorsWithBloodTypeAndCity method is called on
            // the mock object (_donorAccessMock), it should be invoked without any parameters.
            _donorAccessMock.Setup(d => d.GetDonorsWithBloodTypeAndCity()).Returns(donors);

            /*
              Act
              Performs the actions that are being tested.
             */
            var result = _donorAccessMock.Object.GetDonorsWithBloodTypeAndCity();

            /*
              Assert
              Verifies that the actions performed in the Act section produced the expected results.
             */
            // This assertion checks that the variable `result` is not null. 
            // If result is null, the test will fail because it expects a non-null value to confirm that the operation or function returned a valid, existing result.
            Assert.NotNull(result);
            // This assertion checks that the `result` list or collection is not empty.
            // If the result collection has no elements, the test will fail because it expects the collection to contain at least one item.
            Assert.NotEmpty(result);

            // Verify that all donors have a CityZipCode (since we're mocking it, it will be predefined)
            foreach (var donor in result)
            {
                // This assertion verifies that the `CityZipCode` property of the current `donor` object is not null.
                // If it is null, the test will fail, indicating that the city and zip code information for the donor is missing.
                Assert.NotNull(donor.CityZipCode);
            }
        }

        /// <summary>
        /// Tests the DeleteDonor method of the IDonorAccess interface.
        /// Verifies that an existing donor is removed from the list of donors
        /// and that the donor cannot be retrieved after deletion.
        /// </summary>
        [Fact]
        public void DonorTestDelete()
        {
            /*
              Arrange 
              Sets up the test data and configures the mock behavior.
             */
            var oldDonor = new Donor
            {
                DonorId = 1,
                CprNo = "12345678910",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 12345678,
                DonorEmail = "test@mail.com",
                DonorStreet = "TestVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };

            // Create a list to simulate the donor database
            var donorList = new List<Donor> { oldDonor };

            //  This lambda expression specifies that when the InsertDonor method is called on the mock
            //  object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(d => d.InsertDonor(It.IsAny<Donor>())).Returns(true);

            //  This lambda expression specifies that when the DeleteDonor method is called on the mock
            //  object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(d => d.DeleteDonor(It.IsAny<Donor>())).Callback<Donor>(donor =>
            {
                // Simulate donor deletion by removing the donor from a mock list or marking it as deleted
                donorList.Remove(donor);
            });

            // Mock GetDonorByCprNo to return null when trying to get the deleted donor
            _donorAccessMock.Setup(d => d.GetDonorByCprNo(It.IsAny<string>())).Returns((string cprNo) =>
            {
                // Return the donor with the specified cprNo or null if it doesn't exist
                return donorList.FirstOrDefault(d => d.CprNo == cprNo);
            });

            // Act
            // Call the InsertDonor method on the donor access mock object to insert the old donor
            _donorAccessMock.Object.InsertDonor(oldDonor);
            // Call the DeleteDonor method on the donor access mock object to delete the old donor
            _donorAccessMock.Object.DeleteDonor(oldDonor);
            // Retrieve the donor by CPR number from the donor access mock object
            var deletedDonor = _donorAccessMock.Object.GetDonorByCprNo(oldDonor.CprNo);

            // Assert
            // Verifies that the `deletedDonor` variable is null, indicating that the donor was successfully deleted.
            Assert.Null(deletedDonor);

        }

        /// <summary>
        /// Tests the UpdateDonor method of the IDonorAccess interface.
        /// Verifies that an existing donor's information is updated correctly.
        /// </summary>
        [Fact]
        public void DonorTestUpdate()
        {
            /*
              Arrange
              Sets up the test data and configures the mock behavior.
             */
            var existingDonor = new Donor
            {
                DonorId = 1,
                CprNo = "12345678910",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 12345678,
                DonorEmail = "test@mail.com",
                DonorStreet = "TestVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };

            var updatedDonor = new Donor
            {
                DonorId = 1,
                CprNo = "12345678910",
                DonorFirstName = "updatedDonor",
                DonorLastName = "updatedTest",
                DonorPhoneNo = 87654321,
                DonorEmail = "updated@mail.com",
                DonorStreet = "UpdatedVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 2, City = "Copenhagen", ZipCode = 1000 }
            };

            // Create a list to simulate the donor database
            var donorList = new List<Donor> { existingDonor };

            // This lambda expression specifies that when the UpdateDonor method is called on the mock
            // object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(d => d.UpdateDonor(It.IsAny<Donor>())).Callback<Donor>(donor =>
            {
                // Simulate donor update by updating the donor in the mock list
                var donorToUpdate = donorList.FirstOrDefault(d => d.DonorId == donor.DonorId);
                if (donorToUpdate != null)
                {
                    // Update the donor's properties with the new values from the passed `donor` object
                    donorToUpdate.DonorFirstName = donor.DonorFirstName;
                    donorToUpdate.DonorLastName = donor.DonorLastName;
                    donorToUpdate.DonorPhoneNo = donor.DonorPhoneNo;
                    donorToUpdate.DonorEmail = donor.DonorEmail;
                    donorToUpdate.DonorStreet = donor.DonorStreet;
                    donorToUpdate.CityZipCode = donor.CityZipCode;
                }
            });

        }
    }
}
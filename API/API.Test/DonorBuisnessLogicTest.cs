using API.BusinessLogicLayer;
using API.Model;
using API.DatabaseLayer;
using Moq;

namespace API.Test
{

    /// <summary>
    /// This class contains unit tests for the DonorLogic class.
    /// It uses Moq to create mock objects for the IDonorAccess interface.
    /// The tests cover the following functionalities:
    /// - Retrieving all donors
    /// - Creating a new donor
    /// - Deleting an existing donor
    /// - Updating an existing donor
    /// </summary>
    public class DonorBuisnessLogicTest
    {
        // This is a mock object that simulates the behavior of the IDonorAccess interface.
        private Mock<IDonorAccess> _donorAccessMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorBuisnessLogicTest"/> class.
        /// Creates a mock object for the IDonorAccess interface.
        /// </summary>
        public DonorBuisnessLogicTest()
        {
            // Create a mock object for the IDonorAccess interface
            // It now holds a fake/mock object that "mimics" IDonorAccess for testing purposes.
            _donorAccessMock = new Mock<IDonorAccess>();
        }

        /// <summary>
        /// Tests the GetDonors method of the DonorLogic class.
        /// Verifies that the method returns a non-null, non-empty list of donors
        /// and that the first donor's first name is "Test".
        /// </summary>
        [Fact]
        public void DonorTestGetAll()
        {
            // Arrange

            // This lambda expression specifies that when the GetDonorsWithBloodTypeAndCity method is
            // called on the mock IDonorAccess object (da), it should return a predefined list of donors.
            _donorAccessMock.Setup(da => da.GetDonorsWithBloodTypeAndCity())
                           .Returns(new List<Donor> // Specifies what the method will return = a predefined list of donors.
                           {
                           new Donor
                           {
                               DonorId = 1,
                               CprNo = "1111111111",
                               DonorFirstName = "Test",
                               DonorLastName = "User",
                               DonorPhoneNo = 111111,
                               DonorEmail = "test@mail.com",
                               DonorStreet = "Mock Street",
                               CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
                           }
                           });

            
            // Creates an instance of the `DonorLogic` class.
            // The `_donorAccessMock.Object` provides the mock implementation of `IDonorAccess` to the `DonorLogic` constructor.
            var donorLogic = new DonorLogic(_donorAccessMock.Object);

            // Act
            // Calls the `GetDonors` method from the `DonorLogic` class.
            // Internally, `GetDonors` will call the mocked `GetDonorsWithBloodTypeAndCity` method, which returns the predefined list of donors.
            var donors = donorLogic.GetDonors();

            // Assert
            Assert.NotNull(donors); // Verifies that the returned list is not null.
            Assert.NotEmpty(donors); // Verifies that the returned list is not empty.
            Assert.Equal("Test", donors.First().DonorFirstName); // Verifies that the `DonorFirstName` of the first donor in the list is equal to "Test".
        }


        /// <summary>
        /// Tests the InsertDonor method of the DonorLogic class.
        /// Verifies that a new donor is added to the list of donors
        /// and that the list contains the new donor.
        /// </summary>
        [Fact]
        public void DonorTestCreate()
        {
            // Arrange
            var donorsList = new List<Donor>();

            // This lambda expression specifies that when the InsertDonor method is called on the
            // mock object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(da => da.InsertDonor(It.IsAny<Donor>()))
                            .Callback<Donor>(donor => donorsList.Add(donor));

            // This lambda expression specifies that when the GetDonorsWithBloodTypeAndCity method is
            // called on the mock object (_donorAccessMock), it should be invoked without any parameters.
            _donorAccessMock.Setup(da => da.GetDonorsWithBloodTypeAndCity())
                            .Returns(donorsList);

            var donorLogic = new DonorLogic(_donorAccessMock.Object);

            Donor newDonor = new Donor
            {
                DonorId = 0,
                CprNo = "2345678910",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 111111,
                DonorEmail = "test@mail.com",
                DonorStreet = "TestVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };

            // Act
            donorLogic.InsertDonor(newDonor);
            var donors = donorLogic.GetDonors();

            // Assert
            Assert.NotNull(donors);
            Assert.NotEmpty(donors);

            // Confirms that the list of donors contains the newDonor object by checking for a donor
            // with the CprNo of "2345678910".
            Assert.Contains(donors, donor => donor.DonorFirstName == newDonor.DonorFirstName && donor.CprNo == newDonor.CprNo);
        }

        /// <summary>
        /// Tests the DeleteDonor method of the DonorLogic class.
        /// Verifies that an existing donor is removed from the list of donors
        /// and that the list does not contain the deleted donor.
        /// </summary>
        [Fact]
        public void DonorTestDelete()
        {
            // Arrange
            var donorsList = new List<Donor>();

            // This lambda expression specifies that when the InsertDonor method is called on the
            // mock object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(da => da.InsertDonor(It.IsAny<Donor>()))
                           .Callback<Donor>(donor => donorsList.Add(donor));

            // This lambda expression specifies that when the GetDonorsWithBloodTypeAndCity method is
            // called on the mock object (_donorAccessMock), it should be invoked without any parameters.
            _donorAccessMock.Setup(da => da.GetDonorsWithBloodTypeAndCity())
                           .Returns(donorsList);

            // This lambda expression specifies that when the DeleteDonor method is called on the
            // mock object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(da => da.DeleteDonor(It.IsAny<Donor>()))
                            // This lambda expression specifies that when the DeleteDonor method is called on the
                            // mock object (_donorAccessMock), it should remove the donor object from the donorsList.
                            .Callback<Donor>(donor => donorsList.Remove(donor));

            var donorLogic = new DonorLogic(_donorAccessMock.Object); // Create an instance of the DonorLogic class

            var oldDonor = new Donor
            {
                DonorId = 0,
                CprNo = "3456789012",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 111111,
                DonorEmail = "test@mail.com",
                DonorStreet = "TestVejen",
                CityZipCode = new CityZipCode { CityZipCodeId = 1, City = "Aalborg", ZipCode = 9000 }
            };

            // Act
            donorLogic.InsertDonor(oldDonor); // Insert the old donor
            var donorListBeforeDelete = donorLogic.GetDonors(); // Get the list of donors before deletion

            // Confirms that the list of donors contains the oldDonor object by checking for a donor
            // with the CprNo of "3456789012".
            Assert.Contains(donorListBeforeDelete, d => d.CprNo == oldDonor.CprNo);

            donorLogic.DeleteDonor(oldDonor); // Delete the old donor
            var donorListAfterDelete = donorLogic.GetDonors(); // Get the list of donors after deletion

            // Assert
            // Checks that the list of donors does not contain the oldDonor object by checking for a donor
            Assert.DoesNotContain(donorListAfterDelete, d => d.CprNo == oldDonor.CprNo);

        }

        /// <summary>
        /// Tests the UpdateDonor method of the DonorLogic class.
        /// Verifies that an existing donor's details are updated correctly.
        /// </summary>
        [Fact]
        public void DonorTestUpdate()
        {
            // Arrange
            // Create an instance of DonorLogic with the mock object
            var donorLogic = new DonorLogic(_donorAccessMock.Object);

            var oldDonor = new Donor
            {
                DonorId = 1,
                CprNo = "1111111111",
                DonorFirstName = "donor",
                DonorLastName = "test",
                DonorPhoneNo = 1234567890,
                DonorEmail = "test@mail.com",
                DonorStreet = "Mock Street",
                CityZipCode = new CityZipCode { City = "Aalborg", ZipCode = 9000 },
                BloodType = BloodTypeEnum.APositive
            };


            // Act

            // This lambda expression specifies that when the UpdateDonor method is called on the
            // mock object (_donorAccessMock), it should accept any Donor object as a parameter (It.IsAny<Donor>()).
            _donorAccessMock.Setup(d => d.UpdateDonor(It.IsAny<Donor>()))
                // This lambda expression specifies that when the UpdateDonor method is called on the
                // mock object (_donorAccessMock), it should update the donor object with the specified properties.
                .Callback<Donor>((donor) =>
                {
                    donor.DonorFirstName = "UpdatedDonor";
                    donor.DonorEmail = "updated@mail.com";
                })
                // This lambda expression specifies that when the UpdateDonor method is called on the
                // mock object (_donorAccessMock), it should return the updated donor object.
                .Returns(() => oldDonor);  

            Donor updatedDonor = donorLogic.UpdateDonor(oldDonor);

            // Assert
            // Assert that the updated donor object is not null after the update operation.
            Assert.NotNull(updatedDonor);
            // Assert that the first name of the updated donor matches the expected value "UpdatedDonor".
            Assert.Equal("UpdatedDonor", updatedDonor.DonorFirstName);
            // Assert that the email of the updated donor matches the expected value "updated@mail.com".
            Assert.Equal("updated@mail.com", updatedDonor.DonorEmail);

        }

    }
}

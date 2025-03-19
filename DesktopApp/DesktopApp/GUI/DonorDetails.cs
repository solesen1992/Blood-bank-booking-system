using DesktopApp.BusinessLogicLayer;
using DesktopApp.Model;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace DesktopApp.GUI
{
    /// <summary>
    /// Represents the form for displaying and editing donor details.
    /// </summary>
    public partial class DonorDetails : Form
    {
        private readonly IDonorLogic _donorLogic; // Reference to the DonorLogic class for fetching donor information
        private Donor currentDonor;
        private Appointment currentAppointment;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonorDetails"/> class.
        /// </summary>
        public DonorDetails()
        {
            InitializeComponent();
            _donorLogic = new DonorLogic();
            SetEditingMode(false); // Initially disable editing
            // Populate the ComboBox with blood types from the enum
            BloodtypeScroll.DataSource = Enum.GetValues(typeof(BloodTypeEnum));
        }

        /// <summary>
        /// Handles the click event of the close button to close the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// Handles the click event of the save button to save the donor information.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentDonor == null)
                {
                    MessageBox.Show("Donor not initialized.");
                    return;
                }

                // Update donor fields (other fields)
                UpdateDonorFields();
                // Debug: Log or check the updated donor fields
                Console.WriteLine($"Updated Donor: {currentDonor.DonorFirstName}, {currentDonor.BloodType}");

                // If the blood type is selected, update it
                if (BloodtypeScroll.SelectedValue != null)
                {
                    // Set the blood type ID (FK_bloodTypeId) to the selected value
                    currentDonor.FK_CityZipCodeId = (int)BloodtypeScroll.SelectedValue;
                }

                // Save updated donor information
                bool success = await _donorLogic.UpdateDonor(currentDonor.CprNo, currentDonor);

                if (success)
                {
                    MessageBox.Show("Donor information updated successfully.");
                    SetEditingMode(false);  // Disable editing

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update donor information.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the click event of the edit button to enable editing.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button_Edit_Click(object sender, EventArgs e)
        {
            SetEditingMode(true); // Enable editing
        }

        /// <summary>
        /// Enables or disables editing for all fields except CPR number.
        /// </summary>
        /// <param name="isEditing">if set to <c>true</c> enables editing; otherwise, disables editing.</param>
        private void SetEditingMode(bool isEditing)
        {
            textBox_Firstname.ReadOnly = !isEditing;
            textBox_Lastname.ReadOnly = !isEditing;
            textBox_Adresse.ReadOnly = !isEditing;
            textBox_Email.ReadOnly = !isEditing;
            textBox_PhoneNo.ReadOnly = !isEditing;


            // Ensure CPR number is always read-only
            textBox_CprNo.ReadOnly = true;
            BloodtypeScroll.Enabled = false;
            textBox_ZipCode.ReadOnly = true;
            textBox_City.ReadOnly = true;
        }

        /// <summary>
        /// Sets the donor information to be displayed in the form.
        /// </summary>
        /// <param name="donor">The donor to be displayed.</param>
        public void SetDonor(Donor donor)
        {
            currentDonor = donor;

            // Populate the textboxes with donor information
            textBox_Firstname.Text = donor.DonorFirstName;
            textBox_Lastname.Text = donor.DonorLastName;
            textBox_CprNo.Text = donor.CprNo;
            textBox_Adresse.Text = donor.DonorStreet;
            textBox_Email.Text = donor.DonorEmail;
            textBox_PhoneNo.Text = donor.DonorPhoneNo.ToString();
            // Set the selected blood type in the ComboBox
            //BloodtypeScroll.SelectedItem = donor.BloodType;
            // If no blood type, show a default value or leave it empty
            if (donor.BloodType != null)
            {
                BloodtypeScroll.SelectedItem = donor.BloodType;
            }
            else
            {
                BloodtypeScroll.SelectedItem = null;  // Leave it empty if no blood type
            }

            // Disable the ComboBox for now
            BloodtypeScroll.Enabled = false;
            textBox_ZipCode.Text = donor.ZipCode.ToString();
            textBox_City.Text = donor.City;
        }

        /// <summary>
        /// Updates the donor object fields with the values from the form.
        /// </summary>
        private void UpdateDonorFields()
        {
            if (currentDonor == null)
            {
                MessageBox.Show("Current donor is not set.");
                return; // Exit the method if currentDonor is null
            }
            currentDonor.DonorFirstName = textBox_Firstname.Text;
            currentDonor.DonorLastName = textBox_Lastname.Text;
            currentDonor.DonorStreet = textBox_Adresse.Text;
            currentDonor.DonorEmail = textBox_Email.Text;
            // PhoneNumber (int)
            if (int.TryParse(textBox_PhoneNo.Text, out int phoneNumber))
            {
                currentDonor.DonorPhoneNo = phoneNumber;
            }
            else
            {
                MessageBox.Show("Invalid phone number format.");
            }

            // BloodTypeEnum (assuming it's an enum)
            if (BloodtypeScroll.SelectedItem != null && Enum.TryParse(BloodtypeScroll.SelectedItem.ToString(), out BloodTypeEnum bloodType))
            {
                currentDonor.BloodType = bloodType;
            }
            else
            {
                // If no blood type is selected, set it to null
                currentDonor.BloodType = null;
            }
        }

        /// <summary>
        /// Updates the appointment fields in the form with the upcoming appointment details of the current donor.
        /// </summary>
        public async void UpdateAppointmentFields()
        {
            // Check if the donor has an appointment
            Appointment appointment = await _donorLogic.GetAppointmentByCprNoOnlyOnUpcomingAppointsment(currentDonor.CprNo);
            if (appointment != null)
            {
                textBox_Start.Text = appointment.startTime.ToString();
                textBox_End.Text = appointment.endTime.ToString();
            }
            else
            {
                label_NoTime.Text = "Ingen tid til bloddonation";
                textBox_Start.Text = null;
                textBox_End.Text = null;
            }
        }

        /// <summary>
        /// Handles the click event of the delete button to delete the upcoming appointment of the current donor.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void button_Delete_Click(object sender, EventArgs e)
        {
            Appointment appointment = await _donorLogic.GetAppointmentByCprNoOnlyOnUpcomingAppointsment(currentDonor.CprNo);
            var confirmResult = MessageBox.Show("Er du sikker på, at du vil slette denne tid?",
                                      "Bekræft sletning af tid",
                                      MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _donorLogic.DeleteAppointmentByStartTime(currentDonor.DonorId, appointment.startTime);
            }

            UpdateAppointmentFields();
        }
    }
}

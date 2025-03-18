using DesktopApp.ServiceLayer;
using System.Windows.Forms;
using DesktopApp.Model;
using Newtonsoft.Json;
using DesktopApp.BusinessLogicLayer;
using Microsoft.Extensions.Configuration;
using static System.Windows.Forms.DataFormats;
using DesktopApp.GUI;
using System.Configuration;
using System.Linq.Expressions;

namespace DesktopApp
{
    /// <summary>
    /// The MainPage class is the main form of the application. It interacts with the business logic layer to fetch and display donor information.
    /// The class uses dependency injection to receive configuration settings and initializes UI components and event handlers.
    /// </summary>
    public partial class MainPage : Form
    {
        private readonly IDonorLogic _donorLogic; // Reference to the DonorLogic class for fetching donor information
        private readonly IConfiguration _configuration;
        private DataGridView dataGridView_DonorAppointments;
        private readonly Donor donor;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public MainPage(IConfiguration configuration)
        {
            InitializeComponent(); // Initialize the form components

            _configuration = configuration;
            _donorLogic = new DonorLogic(configuration);

            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;

            dataGridView_DonorInformation.AutoGenerateColumns = false;
            menuStrip.ForeColor = Color.White;
            textBox2.TextChanged += textBox2_TextChanged;
            appointmentsToolStripMenuItem.Click += appointmentsToolStripMenuItem_Click;

            dataGridView_DonorAppointments.AutoGenerateColumns = false;

            // Add the CellFormatting event handler
            dataGridView_DonorAppointments.CellFormatting += dataGridView_DonorAppointments_CellFormatting;
        }

        /// <summary>
        /// Formats the cells in the DataGridView based on the appointment start time.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void dataGridView_DonorAppointments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView_DonorAppointments.Columns["StartTime"].Index) // If the column is the Start Time column
            {
                var cellValue = dataGridView_DonorAppointments.Rows[e.RowIndex].Cells["StartTime"].Value; // Get the cell value
                if (cellValue != null && cellValue is DateTime startTime) // If the cell value is a DateTime
                {
                    if (startTime < DateTime.Now) //If the start time is in the past
                    {
                        dataGridView_DonorAppointments.Rows[e.RowIndex].Cells["StartTime"].Style.BackColor = Color.LightGray; // Set the cell background color to light gray
                        dataGridView_DonorAppointments.Rows[e.RowIndex].Cells["EndTime"].Style.BackColor = Color.LightGray; // Set the cell background color to light gray
                    }
                }
            }
        }

        /// <summary>
        /// Fetches and displays the appointments in the specified DataGridView.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to display the appointments.</param>
        private async Task FetchAndDisplayAppointments(DataGridView dataGridView)
        {
            try
            {
                List<Appointment>? fetchedAppointments = await _donorLogic.GetUpcomingAndWholeDayAppointments(); 

                if (fetchedAppointments == null || !fetchedAppointments.Any()) 
                {
                    MessageBox.Show("No appointments found.", "Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView.DataSource = null;
                    return;
                }

                /* 
                 .OrderBy(a => a.startTime) is a LINQ (Language Integrated Query) method that sorts the fetchedAppointments list.
                 OrderBy is a method that sorts the elements of a sequence(in this case, the list of appointments) in ascending
                 order according to a specified key.
                 The lambda expression a => a.startTime specifies that the sorting should be done based on the startTime property
                 of each Appointment object.Here, a represents each Appointment object in the list.
                 .ToList() converts the sorted sequence (which is an IEnumerable<Appointment>) back into a List<Appointment>.
                */
                var sortedAppointments = fetchedAppointments
                    .OrderBy(a => a.startTime) 
                    .ToList();

                dataGridView.DataSource = sortedAppointments; 
                dataGridView.Refresh(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching appointments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the click event of the "Bloddonor Information" menu item to fetch and display donor information.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void bloddonorInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set all controls within groupBox1 to invisible
            SetControlsInvisible();
            // Set dataGridView_DonorInformation to visible
            dataGridView_DonorInformation.Visible = true;

            label1.Visible = true;

            progressBar1.Visible = true;

            // Set the  textBox2 to visible
            textBox2.Visible = true;
            // Fetch and display donors
            await FetchAndDisplayDonors(dataGridView_DonorInformation);
            // Set groupBox1 text to "Donor Information"
            groupBox1.Text = "Administrer bloddonorer";
        }

        /// <summary>
        /// Fetches and displays the donors in the specified DataGridView.
        /// </summary>
        /// <param name="dataGridView">The DataGridView to display the donors.</param>
        public async Task FetchAndDisplayDonors(DataGridView dataGridView)
        {
            List<Donor>? fetchedDonors = await _donorLogic.GetAllDonors();
            if (fetchedDonors != null)
            {
                foreach (var donor in fetchedDonors)
                {
                    Console.WriteLine($"Donor {donor.DonorFirstName} {donor.DonorLastName}: City={donor.City}, ZipCode={donor.ZipCode}"); 
                }
            }
            dataGridView.DataSource = fetchedDonors;
        }

        /// <summary>
        /// Handles the click event of the "Appointments" menu item to fetch and display appointments.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void appointmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set all controls within groupBox1 to invisible
            SetControlsInvisible();
            // Set dataGridView_DonorAppointments to visible
            dataGridView_DonorAppointments.Visible = true;

            label1.Visible = true;

            progressBar1.Visible = true;

            // Set the  textBox2 to visible
            textBox2.Visible = true;
            // Fetch and display appointments
            await FetchAndDisplayAppointments(dataGridView_DonorAppointments);
            // Set groupBox1 text to "Donor Tider"
            groupBox1.Text = "Donor Tider";
        }

        /// <summary>
        /// Sets all controls within the groupBox1 to invisible.
        /// </summary>
        private void SetControlsInvisible()
        {
            foreach (Control control in groupBox1.Controls)
            {
                control.Visible = false;
            }
        }

        /// <summary>
        /// Handles the text changed event of the search text box to filter the list of donors based on the search text and update the data grid view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void textBox2_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox2.Text; // Get the search text from the text box
            List<Donor>? filteredDonors = await _donorLogic.SearchDonor(searchText); // Search donors asynchronously
            dataGridView_DonorInformation.DataSource = filteredDonors; // Update the data grid view with the filtered donors
        }

        /// <summary>
        /// Handles the cell content click event of the DataGridView to open the donor details form with the selected donor information.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private async void dataGridView_DonorInformation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView_DonorInformation.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView_DonorInformation.Rows[e.RowIndex];
                DonorDetails donorDetails = new DonorDetails(_configuration);

                // Get the donor information from the DonorLogic
                Donor donor = await _donorLogic.GetDonorByCprNo(selectedRow.Cells["Column_CprNo"].Value.ToString());

                // Set the donor information in the DonorDetails form
                donorDetails.SetDonor(donor);  // Initialize currentDonor in DonorDetails
                donorDetails.UpdateAppointmentFields();
                donorDetails.StartPosition = FormStartPosition.CenterScreen; // Set the form's position to the center of the screen
                donorDetails.Show();
            }
        }
    }
}
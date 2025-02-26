﻿using DesktopApp.BusinessLogicLayer;
using DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp.GUI
{
    public partial class StartPage : Form
    {
        private readonly DonorLogic _donorLogic;

        public StartPage()
        {
            InitializeComponent();
            _donorLogic = new DonorLogic();

            // Set form to start maximized
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnLoadDonors_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch donors from the Business Logic Layer
                List<Donor> donors = _donorLogic.GetDonors();

                // Set the data source of the DataGridView
                dataGridViewDonors.DataSource = donors;
                // Set custom headers if necessary
                dataGridViewDonors.Columns["CprNo"].HeaderText = "CPR No";
                dataGridViewDonors.Columns["DonorFirstName"].HeaderText = "First Name";
                dataGridViewDonors.Columns["DonorLastName"].HeaderText = "Last Name";
                dataGridViewDonors.Columns["BloodType"].HeaderText = "Blood Type";

                // Optionally hide the donorId column, as it's only needed to fetch details
                dataGridViewDonors.Columns["donorId"].Visible = false;
                dataGridViewDonors.Columns["DonorPhoneNo"].Visible = false;
                dataGridViewDonors.Columns["DonorEmail"].Visible = false;
                dataGridViewDonors.Columns["DonorStreet"].Visible = false;
                dataGridViewDonors.Columns["City"].Visible = false;
                dataGridViewDonors.Columns["ZipCode"].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*if (e.RowIndex >= 0) // Ensure a valid row is clicked
            {
                string selectedDonorCprNo = dataGridViewDonors.Rows[e.RowIndex].Cells["CprNo"].Value?.ToString() ?? string.Empty;
                if (!string.IsNullOrEmpty(selectedDonorCprNo))
                {
                    // Fetch donor details using the donor logic
                    var selectedDonor = _donorLogic.GetDonorDetails(selectedDonorCprNo);

                    if (selectedDonor != null)
                    {
                        // Open the DonorDetailsForm and pass the donor data
                        DonorDetailsForm detailsForm = new DonorDetailsForm(_donorLogic, selectedDonorCprNo);
                        detailsForm.ShowDialog(); // Show the details form as a modal dialog
                    }
                    else
                    {
                        MessageBox.Show("Donor not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid CPR No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }
    }
}

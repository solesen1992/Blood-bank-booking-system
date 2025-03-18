namespace DesktopApp.GUI
{
    partial class FrontPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLoadDonors = new Button();
            dataGridViewDonors = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonors).BeginInit();
            SuspendLayout();
            // 
            // btnLoadDonors
            // 
            btnLoadDonors.Location = new Point(744, 12);
            btnLoadDonors.Name = "btnLoadDonors";
            btnLoadDonors.Size = new Size(216, 60);
            btnLoadDonors.TabIndex = 0;
            btnLoadDonors.Text = "Get donors";
            btnLoadDonors.UseVisualStyleBackColor = true;
            btnLoadDonors.Click += btnLoadDonors_Click;
            // 
            // dataGridViewDonors
            // 
            dataGridViewDonors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDonors.Location = new Point(12, 12);
            dataGridViewDonors.Name = "dataGridViewDonors";
            dataGridViewDonors.RowHeadersWidth = 62;
            dataGridViewDonors.Size = new Size(726, 507);
            dataGridViewDonors.TabIndex = 1;
            dataGridViewDonors.CellContentClick += dataGridViewDonors_CellContentClick;
            // 
            // FrontPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 526);
            Controls.Add(dataGridViewDonors);
            Controls.Add(btnLoadDonors);
            Name = "FrontPage";
            Text = "FrontPage";
            Load += FrontPage_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadDonors;
        private DataGridView dataGridViewDonors;
    }
}
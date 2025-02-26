namespace DesktopApp.GUI
{
    partial class StartPage
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
            dataGridViewDonors = new DataGridView();
            btnLoadDonors = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonors).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewDonors
            // 
            dataGridViewDonors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDonors.Location = new Point(2, 1);
            dataGridViewDonors.Name = "dataGridViewDonors";
            dataGridViewDonors.RowHeadersWidth = 62;
            dataGridViewDonors.Size = new Size(595, 447);
            dataGridViewDonors.TabIndex = 0;
            dataGridViewDonors.CellContentClick += dataGridViewDonors_CellContentClick;
            // 
            // btnLoadDonors
            // 
            btnLoadDonors.Location = new Point(603, 5);
            btnLoadDonors.Name = "btnLoadDonors";
            btnLoadDonors.Size = new Size(185, 55);
            btnLoadDonors.TabIndex = 1;
            btnLoadDonors.Text = "Get donors";
            btnLoadDonors.UseVisualStyleBackColor = true;
            btnLoadDonors.Click += btnLoadDonors_Click;
            // 
            // StartPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoadDonors);
            Controls.Add(dataGridViewDonors);
            Name = "StartPage";
            Text = "StartPage";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDonors).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewDonors;
        private Button btnLoadDonors;
    }
}
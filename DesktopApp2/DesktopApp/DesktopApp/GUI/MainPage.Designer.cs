namespace DesktopApp
{
    partial class MainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            dataGridView_DonorAppointments = new DataGridView();
            CprNo = new DataGridViewTextBoxColumn();
            FirstName = new DataGridViewTextBoxColumn();
            LastName = new DataGridViewTextBoxColumn();
            StartTime = new DataGridViewTextBoxColumn();
            EndTime = new DataGridViewTextBoxColumn();
            progressBar1 = new ProgressBar();
            textBox2 = new TextBox();
            label1 = new Label();
            dataGridView_DonorInformation = new DataGridView();
            Column_CprNo = new DataGridViewTextBoxColumn();
            test = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            SeRedigere = new DataGridViewButtonColumn();
            menuStrip = new MenuStrip();
            bloddonorInformationToolStripMenuItem = new ToolStripMenuItem();
            appointmentsToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DonorAppointments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DonorInformation).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(menuStrip);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(2399, 1060);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(dataGridView_DonorAppointments);
            groupBox1.Controls.Add(progressBar1);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(dataGridView_DonorInformation);
            groupBox1.Font = new Font("Century Gothic", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(33, 154);
            groupBox1.Margin = new Padding(4, 2, 4, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 2, 4, 2);
            groupBox1.Size = new Size(2334, 860);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Administrer bloddonorer";
            // 
            // dataGridView_DonorAppointments
            // 
            dataGridView_DonorAppointments.AllowUserToAddRows = false;
            dataGridView_DonorAppointments.AllowUserToDeleteRows = false;
            dataGridView_DonorAppointments.AllowUserToResizeColumns = false;
            dataGridView_DonorAppointments.AllowUserToResizeRows = false;
            dataGridView_DonorAppointments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView_DonorAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_DonorAppointments.BackgroundColor = Color.White;
            dataGridView_DonorAppointments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView_DonorAppointments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_DonorAppointments.Columns.AddRange(new DataGridViewColumn[] { CprNo, FirstName, LastName, StartTime, EndTime });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 10.125F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView_DonorAppointments.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView_DonorAppointments.Location = new Point(45, 222);
            dataGridView_DonorAppointments.Margin = new Padding(4, 2, 4, 2);
            dataGridView_DonorAppointments.Name = "dataGridView_DonorAppointments";
            dataGridView_DonorAppointments.RowHeadersWidth = 82;
            dataGridView_DonorAppointments.Size = new Size(2247, 602);
            dataGridView_DonorAppointments.TabIndex = 8;
            dataGridView_DonorAppointments.Visible = false;
            // 
            // CprNo
            // 
            CprNo.DataPropertyName = "CprNo";
            CprNo.HeaderText = "CPR-nummer";
            CprNo.MinimumWidth = 10;
            CprNo.Name = "CprNo";
            // 
            // FirstName
            // 
            FirstName.DataPropertyName = "DonorFirstName";
            FirstName.HeaderText = "Fornavn";
            FirstName.MinimumWidth = 10;
            FirstName.Name = "FirstName";
            // 
            // LastName
            // 
            LastName.DataPropertyName = "DonorLastName";
            LastName.HeaderText = "Efternavn";
            LastName.MinimumWidth = 10;
            LastName.Name = "LastName";
            // 
            // StartTime
            // 
            StartTime.DataPropertyName = "startTime";
            StartTime.HeaderText = "Tid - Start";
            StartTime.MinimumWidth = 10;
            StartTime.Name = "StartTime";
            StartTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // EndTime
            // 
            EndTime.DataPropertyName = "endTime";
            EndTime.HeaderText = "Tid - Slut";
            EndTime.MinimumWidth = 10;
            EndTime.Name = "EndTime";
            EndTime.SortMode = DataGridViewColumnSortMode.Programmatic;
            // 
            // progressBar1
            // 
            progressBar1.BackColor = Color.White;
            progressBar1.Location = new Point(45, 222);
            progressBar1.Margin = new Padding(4, 2, 4, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(82, 47);
            progressBar1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox2.BackColor = Color.White;
            textBox2.Location = new Point(2024, 145);
            textBox2.Margin = new Padding(4, 2, 4, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(266, 43);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(1946, 151);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 32);
            label1.TabIndex = 5;
            label1.Text = "Søg:";
            // 
            // dataGridView_DonorInformation
            // 
            dataGridView_DonorInformation.AllowUserToAddRows = false;
            dataGridView_DonorInformation.AllowUserToDeleteRows = false;
            dataGridView_DonorInformation.AllowUserToResizeColumns = false;
            dataGridView_DonorInformation.AllowUserToResizeRows = false;
            dataGridView_DonorInformation.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView_DonorInformation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_DonorInformation.BackgroundColor = Color.White;
            dataGridView_DonorInformation.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView_DonorInformation.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_DonorInformation.Columns.AddRange(new DataGridViewColumn[] { Column_CprNo, test, Column1, Column8, Column4, Column3, Column7, Column5, Column6, SeRedigere });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView_DonorInformation.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView_DonorInformation.Location = new Point(45, 222);
            dataGridView_DonorInformation.Margin = new Padding(4, 2, 4, 2);
            dataGridView_DonorInformation.Name = "dataGridView_DonorInformation";
            dataGridView_DonorInformation.RowHeadersWidth = 82;
            dataGridView_DonorInformation.Size = new Size(2247, 602);
            dataGridView_DonorInformation.TabIndex = 2;
            dataGridView_DonorInformation.Visible = false;
            dataGridView_DonorInformation.CellContentClick += dataGridView_DonorInformation_CellContentClick;
            // 
            // Column_CprNo
            // 
            Column_CprNo.DataPropertyName = "CprNo";
            Column_CprNo.HeaderText = "Cpr-nummer";
            Column_CprNo.MinimumWidth = 10;
            Column_CprNo.Name = "Column_CprNo";
            // 
            // test
            // 
            test.DataPropertyName = "DonorFirstName";
            test.HeaderText = "Fornavn";
            test.MinimumWidth = 10;
            test.Name = "test";
            // 
            // Column1
            // 
            Column1.DataPropertyName = "DonorLastName";
            Column1.HeaderText = "Efternavn";
            Column1.MinimumWidth = 10;
            Column1.Name = "Column1";
            // 
            // Column8
            // 
            Column8.DataPropertyName = "BloodType";
            Column8.HeaderText = "Blodtype";
            Column8.MinimumWidth = 10;
            Column8.Name = "Column8";
            // 
            // Column4
            // 
            Column4.DataPropertyName = "DonorPhoneNo";
            Column4.HeaderText = "Telefon";
            Column4.MinimumWidth = 10;
            Column4.Name = "Column4";
            // 
            // Column3
            // 
            Column3.DataPropertyName = "DonorEmail";
            Column3.HeaderText = "E-mail";
            Column3.MinimumWidth = 10;
            Column3.Name = "Column3";
            // 
            // Column7
            // 
            Column7.DataPropertyName = "DonorStreet";
            Column7.HeaderText = "Adresse";
            Column7.MinimumWidth = 10;
            Column7.Name = "Column7";
            // 
            // Column5
            // 
            Column5.DataPropertyName = "City";
            Column5.HeaderText = "By";
            Column5.MinimumWidth = 10;
            Column5.Name = "Column5";
            // 
            // Column6
            // 
            Column6.DataPropertyName = "ZipCode";
            Column6.HeaderText = "Postnummer";
            Column6.MinimumWidth = 10;
            Column6.Name = "Column6";
            // 
            // Se
            // 
            SeRedigere.HeaderText = "Se/Redigere";
            SeRedigere.MinimumWidth = 10;
            SeRedigere.Name = "SeRedigere";
            // 
            // menuStrip
            // 
            menuStrip.AutoSize = false;
            menuStrip.BackColor = Color.Maroon;
            menuStrip.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menuStrip.ImageScalingSize = new Size(32, 32);
            menuStrip.Items.AddRange(new ToolStripItem[] { bloddonorInformationToolStripMenuItem, appointmentsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.MdiWindowListItem = appointmentsToolStripMenuItem;
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(2399, 81);
            menuStrip.TabIndex = 6;
            menuStrip.Text = "menuStrip";
            // 
            // bloddonorInformationToolStripMenuItem
            // 
            bloddonorInformationToolStripMenuItem.ForeColor = Color.White;
            bloddonorInformationToolStripMenuItem.Name = "bloddonorInformationToolStripMenuItem";
            bloddonorInformationToolStripMenuItem.Size = new Size(158, 77);
            bloddonorInformationToolStripMenuItem.Text = "Donorer";
            bloddonorInformationToolStripMenuItem.Click += bloddonorInformationToolStripMenuItem_Click;
            // 
            // appointmentsToolStripMenuItem
            // 
            appointmentsToolStripMenuItem.ForeColor = Color.White;
            appointmentsToolStripMenuItem.Name = "appointmentsToolStripMenuItem";
            appointmentsToolStripMenuItem.Size = new Size(109, 77);
            appointmentsToolStripMenuItem.Text = "Tider";
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(2399, 1060);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            IsMdiContainer = true;
            Margin = new Padding(6, 4, 6, 4);
            Name = "MainPage";
            Text = "BlodBank";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DonorAppointments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_DonorInformation).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private GroupBox groupBox1;
        private DataGridView dataGridView_DonorInformation;
        private TextBox textBox2;
        private Label label1;
        private MenuStrip menuStrip;
        private ToolStripMenuItem bloddonorInformationToolStripMenuItem;
        private ProgressBar progressBar1;
        private ToolStripMenuItem appointmentsToolStripMenuItem;
        private DataGridViewTextBoxColumn CprNo;
        private DataGridViewTextBoxColumn FirstName;
        private DataGridViewTextBoxColumn LastName;
        private DataGridViewTextBoxColumn StartTime;
        private DataGridViewTextBoxColumn EndTime;
        private DataGridViewTextBoxColumn Column_CprNo;
        private DataGridViewTextBoxColumn test;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewButtonColumn SeRedigere;
    }
}

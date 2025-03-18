namespace DesktopApp.GUI
{
    partial class DonorDetails
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
            groupBox_Donor = new GroupBox();
            BloodtypeScroll = new ComboBox();
            button_Edit = new Button();
            textBox_Adresse = new TextBox();
            textBox_Email = new TextBox();
            textBox_Firstname = new TextBox();
            textBox_CprNo = new TextBox();
            textBox_ZipCode = new TextBox();
            textBox_City = new TextBox();
            textBox_PhoneNo = new TextBox();
            textBox_Lastname = new TextBox();
            label_ZipCode = new Label();
            label_City = new Label();
            label_Firstname = new Label();
            label_Email = new Label();
            label_Bloodtype = new Label();
            label_Lastname = new Label();
            label_PhoneNo = new Label();
            label_Adresse = new Label();
            label_CprNo = new Label();
            groupBox_Appointment = new GroupBox();
            button_Delete = new Button();
            label_NoTime = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox_End = new TextBox();
            textBox_Start = new TextBox();
            button_Close = new Button();
            Save = new Button();
            groupBox_Donor.SuspendLayout();
            groupBox_Appointment.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_Donor
            // 
            groupBox_Donor.Controls.Add(BloodtypeScroll);
            groupBox_Donor.Controls.Add(button_Edit);
            groupBox_Donor.Controls.Add(textBox_Adresse);
            groupBox_Donor.Controls.Add(textBox_Email);
            groupBox_Donor.Controls.Add(textBox_Firstname);
            groupBox_Donor.Controls.Add(textBox_CprNo);
            groupBox_Donor.Controls.Add(textBox_ZipCode);
            groupBox_Donor.Controls.Add(textBox_City);
            groupBox_Donor.Controls.Add(textBox_PhoneNo);
            groupBox_Donor.Controls.Add(textBox_Lastname);
            groupBox_Donor.Controls.Add(label_ZipCode);
            groupBox_Donor.Controls.Add(label_City);
            groupBox_Donor.Controls.Add(label_Firstname);
            groupBox_Donor.Controls.Add(label_Email);
            groupBox_Donor.Controls.Add(label_Bloodtype);
            groupBox_Donor.Controls.Add(label_Lastname);
            groupBox_Donor.Controls.Add(label_PhoneNo);
            groupBox_Donor.Controls.Add(label_Adresse);
            groupBox_Donor.Controls.Add(label_CprNo);
            groupBox_Donor.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox_Donor.Location = new Point(24, 19);
            groupBox_Donor.Margin = new Padding(1);
            groupBox_Donor.Name = "groupBox_Donor";
            groupBox_Donor.Padding = new Padding(1);
            groupBox_Donor.Size = new Size(469, 281);
            groupBox_Donor.TabIndex = 0;
            groupBox_Donor.TabStop = false;
            groupBox_Donor.Text = "Donor";
            // 
            // BloodtypeScroll
            // 
            BloodtypeScroll.FormattingEnabled = true;
            BloodtypeScroll.Location = new Point(74, 148);
            BloodtypeScroll.Margin = new Padding(2);
            BloodtypeScroll.Name = "BloodtypeScroll";
            BloodtypeScroll.Size = new Size(166, 24);
            BloodtypeScroll.TabIndex = 19;
            // 
            // button_Edit
            // 
            button_Edit.Location = new Point(374, 254);
            button_Edit.Margin = new Padding(1);
            button_Edit.Name = "button_Edit";
            button_Edit.Size = new Size(85, 19);
            button_Edit.TabIndex = 18;
            button_Edit.Text = "Rediger";
            button_Edit.UseVisualStyleBackColor = true;
            button_Edit.Click += button_Edit_Click;
            // 
            // textBox_Adresse
            // 
            textBox_Adresse.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Adresse.Location = new Point(326, 34);
            textBox_Adresse.Margin = new Padding(1);
            textBox_Adresse.Name = "textBox_Adresse";
            textBox_Adresse.ReadOnly = true;
            textBox_Adresse.Size = new Size(135, 22);
            textBox_Adresse.TabIndex = 17;
            // 
            // textBox_Email
            // 
            textBox_Email.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Email.Location = new Point(74, 226);
            textBox_Email.Margin = new Padding(1);
            textBox_Email.Name = "textBox_Email";
            textBox_Email.ReadOnly = true;
            textBox_Email.Size = new Size(166, 22);
            textBox_Email.TabIndex = 16;
            // 
            // textBox_Firstname
            // 
            textBox_Firstname.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Firstname.Location = new Point(74, 68);
            textBox_Firstname.Margin = new Padding(1);
            textBox_Firstname.Name = "textBox_Firstname";
            textBox_Firstname.ReadOnly = true;
            textBox_Firstname.Size = new Size(166, 22);
            textBox_Firstname.TabIndex = 15;
            // 
            // textBox_CprNo
            // 
            textBox_CprNo.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_CprNo.Location = new Point(74, 30);
            textBox_CprNo.Margin = new Padding(1);
            textBox_CprNo.Name = "textBox_CprNo";
            textBox_CprNo.ReadOnly = true;
            textBox_CprNo.Size = new Size(166, 22);
            textBox_CprNo.TabIndex = 14;
            // 
            // textBox_ZipCode
            // 
            textBox_ZipCode.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_ZipCode.Location = new Point(326, 107);
            textBox_ZipCode.Margin = new Padding(1);
            textBox_ZipCode.Name = "textBox_ZipCode";
            textBox_ZipCode.ReadOnly = true;
            textBox_ZipCode.Size = new Size(135, 22);
            textBox_ZipCode.TabIndex = 12;
            // 
            // textBox_City
            // 
            textBox_City.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_City.Location = new Point(326, 70);
            textBox_City.Margin = new Padding(1);
            textBox_City.Name = "textBox_City";
            textBox_City.ReadOnly = true;
            textBox_City.Size = new Size(135, 22);
            textBox_City.TabIndex = 11;
            // 
            // textBox_PhoneNo
            // 
            textBox_PhoneNo.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_PhoneNo.Location = new Point(74, 190);
            textBox_PhoneNo.Margin = new Padding(1);
            textBox_PhoneNo.Name = "textBox_PhoneNo";
            textBox_PhoneNo.ReadOnly = true;
            textBox_PhoneNo.Size = new Size(166, 22);
            textBox_PhoneNo.TabIndex = 10;
            // 
            // textBox_Lastname
            // 
            textBox_Lastname.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Lastname.Location = new Point(74, 109);
            textBox_Lastname.Margin = new Padding(1);
            textBox_Lastname.Name = "textBox_Lastname";
            textBox_Lastname.ReadOnly = true;
            textBox_Lastname.Size = new Size(166, 22);
            textBox_Lastname.TabIndex = 9;
            // 
            // label_ZipCode
            // 
            label_ZipCode.AutoSize = true;
            label_ZipCode.Location = new Point(263, 111);
            label_ZipCode.Margin = new Padding(1, 0, 1, 0);
            label_ZipCode.Name = "label_ZipCode";
            label_ZipCode.Size = new Size(44, 16);
            label_ZipCode.TabIndex = 8;
            label_ZipCode.Text = "Postnr:";
            // 
            // label_City
            // 
            label_City.AutoSize = true;
            label_City.Location = new Point(263, 70);
            label_City.Margin = new Padding(1, 0, 1, 0);
            label_City.Name = "label_City";
            label_City.Size = new Size(24, 16);
            label_City.TabIndex = 7;
            label_City.Text = "By:";
            // 
            // label_Firstname
            // 
            label_Firstname.AutoSize = true;
            label_Firstname.Location = new Point(4, 68);
            label_Firstname.Margin = new Padding(1, 0, 1, 0);
            label_Firstname.Name = "label_Firstname";
            label_Firstname.Size = new Size(57, 16);
            label_Firstname.TabIndex = 6;
            label_Firstname.Text = "Fornavn:";
            // 
            // label_Email
            // 
            label_Email.AutoSize = true;
            label_Email.Location = new Point(4, 228);
            label_Email.Margin = new Padding(1, 0, 1, 0);
            label_Email.Name = "label_Email";
            label_Email.Size = new Size(46, 16);
            label_Email.TabIndex = 5;
            label_Email.Text = "E-mail:";
            // 
            // label_Bloodtype
            // 
            label_Bloodtype.AutoSize = true;
            label_Bloodtype.Location = new Point(4, 150);
            label_Bloodtype.Margin = new Padding(1, 0, 1, 0);
            label_Bloodtype.Name = "label_Bloodtype";
            label_Bloodtype.Size = new Size(62, 16);
            label_Bloodtype.TabIndex = 4;
            label_Bloodtype.Text = "Blodtype:";
            // 
            // label_Lastname
            // 
            label_Lastname.AutoSize = true;
            label_Lastname.Location = new Point(4, 111);
            label_Lastname.Margin = new Padding(1, 0, 1, 0);
            label_Lastname.Name = "label_Lastname";
            label_Lastname.Size = new Size(63, 16);
            label_Lastname.TabIndex = 3;
            label_Lastname.Text = "Efternavn:";
            // 
            // label_PhoneNo
            // 
            label_PhoneNo.AutoSize = true;
            label_PhoneNo.Location = new Point(4, 192);
            label_PhoneNo.Margin = new Padding(1, 0, 1, 0);
            label_PhoneNo.Name = "label_PhoneNo";
            label_PhoneNo.Size = new Size(53, 16);
            label_PhoneNo.TabIndex = 2;
            label_PhoneNo.Text = "Telefon:";
            // 
            // label_Adresse
            // 
            label_Adresse.AutoSize = true;
            label_Adresse.Location = new Point(263, 34);
            label_Adresse.Margin = new Padding(1, 0, 1, 0);
            label_Adresse.Name = "label_Adresse";
            label_Adresse.Size = new Size(57, 16);
            label_Adresse.TabIndex = 1;
            label_Adresse.Text = "Adresse:";
            // 
            // label_CprNo
            // 
            label_CprNo.AutoSize = true;
            label_CprNo.Location = new Point(4, 32);
            label_CprNo.Margin = new Padding(1, 0, 1, 0);
            label_CprNo.Name = "label_CprNo";
            label_CprNo.Size = new Size(47, 16);
            label_CprNo.TabIndex = 0;
            label_CprNo.Text = "Cpr-nr:";
            // 
            // groupBox_Appointment
            // 
            groupBox_Appointment.Controls.Add(button_Delete);
            groupBox_Appointment.Controls.Add(label_NoTime);
            groupBox_Appointment.Controls.Add(label2);
            groupBox_Appointment.Controls.Add(label1);
            groupBox_Appointment.Controls.Add(textBox_End);
            groupBox_Appointment.Controls.Add(textBox_Start);
            groupBox_Appointment.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox_Appointment.Location = new Point(24, 334);
            groupBox_Appointment.Margin = new Padding(1);
            groupBox_Appointment.Name = "groupBox_Appointment";
            groupBox_Appointment.Padding = new Padding(1);
            groupBox_Appointment.Size = new Size(469, 118);
            groupBox_Appointment.TabIndex = 1;
            groupBox_Appointment.TabStop = false;
            groupBox_Appointment.Text = "Kommende tid";
            // 
            // button_Delete
            // 
            button_Delete.Location = new Point(384, 91);
            button_Delete.Name = "button_Delete";
            button_Delete.Size = new Size(75, 23);
            button_Delete.TabIndex = 5;
            button_Delete.Text = "Aflys tid";
            button_Delete.UseVisualStyleBackColor = true;
            button_Delete.Click += button_Delete_Click;
            // 
            // label_NoTime
            // 
            label_NoTime.AutoSize = true;
            label_NoTime.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_NoTime.Location = new Point(10, 23);
            label_NoTime.Margin = new Padding(1, 0, 1, 0);
            label_NoTime.Name = "label_NoTime";
            label_NoTime.Size = new Size(0, 17);
            label_NoTime.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(243, 52);
            label2.Margin = new Padding(1, 0, 1, 0);
            label2.Name = "label2";
            label2.Size = new Size(57, 16);
            label2.TabIndex = 3;
            label2.Text = "Tid - Slut:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 49);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(62, 16);
            label1.TabIndex = 2;
            label1.Text = "Tid - Start:";
            // 
            // textBox_End
            // 
            textBox_End.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_End.Location = new Point(307, 49);
            textBox_End.Margin = new Padding(1);
            textBox_End.Name = "textBox_End";
            textBox_End.ReadOnly = true;
            textBox_End.Size = new Size(154, 22);
            textBox_End.TabIndex = 1;
            // 
            // textBox_Start
            // 
            textBox_Start.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Start.Location = new Point(80, 49);
            textBox_Start.Margin = new Padding(1);
            textBox_Start.Name = "textBox_Start";
            textBox_Start.ReadOnly = true;
            textBox_Start.Size = new Size(154, 22);
            textBox_Start.TabIndex = 0;
            // 
            // button_Close
            // 
            button_Close.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_Close.Location = new Point(408, 476);
            button_Close.Margin = new Padding(1);
            button_Close.Name = "button_Close";
            button_Close.Size = new Size(94, 20);
            button_Close.TabIndex = 2;
            button_Close.Text = "Luk";
            button_Close.UseVisualStyleBackColor = true;
            button_Close.Click += button_Close_Click;
            // 
            // Save
            // 
            Save.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Save.Location = new Point(305, 476);
            Save.Margin = new Padding(1);
            Save.Name = "Save";
            Save.Size = new Size(94, 20);
            Save.TabIndex = 3;
            Save.Text = "Gem";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // DonorDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(526, 528);
            Controls.Add(Save);
            Controls.Add(button_Close);
            Controls.Add(groupBox_Appointment);
            Controls.Add(groupBox_Donor);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "DonorDetails";
            Text = "Donor information";
            TopMost = true;
            groupBox_Donor.ResumeLayout(false);
            groupBox_Donor.PerformLayout();
            groupBox_Appointment.ResumeLayout(false);
            groupBox_Appointment.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox_Donor;
        private GroupBox groupBox_Appointment;
        private Label label_Firstname;
        private Label label_Email;
        private Label label_Bloodtype;
        private Label label_Lastname;
        private Label label_PhoneNo;
        private Label label_Adresse;
        private Label label_CprNo;
        public TextBox textBox_Adresse;
        public TextBox textBox_Email;
        public TextBox textBox_Firstname;
        public TextBox textBox_CprNo;
        public TextBox textBox_ZipCode;
        public TextBox textBox_City;
        public TextBox textBox_PhoneNo;
        public TextBox textBox_Lastname;
        private Label label_ZipCode;
        private Label label_City;
        public TextBox textBox_End;
        public TextBox textBox_Start;
        private Label label2;
        private Label label1;
        private Button button_Close;
        private Button button_Edit;
        public Label label_NoTime;
        private Button Save;
        private ComboBox BloodtypeScroll;
        private Button button_Delete;
    }
}
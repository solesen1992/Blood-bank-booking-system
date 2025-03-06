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
            donorBox = new GroupBox();
            label_ZipCode = new Label();
            label_City = new Label();
            label_Adresse = new Label();
            label_Email = new Label();
            label_PhoneNo = new Label();
            label_Bloodtype = new Label();
            label_Lastname = new Label();
            button_Edit = new Button();
            textBox_ZipCode = new TextBox();
            textBox_City = new TextBox();
            textBox_Adresse = new TextBox();
            textBox_Email = new TextBox();
            textBox_PhoneNo = new TextBox();
            BloodtypeScroll = new ComboBox();
            textBox_Lastname = new TextBox();
            textBox_Firstname = new TextBox();
            label_Firstname = new Label();
            textBox_CprNo = new TextBox();
            cprLabel = new Label();
            groupBox_Appointment = new GroupBox();
            button_Delete = new Button();
            textBox_End = new TextBox();
            label_EndTime = new Label();
            textBox_Start = new TextBox();
            label_TimeStart = new Label();
            btn_Save = new Button();
            btn_Close = new Button();
            donorBox.SuspendLayout();
            groupBox_Appointment.SuspendLayout();
            SuspendLayout();
            // 
            // donorBox
            // 
            donorBox.Controls.Add(label_ZipCode);
            donorBox.Controls.Add(label_City);
            donorBox.Controls.Add(label_Adresse);
            donorBox.Controls.Add(label_Email);
            donorBox.Controls.Add(label_PhoneNo);
            donorBox.Controls.Add(label_Bloodtype);
            donorBox.Controls.Add(label_Lastname);
            donorBox.Controls.Add(button_Edit);
            donorBox.Controls.Add(textBox_ZipCode);
            donorBox.Controls.Add(textBox_City);
            donorBox.Controls.Add(textBox_Adresse);
            donorBox.Controls.Add(textBox_Email);
            donorBox.Controls.Add(textBox_PhoneNo);
            donorBox.Controls.Add(BloodtypeScroll);
            donorBox.Controls.Add(textBox_Lastname);
            donorBox.Controls.Add(textBox_Firstname);
            donorBox.Controls.Add(label_Firstname);
            donorBox.Controls.Add(textBox_CprNo);
            donorBox.Controls.Add(cprLabel);
            donorBox.Location = new Point(12, 12);
            donorBox.Name = "donorBox";
            donorBox.Size = new Size(684, 427);
            donorBox.TabIndex = 0;
            donorBox.TabStop = false;
            donorBox.Text = "Donor";
            donorBox.Enter += groupBox1_Enter;
            // 
            // label_ZipCode
            // 
            label_ZipCode.AutoSize = true;
            label_ZipCode.Location = new Point(357, 143);
            label_ZipCode.Name = "label_ZipCode";
            label_ZipCode.Size = new Size(113, 25);
            label_ZipCode.TabIndex = 18;
            label_ZipCode.Text = "Postnummer";
            // 
            // label_City
            // 
            label_City.AutoSize = true;
            label_City.Location = new Point(357, 89);
            label_City.Name = "label_City";
            label_City.Size = new Size(31, 25);
            label_City.TabIndex = 17;
            label_City.Text = "By";
            // 
            // label_Adresse
            // 
            label_Adresse.AutoSize = true;
            label_Adresse.Location = new Point(357, 36);
            label_Adresse.Name = "label_Adresse";
            label_Adresse.Size = new Size(75, 25);
            label_Adresse.TabIndex = 16;
            label_Adresse.Text = "Adresse";
            // 
            // label_Email
            // 
            label_Email.AutoSize = true;
            label_Email.Location = new Point(11, 312);
            label_Email.Name = "label_Email";
            label_Email.Size = new Size(61, 25);
            label_Email.TabIndex = 15;
            label_Email.Text = "E-mail";
            // 
            // label_PhoneNo
            // 
            label_PhoneNo.AutoSize = true;
            label_PhoneNo.Location = new Point(11, 253);
            label_PhoneNo.Name = "label_PhoneNo";
            label_PhoneNo.Size = new Size(68, 25);
            label_PhoneNo.TabIndex = 14;
            label_PhoneNo.Text = "Telefon";
            // 
            // label_Bloodtype
            // 
            label_Bloodtype.AutoSize = true;
            label_Bloodtype.Location = new Point(11, 197);
            label_Bloodtype.Name = "label_Bloodtype";
            label_Bloodtype.Size = new Size(83, 25);
            label_Bloodtype.TabIndex = 13;
            label_Bloodtype.Text = "Blodtype";
            // 
            // label_Lastname
            // 
            label_Lastname.AutoSize = true;
            label_Lastname.Location = new Point(11, 140);
            label_Lastname.Name = "label_Lastname";
            label_Lastname.Size = new Size(86, 25);
            label_Lastname.TabIndex = 12;
            label_Lastname.Text = "Efternavn";
            // 
            // button_Edit
            // 
            button_Edit.Location = new Point(477, 370);
            button_Edit.Name = "button_Edit";
            button_Edit.Size = new Size(179, 34);
            button_Edit.TabIndex = 11;
            button_Edit.Text = "Rediger";
            button_Edit.UseVisualStyleBackColor = true;
            button_Edit.Click += button_Edit_Click;
            // 
            // textBox_ZipCode
            // 
            textBox_ZipCode.Location = new Point(477, 140);
            textBox_ZipCode.Name = "textBox_ZipCode";
            textBox_ZipCode.Size = new Size(180, 31);
            textBox_ZipCode.TabIndex = 10;
            // 
            // textBox_City
            // 
            textBox_City.Location = new Point(477, 89);
            textBox_City.Name = "textBox_City";
            textBox_City.Size = new Size(181, 31);
            textBox_City.TabIndex = 9;
            // 
            // textBox_Adresse
            // 
            textBox_Adresse.Location = new Point(478, 33);
            textBox_Adresse.Name = "textBox_Adresse";
            textBox_Adresse.Size = new Size(180, 31);
            textBox_Adresse.TabIndex = 8;
            // 
            // textBox_Email
            // 
            textBox_Email.Location = new Point(106, 309);
            textBox_Email.Name = "textBox_Email";
            textBox_Email.Size = new Size(180, 31);
            textBox_Email.TabIndex = 7;
            // 
            // textBox_PhoneNo
            // 
            textBox_PhoneNo.Location = new Point(104, 253);
            textBox_PhoneNo.Name = "textBox_PhoneNo";
            textBox_PhoneNo.Size = new Size(182, 31);
            textBox_PhoneNo.TabIndex = 6;
            // 
            // BloodtypeScroll
            // 
            BloodtypeScroll.FormattingEnabled = true;
            BloodtypeScroll.Location = new Point(105, 194);
            BloodtypeScroll.Name = "BloodtypeScroll";
            BloodtypeScroll.Size = new Size(181, 33);
            BloodtypeScroll.TabIndex = 5;
            // 
            // textBox_Lastname
            // 
            textBox_Lastname.Location = new Point(106, 140);
            textBox_Lastname.Name = "textBox_Lastname";
            textBox_Lastname.Size = new Size(180, 31);
            textBox_Lastname.TabIndex = 4;
            // 
            // textBox_Firstname
            // 
            textBox_Firstname.Location = new Point(106, 87);
            textBox_Firstname.Name = "textBox_Firstname";
            textBox_Firstname.Size = new Size(180, 31);
            textBox_Firstname.TabIndex = 3;
            // 
            // label_Firstname
            // 
            label_Firstname.AutoSize = true;
            label_Firstname.Location = new Point(11, 87);
            label_Firstname.Name = "label_Firstname";
            label_Firstname.Size = new Size(76, 25);
            label_Firstname.TabIndex = 2;
            label_Firstname.Text = "Fornavn";
            label_Firstname.Click += label1_Click;
            // 
            // textBox_CprNo
            // 
            textBox_CprNo.Location = new Point(105, 36);
            textBox_CprNo.Name = "textBox_CprNo";
            textBox_CprNo.Size = new Size(181, 31);
            textBox_CprNo.TabIndex = 1;
            // 
            // cprLabel
            // 
            cprLabel.AutoSize = true;
            cprLabel.Location = new Point(11, 39);
            cprLabel.Name = "cprLabel";
            cprLabel.Size = new Size(71, 25);
            cprLabel.TabIndex = 0;
            cprLabel.Text = "CPR-nr.";
            // 
            // groupBox_Appointment
            // 
            groupBox_Appointment.Controls.Add(button_Delete);
            groupBox_Appointment.Controls.Add(textBox_End);
            groupBox_Appointment.Controls.Add(label_EndTime);
            groupBox_Appointment.Controls.Add(textBox_Start);
            groupBox_Appointment.Controls.Add(label_TimeStart);
            groupBox_Appointment.Location = new Point(12, 445);
            groupBox_Appointment.Name = "groupBox_Appointment";
            groupBox_Appointment.Size = new Size(684, 216);
            groupBox_Appointment.TabIndex = 1;
            groupBox_Appointment.TabStop = false;
            groupBox_Appointment.Text = "Kommende tid";
            // 
            // button_Delete
            // 
            button_Delete.Location = new Point(477, 132);
            button_Delete.Name = "button_Delete";
            button_Delete.Size = new Size(178, 34);
            button_Delete.TabIndex = 4;
            button_Delete.Text = "Aflys tid";
            button_Delete.UseVisualStyleBackColor = true;
            button_Delete.Click += button_Delete_Click;
            // 
            // textBox_End
            // 
            textBox_End.Location = new Point(477, 66);
            textBox_End.Name = "textBox_End";
            textBox_End.Size = new Size(178, 31);
            textBox_End.TabIndex = 3;
            // 
            // label_EndTime
            // 
            label_EndTime.AutoSize = true;
            label_EndTime.Location = new Point(357, 72);
            label_EndTime.Name = "label_EndTime";
            label_EndTime.Size = new Size(87, 25);
            label_EndTime.TabIndex = 2;
            label_EndTime.Text = "Tid - Slut:";
            // 
            // textBox_Start
            // 
            textBox_Start.Location = new Point(110, 66);
            textBox_Start.Name = "textBox_Start";
            textBox_Start.Size = new Size(176, 31);
            textBox_Start.TabIndex = 1;
            // 
            // label_TimeStart
            // 
            label_TimeStart.AutoSize = true;
            label_TimeStart.Location = new Point(11, 66);
            label_TimeStart.Name = "label_TimeStart";
            label_TimeStart.Size = new Size(93, 25);
            label_TimeStart.TabIndex = 0;
            label_TimeStart.Text = "Tid - Start:";
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(308, 688);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(177, 34);
            btn_Save.TabIndex = 2;
            btn_Save.Text = "Gem";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_Close
            // 
            btn_Close.Location = new Point(491, 688);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(177, 34);
            btn_Close.TabIndex = 3;
            btn_Close.Text = "Luk";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += btn_Close_Click;
            // 
            // DonorDetails
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 749);
            Controls.Add(btn_Close);
            Controls.Add(btn_Save);
            Controls.Add(groupBox_Appointment);
            Controls.Add(donorBox);
            Name = "DonorDetails";
            Text = "DonorDetails";
            donorBox.ResumeLayout(false);
            donorBox.PerformLayout();
            groupBox_Appointment.ResumeLayout(false);
            groupBox_Appointment.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox donorBox;
        private TextBox textBox_CprNo;
        private Label cprLabel;
        private Label label_Firstname;
        private TextBox textBox_ZipCode;
        private TextBox textBox_City;
        private TextBox textBox_Adresse;
        private TextBox textBox_Email;
        private TextBox textBox_PhoneNo;
        private ComboBox BloodtypeScroll;
        private TextBox textBox_Lastname;
        private TextBox textBox_Firstname;
        private Label label_ZipCode;
        private Label label_City;
        private Label label_Adresse;
        private Label label_Email;
        private Label label_PhoneNo;
        private Label label_Bloodtype;
        private Label label_Lastname;
        private Button button_Edit;
        private GroupBox groupBox_Appointment;
        private Button button_Delete;
        private TextBox textBox_End;
        private Label label_EndTime;
        private TextBox textBox_Start;
        private Label label_TimeStart;
        private Button btn_Save;
        private Button btn_Close;
    }
}
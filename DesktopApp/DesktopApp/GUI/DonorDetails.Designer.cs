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
            cprLabel = new Label();
            textBox1 = new TextBox();
            donorBox.SuspendLayout();
            SuspendLayout();
            // 
            // donorBox
            // 
            donorBox.Controls.Add(textBox1);
            donorBox.Controls.Add(cprLabel);
            donorBox.Location = new Point(25, 31);
            donorBox.Name = "donorBox";
            donorBox.Size = new Size(648, 435);
            donorBox.TabIndex = 0;
            donorBox.TabStop = false;
            donorBox.Text = "Donor";
            donorBox.Enter += groupBox1_Enter;
            // 
            // cprLabel
            // 
            cprLabel.AutoSize = true;
            cprLabel.Location = new Point(19, 39);
            cprLabel.Name = "cprLabel";
            cprLabel.Size = new Size(71, 25);
            cprLabel.TabIndex = 0;
            cprLabel.Text = "CPR-nr.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(105, 36);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(229, 31);
            textBox1.TabIndex = 1;
            // 
            // DonorDetails
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 804);
            Controls.Add(donorBox);
            Name = "DonorDetails";
            Text = "DonorDetails";
            donorBox.ResumeLayout(false);
            donorBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox donorBox;
        private TextBox textBox1;
        private Label cprLabel;
    }
}
namespace Driver_Licence_Project
{
    partial class frmShowInternationalLicenseDetails
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
            this.ctrlInternationalDriverLicense1 = new Driver_Licence_Project.ctrlInternationalDriverLicense();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlInternationalDriverLicense1
            // 
            this.ctrlInternationalDriverLicense1.Location = new System.Drawing.Point(12, 103);
            this.ctrlInternationalDriverLicense1.Name = "ctrlInternationalDriverLicense1";
            this.ctrlInternationalDriverLicense1.Size = new System.Drawing.Size(851, 341);
            this.ctrlInternationalDriverLicense1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(172, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Driver International License Info";
            // 
            // frmShowInternationalLicenseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 454);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlInternationalDriverLicense1);
            this.Name = "frmShowInternationalLicenseDetails";
            this.Text = "frmShowInternationalLicenseDetails";
            this.Load += new System.EventHandler(this.frmShowInternationalLicenseDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlInternationalDriverLicense ctrlInternationalDriverLicense1;
        private System.Windows.Forms.Label label1;
    }
}
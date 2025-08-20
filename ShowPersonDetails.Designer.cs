namespace Driver_Licence_Project
{
    partial class frmShowPersonDetails
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
            this.label2 = new System.Windows.Forms.Label();
            this.ucPersonInformationCard1 = new Driver_Licence_Project.ucPersonInformationCard();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.ForestGreen;
            this.label2.Location = new System.Drawing.Point(308, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 38);
            this.label2.TabIndex = 6;
            this.label2.Text = "Show Details";
            // 
            // ucPersonInformationCard1
            // 
            this.ucPersonInformationCard1.AddressVal = "[?????]";
            this.ucPersonInformationCard1.CountryIDVal = "[?????]";
            this.ucPersonInformationCard1.DateTimeVal = "[?????]";
            this.ucPersonInformationCard1.EmailVal = "[?????]";
            this.ucPersonInformationCard1.GendorVal = "[?????]";
            this.ucPersonInformationCard1.Location = new System.Drawing.Point(35, 54);
            this.ucPersonInformationCard1.Name = "ucPersonInformationCard1";
            this.ucPersonInformationCard1.NameVal = "[?????]";
            this.ucPersonInformationCard1.NationalNoVal = "[?????]";
            this.ucPersonInformationCard1.PhoneVal = "[?????]";
            this.ucPersonInformationCard1.Size = new System.Drawing.Size(803, 408);
            this.ucPersonInformationCard1.TabIndex = 7;
            this.ucPersonInformationCard1.Load += new System.EventHandler(this.ucPersonInformationCard1_Load);
            // 
            // frmShowPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 474);
            this.Controls.Add(this.ucPersonInformationCard1);
            this.Controls.Add(this.label2);
            this.Name = "frmShowPersonDetails";
            this.Text = "ShowPersonDetails";
            this.Load += new System.EventHandler(this.frmShowPersonDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private ucPersonInformationCard ucPersonInformationCard1;
    }
}
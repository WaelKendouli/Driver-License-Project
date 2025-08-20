namespace Driver_Licence_Project
{
    partial class frmDriverCard
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
            this.driversInfo1 = new Driver_Licence_Project.DriversInfo();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // driversInfo1
            // 
            this.driversInfo1.ApplicationID = 0;
            this.driversInfo1.Location = new System.Drawing.Point(29, 90);
            this.driversInfo1.Name = "driversInfo1";
            this.driversInfo1.Size = new System.Drawing.Size(929, 511);
            this.driversInfo1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(333, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Driver License Info";
            // 
            // frmDriverCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 613);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driversInfo1);
            this.Name = "frmDriverCard";
            this.Text = "DriverCard";
            this.Load += new System.EventHandler(this.frmDriverCard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DriversInfo driversInfo1;
        private System.Windows.Forms.Label label1;
    }
}
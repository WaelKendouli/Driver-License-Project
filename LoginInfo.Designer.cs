namespace Driver_Licence_Project
{
    partial class frmLoginInfo
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
            this.ctrlPasswordChange1 = new Driver_Licence_Project.ctrlPasswordChange();
            this.SuspendLayout();
            // 
            // ctrlPasswordChange1
            // 
            this.ctrlPasswordChange1.Location = new System.Drawing.Point(13, 2);
            this.ctrlPasswordChange1.Name = "ctrlPasswordChange1";
            this.ctrlPasswordChange1.Size = new System.Drawing.Size(998, 566);
            this.ctrlPasswordChange1.TabIndex = 0;
            this.ctrlPasswordChange1.Load += new System.EventHandler(this.ctrlPasswordChange1_Load);
            // 
            // frmLoginInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 580);
            this.Controls.Add(this.ctrlPasswordChange1);
            this.Name = "frmLoginInfo";
            this.Text = "LoginInfo";
            this.Load += new System.EventHandler(this.frmLoginInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPasswordChange ctrlPasswordChange1;
    }
}
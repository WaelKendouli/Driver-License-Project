namespace Driver_Licence_Project
{
    partial class frmAddEdit
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
            this.button1 = new System.Windows.Forms.Button();
            this.lTitle = new System.Windows.Forms.Label();
            this.personInfo1 = new Driver_Licence_Project.PersonInfo();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(964, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lTitle.Location = new System.Drawing.Point(430, 56);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(23, 36);
            this.lTitle.TabIndex = 3;
            this.lTitle.Text = ".";
            // 
            // personInfo1
            // 
            this.personInfo1.AutoSize = true;
            this.personInfo1.CountryIDVal = 2;
            this.personInfo1.DateTimeVal = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
            this.personInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personInfo1.GendorVal = 0;
            this.personInfo1.ImagePathVal = null;
            this.personInfo1.Location = new System.Drawing.Point(21, 84);
            this.personInfo1.Name = "personInfo1";
            this.personInfo1.PersonIDVal = -1;
            this.personInfo1.rtbAddressVal = "";
            this.personInfo1.Size = new System.Drawing.Size(877, 570);
            this.personInfo1.TabIndex = 2;
            this.personInfo1.txtEmailVal = "";
            this.personInfo1.txtFirstNameVal = "";
            this.personInfo1.txtLastNameVal = "";
            this.personInfo1.txtNationalNoVal = "";
            this.personInfo1.txtPhoneVal = "";
            this.personInfo1.txtSecondNameVal = "";
            this.personInfo1.txtThirdNameVal = "";
            this.personInfo1.Load += new System.EventHandler(this.personInfo1_Load);
            // 
            // frmAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1077, 655);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.personInfo1);
            this.Controls.Add(this.button1);
            this.Name = "frmAddEdit";
            this.Text = "AddEditForm";
            this.Load += new System.EventHandler(this.frmAddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lTitle;
        private PersonInfo personInfo1;
    }
}
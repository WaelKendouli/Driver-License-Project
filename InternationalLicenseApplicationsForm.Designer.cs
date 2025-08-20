namespace Driver_Licence_Project
{
    partial class frmInternationalLicenseApplicationsForm
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
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInternationalLicencesApplications = new System.Windows.Forms.DataGridView();
            this.cbIsAtive = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicencesApplications)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(342, 161);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(176, 30);
            this.txtFilter.TabIndex = 13;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(150, 161);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(167, 30);
            this.cbFilter.TabIndex = 12;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Filter by ";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(1209, 148);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(97, 40);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.ForestGreen;
            this.label1.Location = new System.Drawing.Point(336, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(579, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "International Driving Licences Application";
            // 
            // dgvInternationalLicencesApplications
            // 
            this.dgvInternationalLicencesApplications.AllowUserToAddRows = false;
            this.dgvInternationalLicencesApplications.AllowUserToDeleteRows = false;
            this.dgvInternationalLicencesApplications.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dgvInternationalLicencesApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicencesApplications.Location = new System.Drawing.Point(12, 206);
            this.dgvInternationalLicencesApplications.Name = "dgvInternationalLicencesApplications";
            this.dgvInternationalLicencesApplications.ReadOnly = true;
            this.dgvInternationalLicencesApplications.RowHeadersWidth = 51;
            this.dgvInternationalLicencesApplications.RowTemplate.Height = 24;
            this.dgvInternationalLicencesApplications.Size = new System.Drawing.Size(1294, 423);
            this.dgvInternationalLicencesApplications.TabIndex = 8;
            // 
            // cbIsAtive
            // 
            this.cbIsAtive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsAtive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsAtive.FormattingEnabled = true;
            this.cbIsAtive.Location = new System.Drawing.Point(342, 163);
            this.cbIsAtive.Name = "cbIsAtive";
            this.cbIsAtive.Size = new System.Drawing.Size(120, 30);
            this.cbIsAtive.TabIndex = 14;
            this.cbIsAtive.SelectedIndexChanged += new System.EventHandler(this.cbIsAtive_SelectedIndexChanged);
            // 
            // frmInternationalLicenseApplicationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 641);
            this.Controls.Add(this.cbIsAtive);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvInternationalLicencesApplications);
            this.Name = "frmInternationalLicenseApplicationsForm";
            this.Text = "International License Applications Form";
            this.Load += new System.EventHandler(this.InternationalLicenseApplicationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicencesApplications)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInternationalLicencesApplications;
        private System.Windows.Forms.ComboBox cbIsAtive;
    }
}
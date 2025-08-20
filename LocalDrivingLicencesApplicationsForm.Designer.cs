namespace Driver_Licence_Project
{
    partial class frmLocalDrivingLicencesApplications
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
            this.components = new System.ComponentModel.Container();
            this.dgvLocalLicencesApplications = new System.Windows.Forms.DataGridView();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduelTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduelWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduelStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issueDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicencesApplications)).BeginInit();
            this.cmsApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocalLicencesApplications
            // 
            this.dgvLocalLicencesApplications.BackgroundColor = System.Drawing.Color.GhostWhite;
            this.dgvLocalLicencesApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicencesApplications.ContextMenuStrip = this.cmsApplications;
            this.dgvLocalLicencesApplications.Location = new System.Drawing.Point(12, 170);
            this.dgvLocalLicencesApplications.Name = "dgvLocalLicencesApplications";
            this.dgvLocalLicencesApplications.RowHeadersWidth = 51;
            this.dgvLocalLicencesApplications.RowTemplate.Height = 24;
            this.dgvLocalLicencesApplications.Size = new System.Drawing.Size(1267, 492);
            this.dgvLocalLicencesApplications.TabIndex = 0;
            // 
            // cmsApplications
            // 
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.editApplicationsToolStripMenuItem,
            this.deleteApplicationToolStripMenuItem,
            this.cancelApplicationToolStripMenuItem,
            this.scheduelTestsToolStripMenuItem,
            this.issueDrivingLicenceToolStripMenuItem,
            this.showLicenceToolStripMenuItem,
            this.showPersToolStripMenuItem});
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.Size = new System.Drawing.Size(302, 196);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.showApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.showApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // editApplicationsToolStripMenuItem
            // 
            this.editApplicationsToolStripMenuItem.Name = "editApplicationsToolStripMenuItem";
            this.editApplicationsToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.editApplicationsToolStripMenuItem.Text = "Edit Applications";
            this.editApplicationsToolStripMenuItem.Click += new System.EventHandler(this.editApplicationsToolStripMenuItem_Click);
            // 
            // deleteApplicationToolStripMenuItem
            // 
            this.deleteApplicationToolStripMenuItem.Name = "deleteApplicationToolStripMenuItem";
            this.deleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.deleteApplicationToolStripMenuItem.Text = "Delete Application";
            this.deleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.deleteApplicationToolStripMenuItem_Click);
            // 
            // cancelApplicationToolStripMenuItem
            // 
            this.cancelApplicationToolStripMenuItem.Name = "cancelApplicationToolStripMenuItem";
            this.cancelApplicationToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.cancelApplicationToolStripMenuItem.Text = "Cancel Application";
            this.cancelApplicationToolStripMenuItem.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // scheduelTestsToolStripMenuItem
            // 
            this.scheduelTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduelToolStripMenuItem,
            this.scheduelWrittenTestToolStripMenuItem,
            this.scheduelStreetTestToolStripMenuItem});
            this.scheduelTestsToolStripMenuItem.Name = "scheduelTestsToolStripMenuItem";
            this.scheduelTestsToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.scheduelTestsToolStripMenuItem.Text = "Scheduel Tests";
            this.scheduelTestsToolStripMenuItem.Click += new System.EventHandler(this.scheduelTestsToolStripMenuItem_Click);
            // 
            // scheduelToolStripMenuItem
            // 
            this.scheduelToolStripMenuItem.Name = "scheduelToolStripMenuItem";
            this.scheduelToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.scheduelToolStripMenuItem.Text = "Scheduel Vision";
            this.scheduelToolStripMenuItem.Click += new System.EventHandler(this.scheduelToolStripMenuItem_Click);
            // 
            // scheduelWrittenTestToolStripMenuItem
            // 
            this.scheduelWrittenTestToolStripMenuItem.Name = "scheduelWrittenTestToolStripMenuItem";
            this.scheduelWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.scheduelWrittenTestToolStripMenuItem.Text = "Scheduel Written test";
            this.scheduelWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.scheduelWrittenTestToolStripMenuItem_Click);
            // 
            // scheduelStreetTestToolStripMenuItem
            // 
            this.scheduelStreetTestToolStripMenuItem.Name = "scheduelStreetTestToolStripMenuItem";
            this.scheduelStreetTestToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.scheduelStreetTestToolStripMenuItem.Text = "Scheduel Street Test";
            this.scheduelStreetTestToolStripMenuItem.Click += new System.EventHandler(this.scheduelStreetTestToolStripMenuItem_Click);
            // 
            // issueDrivingLicenceToolStripMenuItem
            // 
            this.issueDrivingLicenceToolStripMenuItem.Name = "issueDrivingLicenceToolStripMenuItem";
            this.issueDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.issueDrivingLicenceToolStripMenuItem.Text = "Issue Driving Licence ( First Time )";
            this.issueDrivingLicenceToolStripMenuItem.Click += new System.EventHandler(this.issueDrivingLicenceToolStripMenuItem_Click);
            // 
            // showLicenceToolStripMenuItem
            // 
            this.showLicenceToolStripMenuItem.Name = "showLicenceToolStripMenuItem";
            this.showLicenceToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.showLicenceToolStripMenuItem.Text = "Show Licence";
            this.showLicenceToolStripMenuItem.Click += new System.EventHandler(this.showLicenceToolStripMenuItem_Click);
            // 
            // showPersToolStripMenuItem
            // 
            this.showPersToolStripMenuItem.Name = "showPersToolStripMenuItem";
            this.showPersToolStripMenuItem.Size = new System.Drawing.Size(301, 24);
            this.showPersToolStripMenuItem.Text = "Show persons License history";
            this.showPersToolStripMenuItem.Click += new System.EventHandler(this.showPersToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(413, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(482, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Driving Licences Application";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(1182, 121);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(97, 40);
            this.btnAddNew.TabIndex = 2;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(355, 125);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(176, 30);
            this.txtFilter.TabIndex = 7;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(150, 125);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(167, 30);
            this.cbFilter.TabIndex = 6;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter by ";
            // 
            // frmLocalDrivingLicencesApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 674);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvLocalLicencesApplications);
            this.Name = "frmLocalDrivingLicencesApplications";
            this.Text = "LocalDrivingLicencesApplicationsForm";
            this.Load += new System.EventHandler(this.LocalDrivingLicencesApplicationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicencesApplications)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalLicencesApplications;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduelTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem issueDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduelWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduelStreetTestToolStripMenuItem;
    }
}
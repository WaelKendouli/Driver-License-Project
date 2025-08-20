namespace Driver_Licence_Project
{
    partial class TestsForm
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
            this.ctrlDrivingLicenseApplicationInfo1 = new Driver_Licence_Project.ctrlDrivingLicenseApplicationInfo();
            this.lbMainTitle = new System.Windows.Forms.Label();
            this.btnAddApointment = new System.Windows.Forms.Button();
            this.dgvTestApointments = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestApointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlDrivingLicenseApplicationInfo1
            // 
            this.ctrlDrivingLicenseApplicationInfo1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctrlDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(21, 80);
            this.ctrlDrivingLicenseApplicationInfo1.Name = "ctrlDrivingLicenseApplicationInfo1";
            this.ctrlDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(947, 493);
            this.ctrlDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // lbMainTitle
            // 
            this.lbMainTitle.AutoSize = true;
            this.lbMainTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMainTitle.Location = new System.Drawing.Point(408, 41);
            this.lbMainTitle.Name = "lbMainTitle";
            this.lbMainTitle.Size = new System.Drawing.Size(105, 36);
            this.lbMainTitle.TabIndex = 2;
            this.lbMainTitle.Text = "[????]";
            // 
            // btnAddApointment
            // 
            this.btnAddApointment.Location = new System.Drawing.Point(864, 597);
            this.btnAddApointment.Name = "btnAddApointment";
            this.btnAddApointment.Size = new System.Drawing.Size(127, 35);
            this.btnAddApointment.TabIndex = 3;
            this.btnAddApointment.Text = "Add Apointment";
            this.btnAddApointment.UseVisualStyleBackColor = true;
            this.btnAddApointment.Click += new System.EventHandler(this.btnAddApointment_Click);
            // 
            // dgvTestApointments
            // 
            this.dgvTestApointments.AllowUserToAddRows = false;
            this.dgvTestApointments.AllowUserToDeleteRows = false;
            this.dgvTestApointments.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvTestApointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestApointments.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvTestApointments.Location = new System.Drawing.Point(8, 608);
            this.dgvTestApointments.Name = "dgvTestApointments";
            this.dgvTestApointments.ReadOnly = true;
            this.dgvTestApointments.RowHeadersWidth = 51;
            this.dgvTestApointments.RowTemplate.Height = 24;
            this.dgvTestApointments.Size = new System.Drawing.Size(846, 180);
            this.dgvTestApointments.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 80);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // TestsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 861);
            this.Controls.Add(this.dgvTestApointments);
            this.Controls.Add(this.btnAddApointment);
            this.Controls.Add(this.lbMainTitle);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo1);
            this.Name = "TestsForm";
            this.Text = "TestsForm";
            this.Load += new System.EventHandler(this.TestsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestApointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label lbMainTitle;
        private System.Windows.Forms.Button btnAddApointment;
        private System.Windows.Forms.DataGridView dgvTestApointments;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}
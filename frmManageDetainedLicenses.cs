using DetainedLicensesBussiness;
using LicenseBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class frmManageDetainedLicenses : Form
    {
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }
        DataTable _dt = new DataTable();
        private void _RefreshGrid()
        {
          _dt  = clsDetainedLicenses.GetAllDetainedLicenses();
            if (_dt.Rows.Count>0)
            {
                dgvDteainedLicenses.DataSource = _dt;
                dgvDteainedLicenses.Columns[0].HeaderText = "Detain ID";
                dgvDteainedLicenses.Columns[0].Width = 50;

                dgvDteainedLicenses.Columns[1].HeaderText = "License ID";
                dgvDteainedLicenses.Columns[1].Width = 50;

                dgvDteainedLicenses.Columns[2].HeaderText = "Detain Date";
                dgvDteainedLicenses.Columns[2].Width = 100;

                dgvDteainedLicenses.Columns[3].HeaderText = "is Released";
                dgvDteainedLicenses.Columns[3].Width = 20;

                dgvDteainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDteainedLicenses.Columns[4].Width = 50;

                dgvDteainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDteainedLicenses.Columns[5].Width = 100;

                dgvDteainedLicenses.Columns[6].HeaderText = "National No";
                dgvDteainedLicenses.Columns[6].Width = 50;

                dgvDteainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDteainedLicenses.Columns[7].Width = 250;

                dgvDteainedLicenses.Columns[8].HeaderText = "Release Application ID";
                dgvDteainedLicenses.Columns[8].Width = 100;



            }
        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _RefreshGrid();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _RefreshGrid();

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            _RefreshGrid();

        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            releaseToolStripMenuItem.Enabled = !(bool)dgvDteainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.FindLicenseByID((int)dgvDteainedLicenses.CurrentRow.Cells[1].Value);

            frmShowPersonDetails frm = new frmShowPersonDetails(license.driver.PersonID);
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.FindLicenseByID((int)dgvDteainedLicenses.CurrentRow.Cells[1].Value);

            frmDriverCard frm = new frmDriverCard(license.ApplicationID);
            frm.ShowDialog();
        }

        private void releaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dgvDteainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshGrid();
        }

        private void showPersonLicenseHistroyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.FindLicenseByID((int)dgvDteainedLicenses.CurrentRow.Cells[1].Value);

            frmShowLicenseHistory frm = new frmShowLicenseHistory(license.ApplicationID);
            frm.ShowDialog();
        }
    }
}

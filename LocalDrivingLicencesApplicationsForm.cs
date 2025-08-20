using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationsBussinessLayer;
using LicenseBussinessLayer;
using LocalDLBussinessLayer;
namespace Driver_Licence_Project
{
    public partial class frmLocalDrivingLicencesApplications : Form
    {
        private DataTable _dtAllLocalLicenses;
        public frmLocalDrivingLicencesApplications()
        {
            InitializeComponent();
        }

        private void _RefreshDgv()
        {
            _dtAllLocalLicenses = clsLocalDrivingLicenseApplication.GetAllLocalLicencesData();
            dgvLocalLicencesApplications.DataSource = _dtAllLocalLicenses;
        }
        private void _FillComboBoxFilter()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("LDL AppID");
            cbFilter.Items.Add("National No");
            cbFilter.Items.Add("Full name");
            cbFilter.Items.Add("Status");

        }
        private void LocalDrivingLicencesApplicationsForm_Load(object sender, EventArgs e)
        {

            _RefreshDgv();
            if (dgvLocalLicencesApplications.Rows.Count>0)
            {
            dgvLocalLicencesApplications.Columns[0].HeaderText = "LDL AppID";
            dgvLocalLicencesApplications.Columns[1].HeaderText = "Driving Class";
            dgvLocalLicencesApplications.Columns[2].HeaderText = "National No";
            dgvLocalLicencesApplications.Columns[3].HeaderText = "Full name";
            dgvLocalLicencesApplications.Columns[4].HeaderText = "Application Date";
            dgvLocalLicencesApplications.Columns[5].HeaderText = "Passed Tests";
            dgvLocalLicencesApplications.Columns[6].HeaderText = "Status";
            }
            _FillComboBoxFilter();
            cbFilter.SelectedIndex = 0;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicence frm = new frmAddNewLocalDrivingLicence();
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilter.SelectedIndex != 0);
            if (cbFilter.SelectedIndex==0||string.IsNullOrEmpty(txtFilter.Text))
            {
                _RefreshDgv();
                txtFilter.Clear();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";
            switch (cbFilter.Text)
            {
                case "LDL AppID":
                    FilterBy = "ApplicationID";
                    break;
                case "Driving Class":
                    FilterBy = "ClassName";
                    break;
                case "National No":
                    FilterBy = "NationalNo";
                    break;
                case "Full name":
                    FilterBy = "Fullname";
                    break;
                case "Status":
                    FilterBy = "Status";
                    break;

            }

            if (txtFilter.Text==""||cbFilter.SelectedIndex==0)
            {
                return;
            }

            if (cbFilter.Text!= "LDL AppID")
            {
                _dtAllLocalLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%' ", FilterBy, txtFilter.Text);
            }
            else
            {
                _dtAllLocalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterBy, txtFilter.Text);
            }

        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to switch to canceled" , "Canceling" , MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
             clsApplication.SwitchAppStatusToCanceled((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
                MessageBox.Show("Canceled Successfully");
            _RefreshDgv();   
            }
            
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLocalDrivingLicenseDetails frm = new frmShowLocalDrivingLicenseDetails((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void editApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLocalDrivingLicence frm = new frmAddNewLocalDrivingLicence((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void scheduelToolStripMenuItem_Click(object sender, EventArgs e) // Scheduel vision
        {
            TestsForm frm = new TestsForm((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value, 1);// Test Type
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void scheduelWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestsForm frm = new TestsForm((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value, 2);
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void scheduelStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestsForm frm = new TestsForm((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value, 3);
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void issueDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicense frm = new frmIssueDrivingLicense((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshDgv();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsLocalDrivingLicenseApplication.DeleteLDLA((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value))
            {
                if (clsApplication.DeleteApplication((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshDgv();
                }
             
            }
            else
            {
                MessageBox.Show("Deletion Failed", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void showLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverCard frm = new frmDriverCard((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {

            clsLocalDrivingLicenseApplication LDLA = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicationID((int)dgvLocalLicencesApplications.CurrentRow.Cells[0].Value);

            int TotalPassedTests = (int)dgvLocalLicencesApplications.CurrentRow.Cells[5].Value;

            bool DoesLicenseExist = clsLicense.isLicenseExist(LDLA.ApplicationID);

            issueDrivingLicenceToolStripMenuItem.Enabled = (TotalPassedTests == 3 && !DoesLicenseExist);
            showLicenceToolStripMenuItem.Enabled = DoesLicenseExist;
            bool VisionTestPassed = clsLocalDrivingLicenseApplication.IsPassedThisTestBefore(LDLA.ApplicationID, 1);//Vision Test
            bool WrittenTestPassed = clsLocalDrivingLicenseApplication.IsPassedThisTestBefore(LDLA.ApplicationID, 2);//Written Test
            bool StreetTestPassed = clsLocalDrivingLicenseApplication.IsPassedThisTestBefore(LDLA.ApplicationID, 3);//Street Test
            scheduelToolStripMenuItem.Enabled = (VisionTestPassed == false && WrittenTestPassed == false && StreetTestPassed == false);
            scheduelWrittenTestToolStripMenuItem.Enabled = (VisionTestPassed == true && WrittenTestPassed == false && StreetTestPassed == false);
            scheduelStreetTestToolStripMenuItem.Enabled = (VisionTestPassed == true && WrittenTestPassed == true && StreetTestPassed == false);

            scheduelTestsToolStripMenuItem.Enabled = !(VisionTestPassed == true && WrittenTestPassed == true && StreetTestPassed == true);


            editApplicationsToolStripMenuItem.Enabled = (LDLA.ApplicationStatusNum < 2);// 2 is Cnaceled Status
            cancelApplicationToolStripMenuItem.Enabled = (LDLA.ApplicationStatusNum == 1);// 1 New Status 
            deleteApplicationToolStripMenuItem.Enabled = (LDLA.ApplicationStatusNum != 3);

        }

        private void scheduelTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

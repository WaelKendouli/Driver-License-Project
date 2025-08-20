using DetainedLicensesBussiness;
using LicenseBussinessLayer;
using LoggedInUser;
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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private int LicenseID =-1; 
        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LicenseID == -1)
            {
                return;
            }
            clsLicense License = clsLicense.FindLicenseByID(LicenseID);
            frmDriverCard frm = new frmDriverCard(License.ApplicationID);
            frm.ShowDialog();
        }

        private void ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            LicenseID = obj;
            if (clsDetainedLicenses.isLicenseDetained(LicenseID))
            {
                MessageBox.Show("License with ID : "+ LicenseID+ " is already detained choose another one", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                btnIssue.Enabled = true;
            }


        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = true;
            btnIssue.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            lblCreatedByUser.Text = clsCurrentUser.GlobalUser.UserName;
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            llShowLicenseHistory.Enabled = true;
        }

        private void _FillDetainedLicenseData(clsDetainedLicenses DetainedLicense)
        {
            DetainedLicense.DetainDate = DateTime.Now;
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                DetainedLicense.FineFees = 0;
            }
            else
            {
                DetainedLicense.FineFees = Convert.ToSingle(txtFineFees.Text);
            }
            DetainedLicense.LicenseID = LicenseID;
            DetainedLicense.IsReleased = 0;
            DetainedLicense.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
            DetainedLicense.ReleaseDate = Convert.ToDateTime("1/1/1753");
            DetainedLicense.ReleaseApplicationID = -1;
            DetainedLicense.ReleasedByUserID = -1;

        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense==null)
            {
                MessageBox.Show("select a license please", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            clsDetainedLicenses DetainLicense = new clsDetainedLicenses(clsDetainedLicenses.enStatus.eDetain);
            _FillDetainedLicenseData(DetainLicense);
            if (DetainLicense.Save())
            {
                MessageBox.Show("License with ID : " + LicenseID + " Detained Successfully ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Detain ID : " + DetainLicense.DetainID , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                llShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
                ctrlDriversLicenseInfoWithFilter1.FilterEnabled = false;
                lblLicenseID.Text = LicenseID.ToString();
                lblDetainID.Text = DetainLicense.DetainID.ToString();
                llShowLicenseHistory.Enabled = true;
            }
            else
            {
                MessageBox.Show("License with ID : " + LicenseID + " Detaining failed", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LicenseID==-1)
            {
                return;
            }
            clsLicense License = clsLicense.FindLicenseByID(LicenseID);
            frmShowLicenseHistory frm = new frmShowLicenseHistory(License.ApplicationID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

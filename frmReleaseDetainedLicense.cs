using ApplicationTypesBussinessLayer;
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
using UsersBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = true;
            btnRelease.Enabled = false;
        }
        public frmReleaseDetainedLicense(int LicenceID)
        {
            InitializeComponent();
            ctrlDriversLicenseInfoWithFilter1.LoadLicenseInfos(LicenceID);
            ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(LicenceID);
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = false;
            btnRelease.Enabled = true;
        }

        private int LicenseID = -1;
        private clsDetainedLicenses DetainedLicense;
        private double AppFees;
        private void ctrlDriversLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {
            llShowLicenseHistory.Enabled = true;
        }

        private void ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            LicenseID = obj;
            if (clsDetainedLicenses.isLicenseDetained(LicenseID)==false)
            {
                MessageBox.Show("License with ID : " + LicenseID + " is not detained choose another one", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                btnRelease.Enabled = true;
            }
            DetainedLicense = clsDetainedLicenses.FindDetainedLicenseByLicenseID(LicenseID);
            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblLicenseID.Text = DetainedLicense.LicenseID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text = DetainedLicense.FineFees.ToString();
            lblCreatedByUser.Text = clsUser.FindUserByID(DetainedLicense.CreatedByUserID).UserName;
             AppFees = clsApplicationTypes.FindApplicationTypeByID(5).ApplicationFees;
            lblApplicationFees.Text = AppFees.ToString();
            lblTotalFees.Text = (AppFees + DetainedLicense.FineFees).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillApplicationInfo()
        {
            DetainedLicense.LicenseID = LicenseID;
            DetainedLicense.ApplicationDate = DateTime.Now;
            DetainedLicense.ApplicationStatusNum= 1;
            DetainedLicense.ApplicantPersonID = ctrlDriversLicenseInfoWithFilter1.SelectedLicense.driver.PersonID;
            DetainedLicense.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = clsCurrentUser.GlobalUser.UserID;
            DetainedLicense.LastStatusDate = DateTime.Now;
            DetainedLicense.PaidFees = Convert.ToSingle(AppFees + DetainedLicense.FineFees);
            DetainedLicense.ApplicationTypeID = 5;
           

        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (LicenseID==-1)
            {
                MessageBox.Show("Please choose a License");
                return;
            }
            if (DetainedLicense.IsReleased==1)
            {
                MessageBox.Show("License is already released ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Are you sure ?","",MessageBoxButtons.OKCancel , MessageBoxIcon.Question)==DialogResult.Cancel)
            {
                return;
            }
            _FillApplicationInfo();
            if (DetainedLicense.Save())
            {
                MessageBox.Show("License with id " + DetainedLicense.LicenseID + " Released Successfully ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Application with ID " + DetainedLicense.ApplicationID + " created Successfully ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblApplicationID.Text = DetainedLicense.ApplicationID.ToString();
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                ctrlDriversLicenseInfoWithFilter1.FilterEnabled = false;
                btnRelease.Enabled = false;
            }
            else
            {
                MessageBox.Show("License with id " + DetainedLicense.LicenseID + " Released failed ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LicenseID == -1)
            {
                return;
            }
            clsLicense License = clsLicense.FindLicenseByID(LicenseID);
            frmShowLicenseHistory frm = new frmShowLicenseHistory(License.ApplicationID);
            frm.ShowDialog();
        }

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
    }
}

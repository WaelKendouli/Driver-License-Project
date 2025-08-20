using ApplicationsBussinessLayer;
using ApplicationTypesBussinessLayer;
using LicenseBussinessLayer;
using LocalDLBussinessLayer;
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
    public partial class frmRenewDrivingLicense : Form
    {
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }
        private double AppFees;
        clsLicense license;
        clsLicense NewLicense = new clsLicense();
        clsApplication NewApp = new clsApplication();
        clsApplication OldApplication = new clsApplication();
        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = true;
            llShowNewLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            AppFees = clsApplicationTypes.FindApplicationTypeByTitle("Renew Driving License Service").ApplicationFees;
            lblApplicationFees.Text = AppFees.ToString();
            lblCreatedByUser.Text = clsCurrentUser.GlobalUser.UserName;

        }

        private void ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
             license = ctrlDriversLicenseInfoWithFilter1.SelectedLicense;
            if (license!=null)
            {
                bool isExspired = license.IsLicenseExpired();
            if (isExspired == false)
            {
                MessageBox.Show("License with ID : " + license.LicenseID + " is not Exspired cannot renew it", "Unexspired License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
            }
            else
            {
                    btnIssue.Enabled = true;
            }
                lblOldLicenseID.Text = license.LicenseID.ToString();
                lblLicenseFees.Text = license.PaidFees.ToString();
                lblTotalFees.Text = (license.PaidFees + AppFees).ToString();
                lblExpirationDate.Text = DateTime.Now.AddYears(license._LicenseClass.DefaultValidityLength).ToShortDateString();
            }
            

        }

        private bool _AddNewApplicationInfo()
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense!=null)
            {
                OldApplication = clsApplication.FindApplicationByID(license.ApplicationID);
                NewApp.ApplicationStatusNum = 0;
                NewApp.ApplicationDate = DateTime.Now;
                NewApp.LastStatusDate = DateTime.Now;
                NewApp.PaidFees = OldApplication.PaidFees;
                NewApp.ApplicantPersonID = OldApplication.ApplicantPersonID;
                NewApp.ApplicationTypeID = clsApplicationTypes.FindApplicationTypeByTitle("Renew Driving License Service").ApplicationTypeID;
                NewApp.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
                
            }
            else
            {
                return false;
            }
            return NewApp.Save();

           
        }

        private bool _AddNewLicenseInfos()
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense!=null)
            {
                NewLicense.LicenseClass = license.LicenseClass;
                NewLicense.ApplicationID = NewApp.ApplicationID;
                NewLicense.Notes = txtNotes.Text;
                NewLicense.DriverID = license.DriverID;
                NewLicense.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(license._LicenseClass.DefaultValidityLength);
                NewLicense.PaidFees = license.PaidFees;
                NewLicense.IssueReason = (clsLicense.enIssueReason)2;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.IsActive = 1;
                
            }
            else
            {
                return false;
            }

            return NewLicense.Save();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense==null)
            {
                MessageBox.Show("Invalid Issue , search for license first", "Unexspired License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (license.IsActive ==0)
            {
                MessageBox.Show("Invalid Renew , your driving license is disactivated", "Unexspired License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_AddNewApplicationInfo()==true)
            {
                if (_AddNewLicenseInfos())
                {
                    MessageBox.Show("New License with ID : "+NewLicense.LicenseID+" Added successfully", "Renew License", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                    lblApplicationID.Text = NewApp.ApplicationID.ToString();
                    btnIssue.Enabled = false;
                    ctrlDriversLicenseInfoWithFilter1.FilterEnabled = false;
                    llShowNewLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Adding New License Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                if (license.DisactivateLicense())
                {
                    MessageBox.Show("Old License with ID : " + license.LicenseID + " Disativated successfully", "Disactivating", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Old License with ID : " + license.LicenseID + " Disativating failed", "Disactivating", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Adding New Application Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense!=null)
            {
                frmShowLicenseHistory frm = new frmShowLicenseHistory(license.ApplicationID);
                frm.ShowDialog();
            }
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverCard frm = new frmDriverCard(NewLicense.ApplicationID);
            frm.ShowDialog();
        }
    }
}

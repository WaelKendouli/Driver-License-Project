using ApplicationsBussinessLayer;
using ApplicationTypesBussinessLayer;
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
    public partial class frmReplaceForLostOrDamagedLicense : Form
    {
        clsLicense License;

        enum enReplaceMode  { eDamaged = 1 , eLost = 2}
        enReplaceMode _IssueReplaceMode = enReplaceMode.eDamaged;
        clsApplicationTypes Apptype;
        clsApplication NewApp = new clsApplication();
        clsLicense NewLicense = new clsLicense();
        public frmReplaceForLostOrDamagedLicense()
        {
            InitializeComponent();
        }

       
        private void _SelectAppType()
        {
            string AppTitle = "";
            if (_IssueReplaceMode == enReplaceMode.eDamaged)
            {
                AppTitle = "Replacement for a Damaged Driving License";
                Apptype = clsApplicationTypes.FindApplicationTypeByTitle(AppTitle.Trim());
            }
            else
            {
                AppTitle = "Replacement for a Lost Driving License";
                Apptype = clsApplicationTypes.FindApplicationTypeByTitle(AppTitle.Trim());
            }
            lblApplicationFees.Text = Apptype.ApplicationFees.ToString();
        }
        private void _SetTitle()
        {
            if (_IssueReplaceMode==enReplaceMode.eDamaged)
            {
                lbTitle.Text = "Replacement for a Damaged Driving License";
            }
            else
            {
                lbTitle.Text = "Replacement for a Lost Driving License";
            }
        }
        private void _SetModeForReplacement()
        {
            if (rbDamagedLicense.Checked)
            {
                _IssueReplaceMode = enReplaceMode.eDamaged;
                _SetTitle();
                _SelectAppType();
            }
            else
            {
                _IssueReplaceMode = enReplaceMode.eLost;
                _SetTitle();
                _SelectAppType();
            }
        }

       
        private void ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            lblRreplacedLicenseID.Text = "[???]";
            lblApplicationID.Text = "[???]";
            llShowLicenseInfo.Enabled = false;
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense!=null)
            {
                if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense.isThisLicenseActive()==false)
                {
                    MessageBox.Show("Inactive Licenses cannot be replaced", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    return;
                }
                btnIssue.Enabled = true;
                License = ctrlDriversLicenseInfoWithFilter1.SelectedLicense;
                lblOldLicenseID.Text = License.LicenseID.ToString();
                llShowLicenseHistory.Enabled = (License != null);
            }
        }

        private void frmReplaceForLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
           
            llShowLicenseHistory.Enabled = false;
            llShowLicenseInfo.Enabled = (License != null);
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = true;
            btnIssue.Enabled = false;
            rbDamagedLicense.Checked = true;
            _SetModeForReplacement();
            lblCreatedByUser.Text = clsCurrentUser.GlobalUser.UserName;
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _SetModeForReplacement();
        }

        private bool HandelEdgeCasesBeforeIssue()
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense==null)
            {
                MessageBox.Show("Please choose License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return false;
            }
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense.isThisLicenseActive() == false)
            {
                MessageBox.Show("Inactive Licenses cannot be replaced", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _AddNewApplicationInfo()
        {
            NewApp.ApplicationStatusNum = 1;
            NewApp.ApplicationDate = DateTime.Now;
            NewApp.LastStatusDate = DateTime.Now;
            NewApp.ApplicantPersonID = License.driver.Person.PersonID;
            NewApp.ApplicationTypeID = Apptype.ApplicationTypeID;
            NewApp.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
            NewApp.PaidFees = Convert.ToSingle(Apptype.ApplicationFees);

            return (NewApp.Save());
        }

        private void _SelectIssueReasonForNewLicense()
        {
            switch (_IssueReplaceMode)
            {
                case enReplaceMode.eDamaged:
                    NewLicense.IssueReason = (clsLicense.enIssueReason)3;
                    break;
                case enReplaceMode.eLost:
                    NewLicense.IssueReason = (clsLicense.enIssueReason)4;
                    break;
            }

        }
        private bool _AddNewLicenseInfo()
        {
            NewLicense.Notes = License.Notes;
            NewLicense.ApplicationID = NewApp.ApplicationID;
            NewLicense.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
            _SelectIssueReasonForNewLicense();
            NewLicense.DriverID = License.DriverID;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(License._LicenseClass.DefaultValidityLength);
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.PaidFees = License.PaidFees;
            NewLicense.IsActive = 1;
            NewLicense.LicenseClass = License.LicenseClass;

            return (NewLicense.Save());

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (HandelEdgeCasesBeforeIssue()==false)
            {
                return;
            }
            if (_AddNewApplicationInfo())
            {
                MessageBox.Show("New Application with ID : "+ NewApp.ApplicationID +" Added Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblApplicationID.Text = NewApp.ApplicationID.ToString();
                if (_AddNewLicenseInfo())
                {
                    MessageBox.Show("New License with ID : " + NewLicense.LicenseID + " Added Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (License.DisactivateLicense())
                    {
                        MessageBox.Show(" License with ID : " + License.LicenseID + " disactivated Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(" License with ID : " + License.LicenseID + " disactivating failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    lblRreplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                    llShowLicenseInfo.Enabled = true;
                    btnIssue.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Adding New License Failed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (License!=null)
            {
                frmShowLicenseHistory frm = new frmShowLicenseHistory(License.ApplicationID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please search for a license first ");
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverCard frm = new frmDriverCard(NewLicense.ApplicationID);
            frm.ShowDialog();

        }
    }
}

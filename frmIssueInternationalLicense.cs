using ApplicationTypesBussinessLayer;
using InternationalLicensesBussiness;
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
    public partial class frmIssueInternationalLicense : Form
    {
        clsApplicationTypes AppType = new clsApplicationTypes();
        clsInternational InternationalLicense  = new clsInternational();
        clsLicense license = new clsLicense();
        public frmIssueInternationalLicense()
        {
            InitializeComponent();
        }
        void LoadApplicationInfos()
        {
            ctrlDriversLicenseInfoWithFilter1.FilterEnabled = true;
            AppType = clsApplicationTypes.FindApplicationTypeByTitle("New International License");
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsCurrentUser.GlobalUser.UserName;
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = AppType.ApplicationFees.ToString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            
        }
        private void frmIssueInternationalLicense_Load(object sender, EventArgs e)
        {
            LoadApplicationInfos();
            llShowLicenseInfo.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckLicenseBeforeIssue()
        {
            
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense==null)
            {
                MessageBox.Show("Issue Failed", "error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                license = ctrlDriversLicenseInfoWithFilter1.SelectedLicense;
                lblLocalLicenseID.Text = license.LicenseID.ToString();
                lblApplicationID.Text = license.ApplicationID.ToString();
            }

            if (license.LicenseClass!=3)
            {
                MessageBox.Show("License with ID" + license.LicenseID + " should be from Class 3 , choose another one ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (license.IsLicenseExpired())
            {
                MessageBox.Show("License with ID"+ license.LicenseID +" is Expired , make sure to renew it ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (license.IsActive==0)
            {
                MessageBox.Show("License with ID" + license.LicenseID + " is disactivated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (clsInternational.isLicenseExist(license.LicenseID))
            {
                MessageBox.Show("Issue Failed", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void FillInternationalLicenseInfo()
        {
            if (license!=null)
            {
                InternationalLicense.ApplicationID = license.ApplicationID;
                InternationalLicense.CreatedByUserID = license.CreatedByUserID;
                InternationalLicense.ExpirationDate = license.ExpirationDate;
                InternationalLicense.DriverID = license.DriverID;
                InternationalLicense.IssuedUsingLocalLicenseID = license.LicenseID;
                InternationalLicense.IsActive = license.IsActive;
                InternationalLicense.IssueDate = DateTime.Now;
              
            }

        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (CheckLicenseBeforeIssue()==false)
            {
                return;
            }
            
            FillInternationalLicenseInfo();

            if (MessageBox.Show("Are you sure ?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
             if (InternationalLicense.Save())
            {
                    
                MessageBox.Show("international License with ID " + InternationalLicense.InternationalLicenseID + " Added successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();   
                    llShowLicenseInfo.Enabled = clsInternational.isLicenseExist(InternationalLicense.IssuedUsingLocalLicenseID);
                    ctrlDriversLicenseInfoWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Adding Failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
            }
           
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseDetails frm = new frmShowInternationalLicenseDetails(InternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void ctrlDriversLicenseInfoWithFilter1_OnLicenseFound(int obj)
        {
            bool isExist = clsInternational.isLicenseExist(obj);
            llShowLicenseInfo.Enabled = isExist;

            if (isExist==true)
            {
                btnIssue.Enabled = false;
                InternationalLicense = clsInternational.FindLicenseByLocalLicenseID(obj);
                MessageBox.Show("License with ID " + InternationalLicense.IssuedUsingLocalLicenseID + " is already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (InternationalLicense!=null)
                {
                    lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
                    lblLocalLicenseID.Text = InternationalLicense.License.LicenseID.ToString();
                    lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
                }
            }
            CheckLicenseBeforeIssue();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlDriversLicenseInfoWithFilter1.SelectedLicense!=null)
            {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(ctrlDriversLicenseInfoWithFilter1.SelectedLicense.ApplicationID);
                frm.ShowDialog();    
            }
            
            
        }

        private void ctrlDriversLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}

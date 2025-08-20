using ApplicationsBussinessLayer;
using DriversBussinesLayer;
using LicenseBussinessLayer;
using LocalDLBussinessLayer;
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
    public partial class frmIssueDrivingLicense : Form
    {
        private int AppID = 0;
        clsLocalDrivingLicenseApplication LDLA = new clsLocalDrivingLicenseApplication();
        clsLicense License = new clsLicense();
        clsDrivers Driver = new clsDrivers();
        public frmIssueDrivingLicense(int LDLA_AppID)
        {
            InitializeComponent();
            AppID = LDLA_AppID;
            LDLA = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicationID(LDLA_AppID);
        }

        private void frmIssueDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseInfoByApplicationID(AppID);
        }
        private bool AddDriversInfo()
        {
            Driver.CreatedDate = DateTime.Now;
            Driver.CreatedByUserID = LDLA.CreatedByUserID;
            Driver.PersonID = LDLA.ApplicantPersonID;
            Driver.Person = LDLA.Person;
            Driver.User = LDLA.User;
            return (Driver.Save());
        }
   
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isPersonADriver = clsDrivers.IsPersonExistsAsDriver(LDLA.ApplicantPersonID);
            if (AddDriversInfo()||isPersonADriver==true)
            {
                if (isPersonADriver==true)
                {
                    Driver = clsDrivers.FindDriverByPersonID(LDLA.ApplicantPersonID);
                }
                License.LicenseClass = LDLA.LicenseClassID;
                License._LicenseClass = LDLA._LicenseClass;
                License.ApplicationID = LDLA.ApplicationID;
                License.IsActive = 0;
                License.DriverID = Driver.DriverID;
                License.driver = Driver;
                License.IssueDate = DateTime.Now;
                License.ExpirationDate = DateTime.Now.AddYears(LDLA._LicenseClass.DefaultValidityLength);
                License.CreatedByUserID = LDLA.CreatedByUserID;
                License.User = LDLA.User;
                License.IssueReason = clsLicense.enIssueReason.FirstTime;
                License.Notes = txtNotes.Text;
                License.PaidFees = LDLA.PaidFees;
                if (License.Save())
                {
                    MessageBox.Show("License with ID "+ License.LicenseID.ToString() +" Addedd  Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    clsApplication.SwitchAppStatusToCompleted(License.ApplicationID);
                }
                else
                {
                    MessageBox.Show("Adding Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

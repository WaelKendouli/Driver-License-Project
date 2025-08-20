using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoggedInUser;
using LicencesClassesBussinessLayer;
using ApplicationTypesBussinessLayer;
using ApplicationsBussinessLayer;
using LocalDLBussinessLayer;
namespace Driver_Licence_Project
{
    public partial class frmAddNewLocalDrivingLicence : Form
    {
        public enum enMode { eAddNew , eUpdate }
        private enMode _Mode; 

        public frmAddNewLocalDrivingLicence()
        {
            InitializeComponent();
            _Mode = enMode.eAddNew;
        }

        public frmAddNewLocalDrivingLicence(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _SelectedLocalDrivingLicenseID = LocalDrivingLicenseApplicationID;
            _Mode = enMode.eUpdate;

        }
        private int _SelectedLocalDrivingLicenseID;
        private DataTable LicensesClasses = clsLicencesClasses.GetAllLicensesClasses();
        private double _AppFees = clsApplicationTypes.FindApplicationTypeByTitle("New Local Driving License Service").ApplicationFees;
        private int _AppTypeID = clsApplicationTypes.FindApplicationTypeByTitle("New Local Driving License Service").ApplicationTypeID;
        private clsLocalDrivingLicenseApplication LDL_App;
        private int LicenseClassID;
        private void _FillComboboxWithClasses()
        {

            foreach (DataRow Row in LicensesClasses.Rows)
            {
                cbClasses.Items.Add(Row["ClassName"]);
            }
            cbClasses.SelectedIndex = 0;
        }

        private void _ResetDefaultValues()
        {
            _FillComboboxWithClasses();
            if (_Mode == enMode.eAddNew)
            {
                lbTitle.Text = "New Local Driving License Service";
                lbAppDate.Text = DateTime.Now.ToShortDateString();
                lbAppFees.Text = _AppFees.ToString();
                LDL_App = new clsLocalDrivingLicenseApplication();
                lbCreatedBy.Text = clsCurrentUser.GlobalUser.UserName;
                cbClasses.SelectedIndex = 2;
            }
            else
            {
                lbTitle.Text = "Update Local Driving License Service";
                this.Text = "Update Local Driving License Service";

            }
        }

        private void _LoadData()
        {
            personInfoCardWithFilter1.ShowFilter = false;
            LDL_App = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicationID(_SelectedLocalDrivingLicenseID);
            if (LDL_App==null)
            {
                MessageBox.Show("Local Driving License Application ID : "+_SelectedLocalDrivingLicenseID+"Doesn't Exist" , "Error" , MessageBoxButtons.OKCancel , MessageBoxIcon.Error);
                return;
            }
            personInfoCardWithFilter1.LoadPersonInfo(LDL_App.ApplicantPersonID);
            lbApplicationID.Text = LDL_App.ApplicationID.ToString();
            lbAppDate.Text = LDL_App.ApplicationDate.ToShortDateString();
            cbClasses.SelectedIndex = cbClasses.FindString(clsLicencesClasses.FindLicenceByID(LDL_App.LicenseClassID).ClassName);
            lbAppFees.Text = LDL_App.PaidFees.ToString();
            lbCreatedBy.Text = LDL_App.User.UserName;
        }

        private void frmAddNewLocalDrivingLicence_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode==enMode.eUpdate)
            {
                _LoadData();
            }

        }

        private bool ValidateApplicationBeforeSaving()
        {
            if (personInfoCardWithFilter1.PersonID == 0)
            {
                MessageBox.Show("Please Make sure to Search for an Applicant first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            LicenseClassID = clsLicencesClasses.FindLicenceByClassName(cbClasses.Text).LicenseClassID;
            if (clsApplication.isApplicationNotValid(personInfoCardWithFilter1.PersonID , LicenseClassID , _AppTypeID)==true)
            {
                MessageBox.Show("Applicant with ID : "+ personInfoCardWithFilter1.PersonID.ToString() +" can't apply for the same class twice or completed applications ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            if (clsApplication.DoesThisPersonHasLicenseInThisClass(personInfoCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Applicant with ID : " + personInfoCardWithFilter1.PersonID.ToString() + " can't apply for the same class if he had a license before ,  pick an other class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        private void _FillApplicationInfo()
        {
            LDL_App.ApplicantPersonID = personInfoCardWithFilter1.PersonID;
            LDL_App.ApplicationDate = DateTime.Today;
            LDL_App.ApplicationStatusNum = 1;
            LDL_App.ApplicationTypeID = _AppTypeID;
            LDL_App.LastStatusDate = DateTime.Now;
            LDL_App.CreatedByUserID = clsCurrentUser.GlobalUser.UserID;
            LDL_App.LastStatusDate = DateTime.Now;
            LDL_App.PaidFees = Convert.ToSingle(_AppFees);
            LDL_App.LicenseClassID = LicenseClassID;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateApplicationBeforeSaving() == false)
            {
                return;
            }
            
             _FillApplicationInfo();

            if (LDL_App.Save())
            {
                MessageBox.Show("Saved Successfully");
                lbApplicationID.Text = LDL_App.ApplicationID.ToString();
                _Mode = enMode.eUpdate;
                lbTitle.Text = "Update Local Driving License Service";
                this.Text = "Update Local Driving License Service";
                
            }
            else
            {
                MessageBox.Show("Saving failed");
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex++;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

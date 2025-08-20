using ApplicationTypesBussinessLayer;
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
using TestApointmentsBussinessLyer;
using TestsBusinessLayer;
using TestTypesBusinessLayer;

namespace Driver_Licence_Project
{
    public partial class frmScheduelTest : Form
    {
        enum enSchedeulMode { eVision = 1 , eWritten = 2 , ePractical = 3 }
        enum enFormMode { eAddNew = 1 , eUpdate = 2 }
        enFormMode _FormMode;
        enSchedeulMode _Mode;
        string TestTile;
        float TestTypeFees;
         int TestTypeID ;
        int TrialCounter = 0;
        private int _TestAppointmentID;
        clsLocalDrivingLicenseApplication _LDLA = new clsLocalDrivingLicenseApplication();
        clsTestApointment _TestAppointment = new clsTestApointment();
         double RetakeTestFees;



        private void SetTestTypeTile()
        {
            switch (_Mode)
            {
                case enSchedeulMode.eVision:
                    TestTile = "Vision Test";
                    groupBox1.Text = TestTile;
                    break;
                case enSchedeulMode.eWritten:
                    TestTile = "Written (Theory) Test";
                    groupBox1.Text = TestTile;
                    break;
                case enSchedeulMode.ePractical:
                    TestTile = "Practical (Street) Test";
                    groupBox1.Text = TestTile;
                    break;
            }
        }

        private void _HandleEditingMode()
        {
            if (_TestAppointment==null&&_FormMode==enFormMode.eUpdate)
            {
                dtpDate.Enabled = false;
                lbWarning.Text = "Person already sat for the test , Appointment locked";
                btnSave.Enabled = false;
            }
            else
            {
                lbWarning.Visible = false;
            }
        }
        public frmScheduelTest(clsLocalDrivingLicenseApplication LDLA , int ModeType , short FormMode , int TestAppointmentID , int TrialsCount)
        {
            InitializeComponent();
            _Mode = (enSchedeulMode)ModeType;
            _FormMode = (enFormMode)FormMode;
            _LDLA = LDLA;
            SetTestTypeTile();
            TestTypeFees = clsTestType.FindTestTypeByTitle(TestTile).TestTypeFees;
            RetakeTestFees = clsApplicationTypes.FindApplicationTypeByID(2).ApplicationFees;
            TestTypeID = clsTestType.FindTestTypeByTitle(TestTile).TestTypeID;
            _TestAppointment = clsTestApointment.FindTestAppointment(_LDLA.LocalLicenseApplicationID, TestTypeID);
            _HandleEditingMode();
            _TestAppointmentID = TestAppointmentID;
            if (TrialsCount < 0)
            {
                TrialCounter = 0;
            }
            else
            {
                TrialCounter = TrialsCount;
            }
        }


        public frmScheduelTest(clsLocalDrivingLicenseApplication LDLA, int ModeType, short FormMode , int TrialsCount)
        {
            InitializeComponent();
            _Mode = (enSchedeulMode)ModeType;
            _FormMode = (enFormMode)FormMode;
            _LDLA = LDLA;
            SetTestTypeTile();
            TestTypeFees = clsTestType.FindTestTypeByTitle(TestTile).TestTypeFees;
            RetakeTestFees = clsApplicationTypes.FindApplicationTypeByID(2).ApplicationFees;
            TestTypeID = clsTestType.FindTestTypeByTitle(TestTile).TestTypeID;
            _TestAppointment = clsTestApointment.FindTestAppointment(_LDLA.LocalLicenseApplicationID, TestTypeID);
            _HandleEditingMode();
            if (TrialsCount < 0)
            {
                TrialCounter = 0;
            }
            else
            {
                TrialCounter = TrialsCount;
            }
        }


        private void SetValuesForTest()
        {
            lbAppID.Text = _LDLA.ApplicationID.ToString();
            lbClass.Text = _LDLA._LicenseClass.ClassName;
            lbName.Text = _LDLA.Person.FirstName + " " + _LDLA.Person.SecondName + " " + _LDLA.Person.ThirdName + " " + _LDLA.Person.LastName;
            lbFees.Text = TestTypeFees.ToString();
            lbTrial.Text = TrialCounter.ToString();
            dtpDate.Value = _LDLA.ApplicationDate;
            dtpDate.MinDate = DateTime.Now;
            dtpDate.MaxDate = DateTime.Now.AddYears(1);
            lbTotalFees.Text = (TestTypeFees + RetakeTestFees).ToString();
            if (_FormMode==enFormMode.eUpdate)
            {
                lbRtestAppID.Text = _TestAppointmentID.ToString();
            }
            else
            {
                lbRtestAppID.Text = "N/A";
            }
            lbRappFees.Text = RetakeTestFees.ToString();
        }
        private void ScheduelTest_Load(object sender, EventArgs e)
        {
            if (clsTests.CheckIfFailedBefore(_LDLA.LocalLicenseApplicationID , TestTypeID))
            {
             lbTestTitle.Text = "Scheduel Retake Test";
                gbRetakeTestInfo.Enabled = true;

            }
            else
            {
                lbTestTitle.Text = "Scheduel Test";
                gbRetakeTestInfo.Enabled = false;
            }
            SetValuesForTest();
        }
        private void AddNewTestAppointment()
        {
             _TestAppointment = new clsTestApointment();

            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLA.LocalLicenseApplicationID;
            _TestAppointment.IsLocked = 0;
            _TestAppointment.PaidFees = TestTypeFees;
            _TestAppointment.AppointmentDate = DateTime.Now;
            _TestAppointment.CreatedByUserID = _LDLA.User.UserID;
            _TestAppointment.TestTypeID = TestTypeID;
            
        }

        private void UpdateTestAppointment()
        {
           
            _TestAppointment.AppointmentDate = dtpDate.Value;
   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_FormMode==enFormMode.eAddNew)
            {
                AddNewTestAppointment();
            }
            else
            {
                UpdateTestAppointment();
            }
            if (_TestAppointment.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                lbRtestAppID.Text = _TestAppointment.TestAppointmentID.ToString();
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Saving Failed");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

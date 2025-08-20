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
using TestsBusinessLayer;
using TestTypesBusinessLayer;
using LoggedInUser;
using TestApointmentsBussinessLyer;
namespace Driver_Licence_Project
{
    public partial class frmTakeTest : Form
    {

        private int _TestAppointmentID;
        private clsLocalDrivingLicenseApplication _LDLA;
        private clsTestType testType;
        enum enSchedeulMode { eVision = 1, eWritten = 2, ePractical = 3 }
        enSchedeulMode _TestMode;
        
        clsTests Test = new clsTests();
        float TestTypeFees;
        string TestTitle;
        int TrialCounter = 0;
        private void SetTestTitle()
        {
            switch (_TestMode)
            {
                case enSchedeulMode.eVision:
                    lbTestTitle.Text = "Vision Test";
                    TestTitle = "Vision Test";
                    groupBox1.Text = TestTitle;
                    break;
                case enSchedeulMode.ePractical:
                    lbTestTitle.Text = "Practical Test";
                    TestTitle = "Practical (Street) Test";
                    groupBox1.Text = TestTitle;
                    break;
                case enSchedeulMode.eWritten:
                    lbTestTitle.Text = "Writing Test";
                    TestTitle = "Written (Theory) Test";
                    groupBox1.Text = TestTitle;
                    break;
            }
        }

        private void FillInfos()
        {
            lbAppID.Text = _LDLA.ApplicationID.ToString();
            lbClass.Text = _LDLA._LicenseClass.ClassName;
            lbName.Text = _LDLA.Person.FirstName + " " + _LDLA.Person.SecondName + " " + _LDLA.Person.ThirdName + " " + _LDLA.Person.LastName;
            lbFees.Text = TestTypeFees.ToString();
            lbTrial.Text = TrialCounter.ToString();
            lbDate.Text = _LDLA.ApplicationDate.ToShortDateString();
            rbPassed.Checked = true;
           
        }
        public frmTakeTest(clsLocalDrivingLicenseApplication LDLA,int TestAppointmentID , int TestType , int TrialsCount)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _LDLA = LDLA;
            _TestMode = (enSchedeulMode)TestType;
            SetTestTitle();
            TestTypeFees = clsTestType.FindTestTypeByTitle(TestTitle).TestTypeFees;
            if (TrialsCount<0)
            {
                TrialCounter = 0;
            }
            else
            {
             TrialCounter = TrialsCount;   
            }
            

        }

        private void FillTestInfos()
        {
            
            Test.TestAppointmentID = _TestAppointmentID;
            Test.TestApp = clsTestApointment.FindTestAppointmentByID(_TestAppointmentID);
            if (rbPassed.Checked)
            {
                Test.TestResult = 1;
            }
            else
            {
                Test.TestResult = 0;
            }
            Test.CreatedByUserID = clsCurrentUser.GlobalUser.UserID; 
            Test.Notes = txtNotes.Text;

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
          
            FillInfos();
            btnSave.Enabled = !clsTestApointment.CheckIfTstAppointmentIsLocked(_TestAppointmentID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FillTestInfos();
            if (MessageBox.Show("Are you sure you want to save , once you click on save you cannot change the results "
                , "Warning ",MessageBoxButtons.OKCancel , MessageBoxIcon.Warning )==DialogResult.OK)
            {
            if (Test.Save())
            {
                    MessageBox.Show("Saved Successfully");
                    lbTestID.Text = Test.TestID.ToString();
                    btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Saving Failed");
            }
            clsTestApointment.LockTestAppointment(_TestAppointmentID);
                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

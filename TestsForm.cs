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
    public partial class TestsForm : Form
    {
        private enum enTestTypes { eVision = 1 , eWritten = 2 , eStreet = 3 }
        private enTestTypes _TestTypeMode;
        private int _ApplicationID;
        private int _TestTypeID;
        private int _LocalDLA_ID;
        clsTestType _TestType = new clsTestType();
        private clsLocalDrivingLicenseApplication LDLA = new clsLocalDrivingLicenseApplication();
        private void ChooseTestType()
        {
            switch (_TestTypeMode)
            {
                case enTestTypes.eVision:
                    _TestTypeID = 1;
                    lbMainTitle.Text = "Vision Test";
                    break;
                case enTestTypes.eWritten:
                    _TestTypeID = 2;
                    lbMainTitle.Text = "Written Test";

                    break;
                case enTestTypes.eStreet:
                    _TestTypeID = 3;
                    lbMainTitle.Text = "Practical Test";
                    break;
            }

        }

        private void RefreshDGV()
        {
            DataTable dtAppointments = clsTestApointment.GetTestAppointment(ctrlDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplicationID , _TestType.TestTypeTitle);
            if (dtAppointments.Rows.Count>0)
            {
               
                dgvTestApointments.DataSource = dtAppointments.DefaultView.ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");
                dgvTestApointments.Columns[0].HeaderText = "TestAppointment ID";
                dgvTestApointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestApointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestApointments.Columns[3].HeaderText = "Is Locked";
            }
            
        }
        public TestsForm(int DLDA_ApplicationID , int TestTypeMode)
        {
            InitializeComponent();
            _TestTypeMode = (enTestTypes)TestTypeMode;
            _ApplicationID = DLDA_ApplicationID;
            ChooseTestType();
       
        }

        private void TestsForm_Load(object sender, EventArgs e)
        {
           
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseInfoByApplicationID(_ApplicationID);
            _TestType = clsTestType.FindTestTypeByID(_TestTypeID);
            _LocalDLA_ID = ctrlDrivingLicenseApplicationInfo1.LocalDrivingLicenseApplicationID;
            LDLA = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplicationByID(_LocalDLA_ID);
            RefreshDGV();
        }

        private void btnAddApointment_Click(object sender, EventArgs e)
        {
            if (clsTests.CheckIfPassedAnExamBefore(_LocalDLA_ID , _TestTypeID))
            {
                MessageBox.Show("Person already Passed this test , you can't have more appointments ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestApointment.CheckIfThereIsAnActiveApointment(_LocalDLA_ID, _TestTypeID)==true)
            {
                MessageBox.Show("Person already have an appointment for this test , choose an other one ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return; 
            }
            frmScheduelTest frm = new frmScheduelTest(LDLA, _TestTypeID , 1  ,  dgvTestApointments.Rows.Count - 1);
            frm.ShowDialog();
            RefreshDGV();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduelTest frm = new frmScheduelTest(LDLA, _TestTypeID, 2 , (int)dgvTestApointments.CurrentRow.Cells[0].Value  , dgvTestApointments.Rows.Count - 1);
            frm.ShowDialog();
            RefreshDGV();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmTakeTest frm = new frmTakeTest(LDLA, (int)dgvTestApointments.CurrentRow.Cells[0].Value, _TestTypeID, dgvTestApointments.Rows.Count -1);
            frm.ShowDialog();
            RefreshDGV();
        }

    }
}

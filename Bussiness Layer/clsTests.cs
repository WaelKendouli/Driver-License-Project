using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestApointmentsBussinessLyer;
using TestsDataAccessLayer; 

namespace TestsBusinessLayer
{
    public class clsTests 
    {
        enum enMode { eAddNew = 1 , eUpdate = 2 }
        enMode _Mode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public short TestResult { get; set; }
        public clsTestApointment TestApp;
        public string Notes { get; set; }
      public int  CreatedByUserID { get; set; }

        public clsTests()
        {
            this._Mode = enMode.eAddNew;
            this.TestID = -1;
            this.TestAppointmentID = -1;
            TestApp = new clsTestApointment();
            this.TestResult = 0;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }
        private clsTests(int TestID , int TestAppointmentID , short TestResult , string Notes , int CreatedByUserID)
        {
            this._Mode = enMode.eUpdate;
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            TestApp = clsTestApointment.FindTestAppointmentByID(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }

        public static clsTests FindTest(int TestAppointmentID)
        {
           short TestResult = 0;
           string Notes = "";
           int CreatedByUserID = -1;
            int TestID = clsTestsDataAccess.FindTest(ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID);

            if (TestID!=-1)
            {
                return new clsTests(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
            {
                return null;
            }

        }

        public static bool CheckIfPassedAnExamBefore(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestsDataAccess.CheckIfPassedAnExamBefore(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool CheckIfFailedBefore(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestsDataAccess.CheckIfFailedBefore(LocalDrivingLicenseApplicationID, TestTypeID);
        }


        private  bool _AddNew()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, 
                this.TestResult, this.Notes, this.CreatedByUserID);
            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestsDataAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    return _AddNew();

                case enMode.eUpdate:
                    return _UpdateTest();
                    

            }
            return false;
        }



    }
}

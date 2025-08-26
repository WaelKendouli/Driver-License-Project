using LocalDLBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApointmentsDataAccess;
using TestTypesBusinessLayer;
using UsersBussinessLayer;
namespace TestApointmentsBussinessLyer
{
    public class clsTestApointment
    {

        private enum enMode { eAdd=1 , eUpdate=2}
        private enMode _Mode;
        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public clsTestType TestType;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public clsLocalDrivingLicenseApplication LDLA;
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser User;
        public short IsLocked { get; set; }

        public clsTestApointment()
        {
            _Mode = enMode.eAdd;
            TestAppointmentID = -1;
            this.TestTypeID = -1;
            TestType = new clsTestType();
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            User = new clsUser();
            this.IsLocked = 0;
        }

        private clsTestApointment(int TestAppointmentID , int TestTypeID , int LocalDrivingLicenseApplicationID,
           DateTime AppointmentDate , float PaidFees , int CreatedByUserID , short IsLocked)
        {
            _Mode = enMode.eUpdate;
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            TestType = clsTestType.FindTestTypeByID(TestTypeID);
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            User = clsUser.FindUserByID(CreatedByUserID);
            this.IsLocked = IsLocked;
        }

        public static clsTestApointment FindTestAppointment(int LocalDrivingLicenseApplicationID , int TestTypeID)
        {
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0; int CreatedByUserID = 0; short IsLocked = 0;
            int TestAppointmentID = clsTestApointmentsDataAccess.FindTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID, ref AppointmentDate,
                ref PaidFees, ref CreatedByUserID, ref IsLocked);
            if (TestAppointmentID!=-1)
            {
                return new clsTestApointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            else
            {
                return null;
            }
        }


        public static clsTestApointment FindTestAppointmentByID(int TestAppointmentID)
        {
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0; int CreatedByUserID = 0; short IsLocked = 0;
            int LocalDrivingLicenseApplicationID = -1; int TestTypeID = 0;
            if (clsTestApointmentsDataAccess.FindTestAppointmentByID(TestAppointmentID ,ref TestTypeID ,ref LocalDrivingLicenseApplicationID ,
                ref AppointmentDate , ref PaidFees ,
                ref CreatedByUserID  , ref IsLocked))
            {
                return new clsTestApointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            }
            else
            {
                return null;
            }
        }

        public static bool LockTestAppointment(int TestAppointmentID)
        {
            return clsTestApointmentsDataAccess.LockTestAppointment(TestAppointmentID);
        }

        private  bool _AddNewAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
           DateTime AppointmentDate, float PaidFees, int CreatedByUserID, short IsLocked)
        {
             this.TestAppointmentID = clsTestApointmentsDataAccess.AddNewAppointment(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            return (this.TestAppointmentID != -1);

        }
        public static DataTable GetTestAppointment(int LDLA_ID , string TestTypeTitle)
        {
            return clsTestApointmentsDataAccess.GetTestApointments(LDLA_ID , TestTypeTitle);
        }

        public static bool CheckIfThereIsAnActiveApointment(int LocalDrivingLicenseApplicationID , int TestTypeID)
        {
          return  clsTestApointmentsDataAccess.CheckIfThereIsAnActiveApointment(LocalDrivingLicenseApplicationID, TestTypeID);

        }

        public static bool CheckIfTstAppointmentIsLocked(int TestAppointmentID)
        {
            return clsTestApointmentsDataAccess.CheckIfTestAppointmentIsLocked(TestAppointmentID);
        }

        private static bool _UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
           DateTime AppointmentDate, float PaidFees, int CreatedByUserID, short IsLocked)
        {
            return clsTestApointmentsDataAccess.UpdateTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
            AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
        }

        public bool Save()
        {
            if (_Mode==enMode.eAdd)
            {
                _Mode = enMode.eUpdate;
                return _AddNewAppointment(this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                    this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);

            }
            else
            {
                return _UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID,
                    this.LocalDrivingLicenseApplicationID,
                    this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked);
            }
        }

    }
}

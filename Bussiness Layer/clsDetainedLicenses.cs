using ApplicationsBussinessLayer;
using ApplicationTypesBussinessLayer;
using DetainedLicensesDataAccess;
using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;

namespace DetainedLicensesBussiness
{
    public class clsDetainedLicenses : clsApplication
    {
      public  enum enStatus
        {
            eDetain = 1 , eRelease = 2
        }
        public  enStatus _DetainedLicenseStatus { get; set; }

      public int DetainID { get; set; }
      public  int LicenseID { get; set; }
      public  DateTime DetainDate { get; set; }
      public  float FineFees { get; set; }
      public  int CreatedByUserID { get; set; }
      public  short IsReleased { get; set; }
      public  DateTime ReleaseDate { get; set; }
      public  int ReleasedByUserID { get; set; }
      public  int ReleaseApplicationID { get; set; }


        public clsDetainedLicenses(enStatus DetainStatus)
        {
            _Mode = enMode.eAddNew;
            this._DetainedLicenseStatus =DetainStatus;
            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationStatusNum = 1;
            ChangeAppStatus();
            this.ApplicationTypeID = 0;
            this.AppType = new clsApplicationTypes();
            this.ApplicantPersonID = -1;
            this.Person = new clsPerson();
            this.PaidFees = 0;
            base.CreatedByUserID = -1;
            this.User = new clsUser();
            this.LastStatusDate = DateTime.Now;
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
             this.IsReleased = 0;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
        }

        private clsDetainedLicenses(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, DateTime LastStatusDate,
            int ApplicationTypeID, short ApplicationStatusNum, float PaidFees, int CreatedByUserID , int LicenseID,  int DetainID,  DateTime DetainDate,
            float FineFees,  short IsReleased,
            DateTime ReleaseDate,  int ReleasedByUserID,  int ReleaseApplicationID)
        {
            _Mode = enMode.eUpdate;
            this.ApplicationID = ApplicationID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationStatusNum = ApplicationStatusNum;
            ChangeAppStatus();
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;
            this.LastStatusDate = LastStatusDate;
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }

        private clsDetainedLicenses(int CreatedByUserID, int LicenseID, int DetainID, DateTime DetainDate,
            float FineFees, short IsReleased,
            DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this._Mode = enMode.eAddNew;
            this._DetainedLicenseStatus = enStatus.eRelease;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseID = LicenseID;
            this.DetainID = DetainID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
        }
        public static bool isLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesDataAccess.isLicenseDetained(LicenseID);
        }

        private  bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicence(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID,
                this.IsReleased, this.ReleaseDate,
                this.ReleasedByUserID, this.ReleaseApplicationID);

            return (DetainID != -1);
        }
        public static clsDetainedLicenses FindDetainedLicenseByLicenseID(int LicenseID)
        {
             int DetainID = 1;  DateTime DetainDate = DateTime.Now;
            float FineFees = 0;  int CreatedByUserID = -1;  short IsReleased = 0;
             DateTime ReleaseDate = DateTime.Now;  int ReleasedByUserID = -1;  int ReleaseApplicationID = -1;


            if (clsDetainedLicensesDataAccess.FindDetainedLicenseByLicenseID(LicenseID , ref DetainID ,ref DetainDate ,ref FineFees
                ,ref CreatedByUserID ,ref IsReleased ,ref ReleaseDate ,ref ReleasedByUserID ,ref ReleaseApplicationID ))
            {
                return new clsDetainedLicenses(CreatedByUserID, LicenseID, DetainID, DetainDate,
             FineFees, IsReleased,
             ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        private bool _ReleaseLicense(int DetainID, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {

            if (_DetainedLicenseStatus==enStatus.eDetain||isLicenseDetained(this.LicenseID)==false)
            {
                return false;
            }
            base._Mode = (clsApplication.enMode)this._Mode;
            base.CreatedByUserID = ReleasedByUserID;
            if (base.Save())
            {
                ReleaseApplicationID = base.ApplicationID;
                return clsDetainedLicensesDataAccess.ReleaseLicense(DetainID, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }
            else
            {
                return false;
            }
        }

        public bool isLicenseDetainedByDetainID()
        {
            return clsDetainedLicensesDataAccess.isLicenseDetainedByDetainID(this.LicenseID, this.DetainID);
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }

        public bool Save()
        {
            switch (this._DetainedLicenseStatus)
            {
                case enStatus.eDetain:
                    return _AddNewDetainedLicense();
                    
                case enStatus.eRelease:
                    return _ReleaseLicense(this.DetainID, this.ReleaseDate, this.ReleasedByUserID, this.ReleaseApplicationID);
                    
            }
            return false;
        }


    }
}

using DriversBussinesLayer;
using LicencesClassesBussinessLayer;
using LicencesDataAccessClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;

namespace LicenseBussinessLayer
{
    public class clsLicense
    {

        enum enMode { eAddNew , eUpdate}
        enMode _Mode;
     public   enum  enIssueReason
        {
            FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4
        }
        public  int LicenseID { get; set; }
      public  int ApplicationID { get; set; }
      public  int DriverID { get; set; }
      public clsDrivers driver;
      public  int LicenseClass { get; set; }
      public clsLicencesClasses _LicenseClass;
      public  DateTime IssueDate { get; set; }
      public  DateTime ExpirationDate { get; set; }
      public  string Notes { get; set; }
      public  float PaidFees { get; set; }
      public  short IsActive { get; set; }
      public  enIssueReason IssueReason { get; set; }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText();
            }
        }

      public  int CreatedByUserID { get; set; }
      public clsUser User;

        public string GetIssueReasonText()
        {
            string IssueReasonText = "";
            switch (this.IssueReason)
            {
                case enIssueReason.FirstTime:
                    IssueReasonText = "First Time";
                    break;
                case enIssueReason.Renew:
                    IssueReasonText = "Renew";
                    break;
                case enIssueReason.DamagedReplacement:
                    IssueReasonText = "Damaged Replacement";
                    break;
                case enIssueReason.LostReplacement:
                    IssueReasonText = "Lost Replacement";
                    break;
            }
            return IssueReasonText;
        }
        public clsLicense()
        {
            _Mode = enMode.eAddNew;
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            driver = new clsDrivers();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = 0;
            this.IssueReason = 0;
            this.CreatedByUserID = 0;
            _LicenseClass = new clsLicencesClasses();
            User = new clsUser();
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes
            , float PaidFees, short IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            _Mode = enMode.eUpdate;
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            driver = clsDrivers.FindDriver(DriverID);
            this.IssueDate =IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            _LicenseClass = clsLicencesClasses.FindLicenceByID(LicenseClass);
            User =clsUser.FindUserByID(CreatedByUserID);
        }

        public static clsLicense FindLicenseByID(int LicenseID)
        {
             int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now; string Notes = "";
            float PaidFees = 0; short IsActive = 0; short IssueReason = 0; int CreatedByUserID = 0;
            if (clsLicenceDataAccess.FindLicenseByID(LicenseID ,ref ApplicationID ,ref DriverID ,
                ref LicenseClass ,ref IssueDate ,
                ref ExpirationDate ,ref Notes ,ref PaidFees ,ref IsActive ,
                ref IssueReason ,ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees,
                    IsActive, (enIssueReason) IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsLicense FindLicenseByApplicationID(int ApplicationID)
        {
           int LicenseID  = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now; string Notes = "";
            float PaidFees = 0; short IsActive = 0; short IssueReason = 0; int CreatedByUserID = 0;
            if (clsLicenceDataAccess.FindLicenseByApplicationID(ref LicenseID,  ApplicationID, ref DriverID,
                ref LicenseClass, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive,
                ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees,
                    IsActive,(enIssueReason) IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool isThisLicenseActive()
        {
            return (this.IsActive == 1);
        }

        public static clsLicense FindLicenseByDriverID(int DriverID)
        {
            int LicenseID = -1; int ApplicationID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now; string Notes = "";
            float PaidFees = 0; short IsActive = 0; short IssueReason = 0; int CreatedByUserID = 0;
            if (clsLicenceDataAccess.FindLicenseByDriverID(ref LicenseID,ref ApplicationID,  DriverID,
                ref LicenseClass, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive,
                ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees,
                    IsActive,(enIssueReason) IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        public bool DisactivateLicense()
        {
            return clsLicenceDataAccess.DisactivateLicense(this.LicenseID);
        }

        public bool IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }
        public static bool DeleteLicenseByID(int LicenseID)
        {
            return clsLicenceDataAccess.DeleteLicenseByID(LicenseID);
        }

        private  bool _AddNewLicense()
        {
            if (isLicenseExist(this.ApplicationID))
            {
                return false;
            }
            this.LicenseID = clsLicenceDataAccess.AddNewLisenceClass(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(short) this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenceDataAccess.UpdateLicence(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,(short) this.IssueReason, this.CreatedByUserID);
        }
        public static bool isLicenseExist(int ApplicationID)
        {
            return clsLicenceDataAccess.isLicenseExist(ApplicationID);
        }

        
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    _Mode = enMode.eUpdate;
                    return _AddNewLicense();
                    
                case enMode.eUpdate:
                    return _UpdateLicense();
            }
            return false;
        }
    }
}

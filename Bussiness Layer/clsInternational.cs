using ApplicationsBussinessLayer;
using DriversBussinesLayer;
using InternationalLicensesDataAccess;
using LicenseBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;

namespace InternationalLicensesBussiness
{
    public class clsInternational
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication application;
        public clsDrivers drivers;
       public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }

        public clsLicense License;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public short IsActive { get; set; }

        public int CreatedByUserID { get; set; }
        public clsUser User;

        public clsInternational()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.ExpirationDate = DateTime.Now;
            this.IssueDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.DriverID = -1;
            this.IsActive = 0;
            this.IssuedUsingLocalLicenseID = -1;
            this.drivers = new clsDrivers();
            this.License = new clsLicense();
            this.application = new clsApplication();
            this.User = new clsUser();
        }

        private clsInternational(int InternationalLicenseID,  int ApplicationID,  int DriverID,
           int IssuedUsingLocalLicenseID,  DateTime IssueDate,
            DateTime ExpirationDate,  short IsActive,  int CreatedByUserID)
        {

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.ExpirationDate = ExpirationDate;
            this.IssueDate = IssueDate;
            this.CreatedByUserID = CreatedByUserID;
            this.DriverID = DriverID;
            this.IsActive = IsActive;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.drivers = clsDrivers.FindDriver(DriverID);
            this.License = clsLicense.FindLicenseByID(IssuedUsingLocalLicenseID);
            this.application = clsApplication.FindApplicationByID(ApplicationID);
            this.User = clsUser.FindUserByID(CreatedByUserID);
            
        }

        public static clsInternational FindLicenseByID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1,
          IssuedUsingLocalLicenseID = -1; DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now; short IsActive = 0; int CreatedByUserID = -1;
            if (clsInternationalDataAccess.FindLicenseByID(LicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternational(LicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsInternational FindLicenseByLocalLicenseID(int IssuedUsingLocalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1,
          InternationalLicenseID = -1; DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now; short IsActive = 0; int CreatedByUserID = -1;
            if (clsInternationalDataAccess.FindLicenseByLocalLicenseID( ref InternationalLicenseID, ref ApplicationID, ref DriverID,  IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternational(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static bool isLicenseExist(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalDataAccess.IsLicenseExist(IssuedUsingLocalLicenseID);
        }

        public bool isLicenseExpired()
        {
            return (DateTime.Now > this.ExpirationDate);
        }


        public static DataTable GetListInternationalLicenses()
        {
            return clsInternationalDataAccess.GetAllInternationalDrivingLicencesData();
        }

        private bool AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalDataAccess.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate
                , this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        public bool Save()
        {
            return AddNewInternationalLicense();
        }



    }
}

using ApplicationsDataAccess;
using ApplicationTypesBussinessLayer;
using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace ApplicationsBussinessLayer
{
    public class clsApplication
    {
      protected  enum enMode { eAddNew , eUpdate }
       protected enMode _Mode = enMode.eAddNew;
      public  enum eApplicationStatus { eNewApp = 1 , eCanceled = 2 , eCompleted = 3 }
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson Person ;
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationTypes AppType ; 
        private eApplicationStatus ApplicationStatus { get; set; }
        public short ApplicationStatusNum { get; set; } 
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser User ;

        protected void ChangeAppStatus()
        {
            if (this.ApplicationStatusNum == 1)
            {
                this.ApplicationStatus = eApplicationStatus.eNewApp;
            }
            else if  (ApplicationStatusNum == 2)
            {
                this.ApplicationStatus = eApplicationStatus.eCanceled;
            }
            else
            {
                this.ApplicationStatus = eApplicationStatus.eCompleted;
            }
        }
        public clsApplication()
        {
            _Mode = enMode.eAddNew;
            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationStatusNum = 1;
            ChangeAppStatus();
            this.ApplicationTypeID = 0;
            this.AppType = new clsApplicationTypes();
            this.ApplicantPersonID = -1;
            this.Person = new clsPerson();
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.User = new clsUser();
            this.LastStatusDate = DateTime.Now;
          
        }

        public clsApplication(int ApplicationID,int ApplicantPersonID , DateTime ApplicationDate ,DateTime LastStatusDate ,
            int ApplicationTypeID , short ApplicationStatusNum , float PaidFees ,int CreatedByUserID )
        {
            _Mode = enMode.eUpdate;
            this.ApplicationID = ApplicationID; ;
            this.ApplicationDate =ApplicationDate;
            this.ApplicationStatusNum = ApplicationStatusNum;
            ChangeAppStatus();
            this.ApplicationTypeID = ApplicationTypeID;
            this.AppType = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
            this.ApplicantPersonID = ApplicantPersonID;
            this.Person = clsPerson.FindPersonByID(ApplicantPersonID);
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.User = clsUser.FindUserByID(ApplicationTypeID);
            this.LastStatusDate = LastStatusDate;
        }

        public static DataTable GetAllLocalLicencesData()
        {
            return clsApplicationsDataAccess.GetAllLocalDrivingLicencesData();
        }

        private static bool _UpdateApplication(int ApplicationID , clsApplication app)
        {
            return clsApplicationsDataAccess.UpdateApplication(ApplicationID, ref app);
        }
        public static clsApplication FindApplicationByPersonID(int ApplicantPersonID)
        { 
             int ApplicationID = 0;  DateTime ApplicationDate = new DateTime(), LastStatusDate = new DateTime();
            int ApplicationTypeID = 0; short ApplicationStatusNum = 0; float PaidFees = 0; int CreatedByUserID = -1;

            if (clsApplicationsDataAccess.FindApplicationByPersonID(ApplicantPersonID,ref ApplicationID,ref ApplicationDate ,ref LastStatusDate ,ref ApplicationTypeID ,ref ApplicationStatusNum ,ref PaidFees ,ref CreatedByUserID ))
            {
                return new clsApplication(ApplicationID , ApplicantPersonID , ApplicationDate , LastStatusDate , ApplicationTypeID , ApplicationStatusNum , PaidFees , CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static clsApplication FindApplicationByID(int ApplicationID )
        {
            int ApplicantPersonID = 0; DateTime ApplicationDate = new DateTime(), LastStatusDate = new DateTime();
            int ApplicationTypeID = 0; short ApplicationStatusNum = 0; float PaidFees = 0; int CreatedByUserID = -1;

            if (clsApplicationsDataAccess.FindApplicationByID(ref ApplicantPersonID, ApplicationID, ref ApplicationDate, ref LastStatusDate, ref ApplicationTypeID, ref ApplicationStatusNum, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, LastStatusDate, ApplicationTypeID, ApplicationStatusNum, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }


        private  bool _AddNew()
        {
             ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatusNum,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (ApplicationID != -1);
        }


        public static bool DoesThisPersonHasLicenseInThisClass(int PersonID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.DoesPersonHasLisenceinThisClass(PersonID, LicenseClassID);
        }


        public static bool isApplicationNotValid(int ApplicantPersonID, int LicenseClassID , int ApplicationTypeID)
        {
            return clsApplicationsDataAccess.isApplicationNotValid(ApplicantPersonID, LicenseClassID , ApplicationTypeID);
        }

        public static bool SwitchAppStatusToCanceled(int ApplicationID)
        {
            return clsApplicationsDataAccess.UpdateApplicationStatusToCanceled(ApplicationID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsDataAccess.DeleteApplicationByID(ApplicationID);
        }
        public static bool SwitchAppStatusToCompleted(int ApplicationID)
        {
            return clsApplicationsDataAccess.UpdateApplicationStatusToCompleted(ApplicationID);
        }

        public bool Save()
        {
            if (_Mode==enMode.eAddNew)
            {
               return _AddNew();
            }
            else
            {
                return _UpdateApplication(this.ApplicationID, this);
            }
        }


    }
}

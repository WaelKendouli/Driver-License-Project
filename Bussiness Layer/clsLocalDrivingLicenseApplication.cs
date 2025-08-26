using LocalDrivingLicenseApplicationsDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationsBussinessLayer;
using LicencesClassesBussinessLayer;
using System.Runtime.CompilerServices;
using UsersBussinessLayer;
using PeopleBussinessLayer;
using ApplicationTypesBussinessLayer;
namespace LocalDLBussinessLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        enum enMode
        {
            eAddNew , eUpdate
        }
        private enMode _Mode;
        public int LocalLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicencesClasses _LicenseClass;

        public clsLocalDrivingLicenseApplication()
        {
            LocalLicenseApplicationID = 0;
            LicenseClassID = -1;
            _LicenseClass = new clsLicencesClasses();
            _Mode = enMode.eAddNew;
        }

        private clsLocalDrivingLicenseApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, DateTime LastStatusDate,
            int ApplicationTypeID, short ApplicationStatusNum,
            float PaidFees, int CreatedByUserID , int LocalLicenseApplicationID , int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.LocalLicenseApplicationID = LocalLicenseApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.Person = clsPerson.FindPersonByID(ApplicantPersonID);
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.AppType = clsApplicationTypes.FindApplicationTypeByID(ApplicationTypeID);
            this.ApplicationStatusNum = ApplicationStatusNum;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.User = clsUser.FindUserByID(CreatedByUserID);
            this.LicenseClassID = LicenseClassID;
            _LicenseClass = clsLicencesClasses.FindLicenceByID(LicenseClassID);
            _Mode = enMode.eUpdate;            
        }
        private  bool _AddNewLocalDLAppliction()
        {
            this.LocalLicenseApplicationID = clsLocalLicenceDataAccess.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
            return (LocalLicenseApplicationID != -1);
        }
        private bool _UpdateLocalDLApplication()
        {
            return clsLocalLicenceDataAccess.UpdateLocalDrivingLicenseApplication(this.LocalLicenseApplicationID,this.ApplicationID,this.LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID)
        {
             int ApplicationID= 0, LicenseClassID = 0;
            if (clsLocalLicenceDataAccess.GetLocalDrivingLicenseByID(LocalDrivingLicenseApplicationID,ref ApplicationID, ref LicenseClassID))
            {
                clsApplication App = clsApplication.FindApplicationByID(ApplicationID);
                return new clsLocalDrivingLicenseApplication(ApplicationID, App.ApplicantPersonID, App.ApplicationDate
                    , App.LastStatusDate, App.ApplicationTypeID, App.ApplicationStatusNum, App.PaidFees, App.CreatedByUserID,
                    LocalDrivingLicenseApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }
        }
        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplicationByApplicationID(int ApplicationID)

        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool isFound = clsLocalLicenceDataAccess.GetLocalDrivingLicenseByApplicationID(ref LocalDrivingLicenseApplicationID, ApplicationID, ref LicenseClassID);

            if (isFound)
            {
                clsApplication App = clsApplication.FindApplicationByID(ApplicationID);

                return new clsLocalDrivingLicenseApplication(ApplicationID, App.ApplicantPersonID, App.ApplicationDate
                    , App.LastStatusDate, App.ApplicationTypeID, App.ApplicationStatusNum, App.PaidFees, App.CreatedByUserID,
                    LocalDrivingLicenseApplicationID, LicenseClassID);
            }
            else
            {
                return null;
            }

        }

        public static bool DeleteLDLA(int ApplicationID)
        {
            return clsLocalLicenceDataAccess.DeleteLDLA(ApplicationID);
        }

        public static bool IsPassedThisTestBefore(int ApplicationID, int TestTypeID)
        {
            return clsLocalLicenceDataAccess.IsPassedThisTestBefore(ApplicationID, TestTypeID);
        }

        public bool Save()
        {
            base._Mode = (clsApplication.enMode)this._Mode;
            if (!base.Save())
            {
                return false;
            }

            if (this._Mode==enMode.eAddNew)
            {
                this._Mode = enMode.eUpdate;
                return _AddNewLocalDLAppliction();
            }
            else
            {
                return _UpdateLocalDLApplication();
            }
        }

    }
}

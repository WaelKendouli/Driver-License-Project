using LicenceClassesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicencesClassesBussinessLayer
{
    public class clsLicencesClasses
    {
        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicencesClasses()
        {
            this.LicenseClassID = 0;
            this.DefaultValidityLength = 0;
            this.ClassDescription = "";
            this.ClassName = "";
            this.MinimumAllowedAge = 0;
            this.ClassFees = 0;
        }
        private clsLicencesClasses(int LicenseClassID , string ClassName ,
            string ClassDescription , int MinimumAllowedAge , int DefaultValidityLength , float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            this.MinimumAllowedAge = MinimumAllowedAge;

        }

        public static clsLicencesClasses FindLicenceByClassName(string ClassName)
        {
            int LicenseClassID = 0;
            string ClassDescription = ""; int MinimumAllowedAge = 0, DefaultValidityLength = 0; float ClassFees = 0;
            if (clsLicencesClassesDataAccess.FindLisenceClassByClassName(ref LicenseClassID,ClassName ,ref ClassDescription,ref MinimumAllowedAge , ref DefaultValidityLength ,ref ClassFees ))
            {
                return new clsLicencesClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }

        }

        public static clsLicencesClasses FindLicenceByID(int LicenseClassID)
        {
            string ClassName = "";
            string ClassDescription = ""; int MinimumAllowedAge = 0, DefaultValidityLength = 0; float ClassFees = 0;
            if (clsLicencesClassesDataAccess.FindLisenceClassByID( LicenseClassID,ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicencesClasses(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }

        }

        public static DataTable GetAllLicensesClasses()
        {
            return clsLicencesClassesDataAccess.GetAllClasses();
        }


    }
}

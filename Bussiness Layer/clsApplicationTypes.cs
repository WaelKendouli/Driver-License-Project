using ApplicationTypesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTypesBussinessLayer
{
    public class clsApplicationTypes
    {
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public double ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0;
        }
        public clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle , double ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.GetAllApplicationTypes();
        }

        public bool UpdateApplicationType()
        {
            return (clsApplicationTypesDataAccess.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees));
            
        }
        public static clsApplicationTypes FindApplicationTypeByID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = ""; double ApplicationFees = 0;

            if (clsApplicationTypesDataAccess.FindApplicationTypeByID(ApplicationTypeID , ref ApplicationTypeTitle ,ref ApplicationFees))
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }
        public static clsApplicationTypes FindApplicationTypeByTitle(string ApplicationTypeTitle)
        {
          int ApplicationTypeID   = 0; double ApplicationFees = 0;

            if (clsApplicationTypesDataAccess.FindApplicationTypeByTitle(ref ApplicationTypeID,  ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
            {
                return null;
            }
        }

    }
}

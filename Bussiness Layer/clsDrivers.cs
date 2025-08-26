using DriversDataAccess;
using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;

namespace DriversBussinesLayer
{
    public class clsDrivers
    {

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson Person;
       public int CreatedByUserID { get; set; }
        public clsUser User;
       public DateTime CreatedDate { get; set; }

        public clsDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
             Person = new clsPerson();
            this.CreatedByUserID = 0;
             User = new clsUser();
            this.CreatedDate = DateTime.Now;
        }

        private clsDrivers(int DriverID, int PersonID, int CreatedByUserID,
        DateTime CreatedDate)
        {
            this.DriverID =DriverID;
            this.PersonID = PersonID;
            Person = clsPerson.FindPersonByID(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            User = clsUser.FindUserByID(CreatedByUserID);
            this.CreatedDate = DateTime.Now;
        }

        public static clsDrivers FindDriver(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriversDataAccess.FindDriver(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }

        public static clsDrivers FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (clsDriversDataAccess.FindDriverByPersonID(ref DriverID, PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDrivers(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            return null;
        }

        private bool _AddNew()
        {
            if (IsPersonExistsAsDriver(this.PersonID))
            {
                return false;
            }
            this.DriverID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }

        public static bool IsPersonExistsAsDriver(int PersonID)
        {
            return clsDriversDataAccess.isPersonExistsAsDriver(PersonID);
        }

        public static DataTable GetListOfDrivers()
        {
            return clsDriversDataAccess.GetDriverList();
        }


        public bool Save()
        {
            return _AddNew();
        }
    }
}

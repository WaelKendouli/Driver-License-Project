using LicenseHistoryDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisenceHistoryBussinessLayer
{
    public class clsLicenseHistory
    {
        public static DataTable GetLicenseHistory(int PersonID)
        {
            return clsLicenseHistoryDataAccess.GetLisenceHistory(PersonID);
        }

        public static DataTable GetInternationalLicenseHistory(int PersonID)
        {
            return clsLicenseHistoryDataAccess.GetInternationalLicenseHistory(PersonID);
        }
    }
}

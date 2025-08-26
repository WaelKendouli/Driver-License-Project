using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
namespace LicenseHistoryDataAccess
{
    public class clsLicenseHistoryDataAccess
    {
        public static DataTable GetLisenceHistory(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"SELECT Licenses.LicenseID , Licenses.ApplicationID , LicenseClasses.ClassName , Licenses.IssueDate , Licenses.ExpirationDate , Licenses.IsActive FROM Drivers
inner join Licenses on Licenses.DriverID = Drivers.DriverID
inner join LicenseClasses on LicenseClasses.LicenseClassID = Licenses.LicenseClass
inner join People on People.PersonID = Drivers.PersonID
Where People.PersonID = @PersonID";
           
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    reader.Close();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetInternationalLicenseHistory(int PersonID)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"select InternationalLicenses.InternationalLicenseID, InternationalLicenses.ApplicationID, InternationalLicenses.IssuedUsingLocalLicenseID, InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate , InternationalLicenses.IsActive from InternationalLicenses
inner join Applications on InternationalLicenses.ApplicationID = Applications.ApplicationID
inner join People on Applications.ApplicantPersonID = People.PersonID
where People.PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    reader.Close();
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

    }
}

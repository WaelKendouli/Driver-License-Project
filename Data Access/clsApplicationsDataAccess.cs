using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
using ApplicationsBussinessLayer;
using static System.Net.Mime.MediaTypeNames;
namespace ApplicationsDataAccess
{
    public class clsApplicationsDataAccess
    {
        public static DataTable GetAllLocalDrivingLicencesData()
        {

            DataTable infos = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"SELECT 
  Applications.ApplicationID ,
LicenseClasses.ClassName ,	People.NationalNo ,
    ISNULL(FirstName, '') + ' ' + 
    ISNULL(SecondName, '') + ' ' + 
    ISNULL(ThirdName, '') + ' ' + 
    ISNULL(LastName, '') AS Fullname ,  ApplicationDate 
   , PassedExams = (
        SELECT COUNT(Tests.TestResult)
        FROM Tests 
        INNER JOIN TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
        INNER JOIN LocalDrivingLicenseApplications l ON l.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
        WHERE l.ApplicationID = Applications.ApplicationID
        AND TestResult = 1
    )
	, Status = 
	case
	when	ApplicationStatus = 1 then 'New'
	when    ApplicationStatus = 2 then 'Canceled'
	when    ApplicationStatus = 3 then 'Completed'
	end
FROM 
    People inner join Applications on ApplicantPersonID = People.PersonID
	inner join LocalDrivingLicenseApplications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
	inner join LicenseClasses on LicenseClasses.LicenseClassID = LocalDrivingLicenseApplications.LicenseClassID
Order by Applications.ApplicationID desc";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    infos.Load(reader);
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
            return infos;

        }

        public static bool UpdateApplication(int ApplicationID, ref clsApplication application)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[Applications]
   SET [ApplicantPersonID] = @ApplicantPersonID
      ,[ApplicationDate] = @ApplicationDate
      ,[ApplicationTypeID] = @ApplicationTypeID
      ,[ApplicationStatus] = @ApplicationStatus
      ,[LastStatusDate] = @LastStatusDate
      ,[PaidFees] = @PaidFees
      ,[CreatedByUserID] = @CreatedByUserID
 WHERE ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", application.ApplicationID);
            Command.Parameters.AddWithValue("@ApplicantPersonID", application.ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", application.ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStatus", application.ApplicationStatusNum);
            Command.Parameters.AddWithValue("@LastStatusDate", application.LastStatusDate);
            Command.Parameters.AddWithValue("@CreatedByUserID", application.CreatedByUserID);
            Command.Parameters.AddWithValue("@PaidFees", application.PaidFees);



            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();

                if (RowsEffected > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;
        }
        //'Incorrect syntax near ';'.'

        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
             int ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[Applications]
           ([ApplicantPersonID]
           ,[ApplicationDate]
           ,[ApplicationTypeID]
           ,[ApplicationStatus]
           ,[LastStatusDate]
           ,[PaidFees]
           ,[CreatedByUserID])
     VALUES
           (@ApplicantPersonID
           ,@ApplicationDate
           ,@ApplicationTypeID
           ,@ApplicationStatus
           ,@LastStatusDate
           ,@PaidFees
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationID = insertedID;
                }
                else
                {
                    ApplicationID = -1;
                }

            }
            catch (Exception ex)
            {
                ApplicationID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return ApplicationID;


        }

        public static bool DeleteApplicationByID(int ApplicationID)
        {
            bool isDeleted = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"delete from Applications
where ApplicationID = @ApplicationID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                Connection.Open();
                int rowsEffected = Command.ExecuteNonQuery();
                if (rowsEffected > 0)
                {
                    isDeleted = true;
                }

            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                Connection.Close();
            }
            return isDeleted;
        }
        public static bool DoesPersonHasLisenceinThisClass(int PersonID , int LicenseClassID)
        {
            bool isExsist = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select W=10 from Licenses 
inner join LicenseClasses on LicenseClasses.LicenseClassID = Licenses.LicenseClass
inner join Drivers on Drivers.DriverID = Licenses.DriverID
Where Drivers.PersonID = @PersonID and LicenseClasses.LicenseClassID = @LicenseClassID
";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExsist = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isExsist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExsist;
        }

        public static bool UpdateApplicationStatusToCompleted(int ApplicationID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"update Applications
set ApplicationStatus = 3
,LastStatusDate = @LastStatusDate
where ApplicationID = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();

                if (RowsEffected > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;


        }


        public static bool UpdateApplicationStatusToCanceled(int ApplicationID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"update Applications
set ApplicationStatus = 2
,LastStatusDate = @LastStatusDate
where ApplicationID = @ApplicationID ";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                Connection.Open();
                int RowsEffected = Command.ExecuteNonQuery();

                if (RowsEffected > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch (Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                Connection.Close();
            }
            return isUpdated;


        }



        public static bool FindApplicationByPersonID(int ApplicantPersonID ,ref int ApplicationID,
            ref DateTime ApplicationDate,ref DateTime LastStatusDate,
           ref int ApplicationTypeID,ref short ApplicationStatusNum,ref float PaidFees,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "SELECT * FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatusNum = Convert.ToInt16( reader["ApplicationStatus"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);



                    

                    reader.Close();
                }
                else
                {
                    isFound = false;

                }

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {

                connection.Close();
            }
            return isFound;
        }


        public static bool FindApplicationByID(ref int ApplicantPersonID,  int ApplicationID,
            ref DateTime ApplicationDate, ref DateTime LastStatusDate,
           ref int ApplicationTypeID, ref short ApplicationStatusNum, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    ApplicantPersonID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatusNum = Convert.ToInt16(reader["ApplicationStatus"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);





                    reader.Close();
                }
                else
                {
                    isFound = false;

                }

            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {

                connection.Close();
            }
            return isFound;
        }


        public static bool isApplicationNotValid(int ApplicantPersonID , int LicenseClassID , int ApplicationTypeID)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select  a = 5 from Applications
inner join LocalDrivingLicenseApplications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
where ApplicantPersonID = @ApplicantPersonID and
LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
and ApplicationTypeID = @ApplicationTypeID
and ApplicationStatus =1
";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isAvailable = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isAvailable = false;
            }
            finally
            {
                connection.Close();
            }
            return isAvailable;
        }

    }
}

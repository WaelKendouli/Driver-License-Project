using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
using static System.Net.Mime.MediaTypeNames;
namespace InternationalLicensesDataAccess
{
    public class clsInternationalDataAccess
    {

        public static int AddNewInternationalLicense( int ApplicationID, int DriverID,
             int IssuedUsingLocalLicenseID, DateTime IssueDate,
             DateTime ExpirationDate, short IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@IssuedUsingLocalLicenseID
           ,@IssueDate
           ,@ExpirationDate
           ,@IsActive
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
                else
                {
                    InternationalLicenseID = -1;
                }

            }
            catch (Exception ex)
            {
                InternationalLicenseID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return InternationalLicenseID;


        }

        public static DataTable GetAllInternationalDrivingLicencesData()
        {

            DataTable infos = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"SELECT * FROM InternationalLicenses
order by InternationalLicenses.InternationalLicenseID desc";
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

        public static bool IsLicenseExist(int IssuedUsingLocalLicenseID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT B=0 FROM InternationalLicenses
where InternationalLicenses.IssuedUsingLocalLicenseID= @IssuedUsingLocalLicenseID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExists = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isExists = false;
            }
            finally
            {
                connection.Close();
            }
            return isExists;
        }


        public static bool DisactivateLicense(int InternationalLicenseID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"update InternationalLicenses
set IsActive = 0
where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool FindLicenseByID(int InternationalLicenseID,ref int ApplicationID,ref int DriverID,
          ref   int IssuedUsingLocalLicenseID,ref DateTime IssueDate,
           ref  DateTime ExpirationDate,ref short IsActive,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from InternationalLicenses Where InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    InternationalLicenseID = Convert.ToInt32(reader["InternationalLicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
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

        public static bool FindLicenseByApplicationID(ref int InternationalLicenseID, int ApplicationID, ref int DriverID,
          ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate,
           ref DateTime ExpirationDate, ref short IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from InternationalLicenses Where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    InternationalLicenseID = Convert.ToInt32(reader["InternationalLicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
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

        public static bool FindLicenseByLocalLicenseID(ref int InternationalLicenseID, ref int ApplicationID, ref int DriverID,
           int IssuedUsingLocalLicenseID, ref DateTime IssueDate,
           ref DateTime ExpirationDate, ref short IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from InternationalLicenses Where IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    InternationalLicenseID = Convert.ToInt32(reader["InternationalLicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
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

    }
}

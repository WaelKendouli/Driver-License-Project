using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
using static System.Net.Mime.MediaTypeNames;
namespace LicencesDataAccessClass
{
    public class clsLicenceDataAccess
    {
        public static int AddNewLisenceClass(int ApplicationID , int DriverID , int LicenseClass ,
            DateTime IssueDate , DateTime ExpirationDate , string Notes
            , float PaidFees , short IsActive ,short IssueReason , int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[Licenses]
           ([ApplicationID]
           ,[DriverID]
           ,[LicenseClass]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[Notes]
           ,[PaidFees]
           ,[IsActive]
           ,[IssueReason]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@LicenseClass
           ,@IssueDate
           ,@ExpirationDate
           ,@Notes
           ,@PaidFees
           ,@IsActive
           ,@IssueReason
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
                else
                {
                    LicenseID = -1;
                }

            }
            catch (Exception ex)
            {
                LicenseID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return LicenseID;

        }

        public static bool UpdateLicence(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes
            , float PaidFees, short IsActive, short IssueReason, int CreatedByUserID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[Licenses]
   SET [ApplicationID] = @ApplicationID
      ,[DriverID] = @DriverID
      ,[LicenseClass] = @LicenseClass
      ,[IssueDate] = @IssueDate
      ,[ExpirationDate] = @ExpirationDate
      ,[Notes] = @Notes
      ,[PaidFees] = @PaidFees
      ,[IsActive] = @IsActive
      ,[IssueReason] = @IssueReason
      ,[CreatedByUserID] = @CreatedByUserID
 WHERE LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@DriverID", DriverID);
            Command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            Command.Parameters.AddWithValue("@IssueDate", IssueDate);
            Command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@IsActive", IsActive);
            Command.Parameters.AddWithValue("@IssueReason", IssueReason);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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


        public static bool DisactivateLicense(int LicenseID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Update Licenses
set IsActive = 0
where  Licenses.LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);


 

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

        public static bool FindLicenseByID( int LicenseID,ref int ApplicationID, ref int DriverID, ref int LicenseClass,
           ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes
            , ref float PaidFees, ref short IsActive, ref short IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from Licenses Where LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
                    IssueReason = Convert.ToInt16(reader["IssueReason"]);
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

        public static bool FindLicenseByApplicationID(ref int LicenseID, int ApplicationID,ref int DriverID,ref int LicenseClass,
           ref DateTime IssueDate,ref DateTime ExpirationDate,ref string Notes
            ,ref float PaidFees,ref short IsActive,ref short IssueReason,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from Licenses Where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    if (reader["Notes"]==DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];  
                    }
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
                    IssueReason = Convert.ToInt16(reader["IssueReason"]);
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


        public static bool FindLicenseByDriverID(ref int LicenseID,ref int ApplicationID,  int DriverID, ref int LicenseClass,
           ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes
            , ref float PaidFees, ref short IsActive, ref short IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = "Select * from Licenses Where DriverID = @DriverID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);



            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];
                    }
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsActive = Convert.ToInt16(reader["IsActive"]);
                    IssueReason = Convert.ToInt16(reader["IssueReason"]);
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

        public static bool DeleteLicenseByID(int LicenseID)
        {
            bool isDeleted = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"DELETE FROM Licenses Where LicenseID = @LicenseID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);

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


        public static bool isLicenseExist(int ApplicationID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT W=11 From Licenses
where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);



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


        


    }


    
}

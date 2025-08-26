using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
using static System.Net.Mime.MediaTypeNames;
namespace DetainedLicensesDataAccess
{
    public class clsDetainedLicensesDataAccess
    {
        public static int AddNewDetainedLicence(int LicenseID , DateTime DetainDate ,
            float FineFees , int CreatedByUserID , short IsReleased ,DateTime ReleaseDate , int ReleasedByUserID ,int ReleaseApplicationID)
        {
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            int DetainID = -1;
            string Query = @"INSERT INTO [dbo].[DetainedLicenses]
    ([LicenseID]
    ,[DetainDate]
    ,[FineFees]
    ,[CreatedByUserID]
    ,[IsReleased]
    ,[ReleaseDate]
    ,[ReleasedByUserID]
    ,[ReleaseApplicationID])
VALUES
    (@LicenseID
    ,@DetainDate
    ,@FineFees
    ,@CreatedByUserID
    ,@IsReleased
    ,CASE WHEN @ReleaseDate < GETDATE() THEN NULL ELSE @ReleaseDate END
    ,CASE WHEN @ReleasedByUserID = -1 THEN NULL ELSE @ReleasedByUserID END
    ,CASE WHEN @ReleaseApplicationID = -1 THEN NULL ELSE @ReleaseApplicationID END
    );
SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@LicenseID", LicenseID);
            Command.Parameters.AddWithValue("@DetainDate", DetainDate);
            Command.Parameters.AddWithValue("@FineFees", FineFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsReleased", IsReleased);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);



            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DetainID = insertedID;
                }
                else
                {
                    DetainID = -1;
                }

            }
            catch (Exception ex)
            {
                DetainID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return DetainID;

        }

        public static bool FindDetainedLicenseByLicenseID(int LicenseID ,ref int DetainID,ref DateTime DetainDate,
           ref float FineFees,ref int CreatedByUserID,ref short IsReleased,
           ref DateTime ReleaseDate,ref int ReleasedByUserID,ref int ReleaseApplicationID)
        {
            

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT * FROM DetainedLicenses
WHERE DetainedLicenses.LicenseID = @LicenseID and DetainedLicenses.IsReleased = 0";
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
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = Convert.ToInt16(reader["IsReleased"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    
                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleaseDate = DateTime.MinValue;
                    }
                    else
                    {
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]);
                    }

                    if (reader["ReleasedByUserID"]!=DBNull.Value)
                    {
                        ReleasedByUserID = Convert.ToInt32(reader["ReleasedByUserID"]);
                    }
                    else
                    {
                        ReleasedByUserID = -1;
                    }

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                    {
                        ReleaseApplicationID = Convert.ToInt32(reader["ReleaseApplicationID"]);
                    }
                    else
                    {
                        ReleaseApplicationID = -1;
                    }

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

        public static bool isLicenseDetained(int LicenseID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT W=5 FROM DetainedLicenses
WHERE DetainedLicenses.LicenseID = @LicenseID and IsReleased=0";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
        public static bool isLicenseDetainedByDetainID(int LicenseID , int DetainID)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT W=5 FROM DetainedLicenses
WHERE DetainedLicenses.LicenseID = @LicenseID and IsReleased=0 and DetainedLicenses.DetainID = @DetainID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static bool ReleaseLicense(int DetainID, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[DetainedLicenses]
   SET 
      [IsReleased] = 1
      ,[ReleaseDate] = @ReleaseDate
      ,[ReleasedByUserID] = @ReleasedByUserID
      ,[ReleaseApplicationID] = @ReleaseApplicationID
 WHERE DetainID = @DetainID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            Command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            Command.Parameters.AddWithValue("@DetainID", DetainID);

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

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"SELECT dbo.DetainedLicenses.DetainID, dbo.DetainedLicenses.LicenseID, dbo.DetainedLicenses.DetainDate, dbo.DetainedLicenses.IsReleased, dbo.DetainedLicenses.FineFees, dbo.DetainedLicenses.ReleaseDate, dbo.People.NationalNo, 
                 ISNULL(dbo.People.FirstName , '') +' '+ ISNULL(dbo.People.SecondName , '')+' '+ ISNULL(dbo.People.ThirdName , '')+' '+ ISNULL(dbo.People.LastName , '') as FullName, dbo.DetainedLicenses.ReleaseApplicationID
FROM     dbo.DetainedLicenses INNER JOIN
                  Licenses ON dbo.DetainedLicenses.LicenseID = dbo.Licenses.LicenseID INNER JOIN
				  Applications On Applications.ApplicationID = Licenses.ApplicationID INNER JOIN
                  dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
order by DetainID desc";
            SqlCommand Command = new SqlCommand(query, connection);

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

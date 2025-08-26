using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
using static System.Net.Mime.MediaTypeNames;
namespace TestsDataAccessLayer
{
    public class clsTestsDataAccess
    {

        public static bool CheckIfPassedAnExamBefore( int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isPassed = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select W=8 from Tests inner join TestAppointments
on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
Where TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestAppointments.TestTypeID=@TestTypeID and Tests.TestResult = 1";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isPassed = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isPassed = false;
            }
            finally
            {
                connection.Close();
            }
            return isPassed;
        }


        public static int FindTest(ref int TestAppointmentID,ref short TestResult,ref string Notes, ref int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT * FROM Tests
Where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestID = Convert.ToInt32(reader["TestID"]);
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToInt16(reader["TestResult"]);
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = Convert.ToString(reader["Notes"]); 
                    }
                    
                   
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    reader.Close();
                }
                else
                {
                    TestID = -1;

                }

            }
            catch (Exception ex)
            {
                TestID = -1;
            }
            finally
            {

                connection.Close();
            }
            return TestID;
        }

        public static int AddNewTest( int TestAppointmentID, short TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
                else
                {
                    TestID = -1;
                }

            }
            catch (Exception ex)
            {
                TestID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return TestID;


        }


        public static bool UpdateTest(int TestID, int TestAppointmentID, short TestResult, string Notes, int CreatedByUserID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[Tests]
   SET [TestAppointmentID] = @TestAppointmentID
      ,[TestResult] = @TestResult
      ,[Notes] = @Notes
      ,[CreatedByUserID] = @CreatedByUserID
 WHERE TestID = @TestID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestResult);
            Command.Parameters.AddWithValue("@Notes", Notes);
            Command.Parameters.AddWithValue("@TestID", TestID);
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


        public static bool CheckIfFailedBefore(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            bool isFailedBefore = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select W=10 from TestAppointments 
inner join Tests on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestAppointments.TestTypeID = @TestTypeID and Tests.TestResult = 0";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFailedBefore = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFailedBefore = false;
            }
            finally
            {
                connection.Close();
            }
            return isFailedBefore;

        }

    }
}

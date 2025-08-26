using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionToMyDataBase;
namespace TestApointmentsDataAccess
{
    public class clsTestApointmentsDataAccess
    {
        public static DataTable GetTestApointments(int LocalDrivingLicenseApplicationID , string TestTypeTitle)
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string query = @"Select * from TestAppointments_View 
Where TestAppointments_View.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
and TestAppointments_View.TestTypeTitle = @TestTypeTitle";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);

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

        public static bool FindTestAppointmentByID(int TestAppointmentID, ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,
          ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref short IsLocked)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"SELECT * FROM TestAppointments
Where TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsLocked = Convert.ToInt16(reader["IsLocked"]);
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


        public static int FindTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
          ref DateTime AppointmentDate,ref float PaidFees,ref int CreatedByUserID,ref short IsLocked)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select * from TestAppointments
Where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID = @TestTypeID and IsLocked = 0;";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsLocked = Convert.ToInt16(reader["IsLocked"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    reader.Close();
                }
                else
                {
                    TestAppointmentID = -1;

                }

            }
            catch (Exception ex)
            {
                TestAppointmentID = -1;
            }
            finally
            {

                connection.Close();
            }
            return TestAppointmentID;
        }

        public static bool CheckIfThereIsAnActiveApointment(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select X=1 From TestAppointments inner join LocalDrivingLicenseApplications
on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
inner join TestTypes on TestAppointments.TestTypeID = TestTypes.TestTypeID 
 Where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
 and TestAppointments.TestTypeID = @TestTypeID and TestAppointments.IsLocked = 0";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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

        public static int AddNewAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
          DateTime AppointmentDate, float PaidFees, int CreatedByUserID, short IsLocked)
        {
            int AppointmentID = -1;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);
            string Query = @"INSERT INTO [dbo].[TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked])
     VALUES
           (@TestTypeID
           ,@LocalDrivingLicenseApplicationID
           ,@AppointmentDate
           ,@PaidFees
           ,@CreatedByUserID
           ,@IsLocked);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsLocked", IsLocked);



            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    AppointmentID = insertedID;
                }
                else
                {
                    AppointmentID = -1;
                }

            }
            catch (Exception ex)
            {
                AppointmentID = -1;

            }
            finally
            {
                Connection.Close();

            }
            return AppointmentID;

        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
           DateTime AppointmentDate, float PaidFees, int CreatedByUserID, short IsLocked)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[TestAppointments]
   SET [TestTypeID] = @TestTypeID
      ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
      ,[AppointmentDate] = @AppointmentDate
      ,[PaidFees] = @PaidFees
      ,[CreatedByUserID] = @CreatedByUserID
      ,[IsLocked] = @IsLocked
 WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@IsLocked", IsLocked);


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
        public static bool LockTestAppointment(int TestAppointmentID)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"UPDATE [dbo].[TestAppointments]
   SET 
      [IsLocked] = 1
 WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

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

        public static bool CheckIfTestAppointmentIsLocked(int TestAppointmentID)
        {
            bool isLocked = false;
            SqlConnection connection = new SqlConnection(clsConnection.MyConnectionString);

            string Query = @"Select W=10 from Tests inner join TestAppointments
on Tests.TestAppointmentID = TestAppointments.TestAppointmentID
Where Tests.TestAppointmentID = @TestAppointmentID  and  IsLocked=1";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isLocked = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isLocked = false;
            }
            finally
            {
                connection.Close();
            }
            return isLocked;
        }


    }

}

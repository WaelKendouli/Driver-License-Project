using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTypesBusinessLayer;
namespace ManageTestTypesDataAccess
{
    //SELECT * FROM TestTypes
    public class clsTestTypesDataAccess
    {
        public static string ConnectionString = "Server=.;Database=DVLD;Integrated Security=True;";

        public static DataTable GetAllTestTypes()
        {
            DataTable AppTypes = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"SELECT * FROM TestTypes";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    AppTypes.Load(reader);
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
            return AppTypes;
        }

        public static bool FindTestTypeByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                Connetion.Open();
                //ApplicationFees
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);
                    

                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connetion.Close();
                
            }
            return isFound;
        }

        public static bool FindTestTypeByTitle(ref int TestTypeID,  string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isFound = false;
            SqlConnection Connetion = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM TestTypes WHERE TestTypeTitle = @TestTypeTitle";
            SqlCommand command = new SqlCommand(Query, Connetion);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                Connetion.Open();
                //ApplicationFees
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);


                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                Connetion.Close();

            }
            return isFound;
        }

        public static bool UpdateTestType(int TestTypeID, clsTestType TestType)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE [dbo].[TestTypes]
   SET [TestTypeTitle] =  @TestTypeTitle
      ,[TestTypeDescription] = @TestTypeDescription
      ,[TestTypeFees] = @TestTypeFees
 WHERE TestTypeID = @TestTypeID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            Command.Parameters.AddWithValue("@TestTypeDescription", TestType.TestTypeDescription);
            Command.Parameters.AddWithValue("@TestTypeTitle", TestType.TestTypeTitle);
            Command.Parameters.AddWithValue("@TestTypeFees", TestType.TestTypeFees);


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

    }
}

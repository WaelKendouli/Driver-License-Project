using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersBussinessLayer;
namespace UsersDataAccess
{
    public class clsUsersDataAccess
    {
        private static string ConnectionString = "Server=.;Database=DVLD;Integrated Security=True;";

        public static bool FindUser(ref int UserID , ref string UserName ,ref string Password ,ref short isActive , ref int PersonID )
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID" , UserID);
            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(Reader["UserID"]);
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    isActive = Convert.ToInt16(Reader["isActive"]);
                    PersonID = Convert.ToInt32(Reader["PersonID"]);

                }
                Reader.Close();
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

        public static bool FindUserByName(ref int UserID, ref string UserName, ref string Password, ref short isActive, ref short PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(Reader["UserID"]);
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    isActive = Convert.ToInt16(Reader["isActive"]);
                    PersonID = Convert.ToInt16(Reader["PersonID"]);

                }
                Reader.Close();
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
        public static bool FindUserByID(ref int UserID, ref string UserName, ref string Password, ref short isActive, ref short PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    isFound = true;
                    UserID = Convert.ToInt32(Reader["UserID"]);
                    UserName = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    isActive = Convert.ToInt16(Reader["isActive"]);
                    PersonID = Convert.ToInt16(Reader["PersonID"]);

                }
                Reader.Close();
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
        public static DataTable GetAllUsers()
        {

            DataTable People = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select Users.UserID , Users.PersonID , FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName+ ' ' + People.LastName , Users.UserName , Users.IsActive from
Users inner join  People on Users.PersonID = People.PersonID";
            SqlCommand Command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    People.Load(reader);
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
            return People;

        }

        public static DataTable FilterContentBy(string FilteringMethod, string Filter)
        {
            DataTable Users = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = $"SELECT * FROM Users WHERE [{FilteringMethod}] = @Filter";
            SqlCommand Command = new SqlCommand(query, connection);
            Command.Parameters.AddWithValue("@FilteringMethod", FilteringMethod);
            Command.Parameters.AddWithValue("@Filter", Filter);

            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    Users.Load(reader);
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
            return Users;
        }

        public static DataTable GetAllActiveUsers(int ActiveStatus)
        {

            DataTable People = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionString);

            string query = @"select Users.UserID , Users.PersonID , FullName = People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName+ ' ' + People.LastName , Users.Password , Users.IsActive from
Users inner join  People on Users.PersonID = People.PersonID
WHERE isActive = @ActiveStatus";
            SqlCommand Command = new SqlCommand(query, connection);

            Command.Parameters.AddWithValue("@ActiveStatus", ActiveStatus);
            try
            {
                connection.Open();
                SqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    People.Load(reader);
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
            return People;

        }


        public static bool AddNewUser(ref clsUser User)
        {
            bool isAdded = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);
            string Query = @"INSERT INTO [dbo].[Users]
           ([PersonID]
           ,[UserName]
           ,[Password]
           ,[IsActive])
     VALUES
          ( @PersonID
           ,@UserName
           ,@Password
           ,@IsActive);SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", User.PersonID);
            Command.Parameters.AddWithValue("@UserName", User.UserName);
            Command.Parameters.AddWithValue("@Password", User.Password);
            Command.Parameters.AddWithValue("@IsActive", User.isActive);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    isAdded = true;
                }
                else
                {
                    isAdded = false;
                }

            }
            catch (Exception ex)
            {
                isAdded = false;
            }
            finally
            {
                Connection.Close();

            }
            return isAdded;


        }

        public static bool UpdateUser(int PersonID, ref clsUser User)
        {
            bool isUpdated = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"UPDATE [dbo].[Users]
   SET [PersonID] = @PersonID 
      ,[UserName] = @UserName
      ,[Password] = @Password
      ,[IsActive] = @IsActive
 WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", User.UserName);
            Command.Parameters.AddWithValue("@Password", User.Password);
            Command.Parameters.AddWithValue("@IsActive", User.isActive);
            

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


        public static bool isUserExist(int PersonID)
        {
            bool isExsist = false;
            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = @"Select x=4 FROM Users WHERE PersonID = @PersonID ";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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

        public static bool DeleteAUser(int PersonID)
        {
            bool isDeleted = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = @"DELETE FROM Users Where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

    }


}
    
    


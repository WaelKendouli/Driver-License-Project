using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersDataAccess;
namespace UsersBussinessLayer
{
    public class clsUser
    {
        private enum enMode
        { eAddNew , eUpdate }
        private enMode _Mode = enMode.eAddNew; 
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public short isActive { get; set; }
        public int PersonID { get; set; }

        public clsUser()
        {
            _Mode = enMode.eAddNew;
            UserID = -1;
            UserName = "";
            Password = "";
            isActive = 0;
            PersonID = -1;
        }
        private clsUser(int UserID, string UserName, string Password, short isActive, int PersonID)
        {
            _Mode = enMode.eUpdate;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;
            this.PersonID = PersonID;
        }
        public static bool AddNewUser( clsUser NewUser)
        {
            if (clsUsersDataAccess.AddNewUser(ref NewUser))
            {
                return true;
            }
            return false;
        }
        public static clsUser FindUserByName(string UserName)
        {
            int UserID = 0;  string Password = ""; short isActive = 0; short PersonID = -1;
            if (clsUsersDataAccess.FindUserByName(ref UserID , ref UserName , ref Password , ref isActive , ref PersonID))
            {
                return new clsUser(UserID, UserName, Password, isActive, PersonID);
            }
            return null; 
        }

        public static bool IsUserExist(int PersonID)
        {
            return clsUsersDataAccess.isUserExist(PersonID);
        }

        public static clsUser FindUserByID(int UserID)
        {
            string UserName = ""; string Password = ""; short isActive = 0; short PersonID = -1;
            if (clsUsersDataAccess.FindUserByID(ref UserID, ref UserName, ref Password, ref isActive, ref PersonID))
            {
                return new clsUser(UserID, UserName, Password, isActive, PersonID);
            }
            return null;
        }

        public static bool IsUserHavePermission(string txtUserName , string txtPassword , clsUser User)
        {
            return (txtUserName == User.UserName && txtPassword == User.Password);
        }
        public bool isUserActive()
        {
            return (this.isActive != 0);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }

        public static DataTable FilterContentBy(string FilterMethod , string Filter)
        {
            return clsUsersDataAccess.FilterContentBy(FilterMethod, Filter);
        }

        public static DataTable GetAllActiveAndnonActiveUsers(int ActiveStatus)
        {
            return clsUsersDataAccess.GetAllActiveUsers(ActiveStatus);
        }
        public static clsUser FindUserByPersonID(int PersonID)
        {
            int UserID = 0; string UserName = ""; string Password = ""; short isActive = 0;
            if (clsUsersDataAccess.FindUser(ref UserID ,ref UserName , ref Password , ref isActive , ref PersonID ))
            {
                return new clsUser(UserID, UserName, Password, isActive, PersonID);
            }
            else
            {
                return new clsUser();
            }
        }
        public static bool UpdateUser(int PersonID ,clsUser User)
        {
            return clsUsersDataAccess.UpdateUser(PersonID, ref User);
        }

        public static bool DeleteUser(int PersonID)
        {
            return clsUsersDataAccess.DeleteAUser(PersonID);
        }

        public bool Save()
        {
            if (_Mode==enMode.eAddNew)
            {
                this._Mode = enMode.eUpdate;
                return AddNewUser(this);
            }
            else
            {
                return UpdateUser(this.PersonID, this);
            }
        }

    }
}

using ManageTestTypesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTypesBusinessLayer
{
    public class clsTestType
    {
        enum enMode { eAddNew , eUpdate }
        private enMode _Mode;
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestType()
        {
            _Mode = enMode.eAddNew;
            TestTypeID = -1;
            TestTypeTitle = "";
            TestTypeDescription = "";
            TestTypeFees = 0;
        }
        public clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            _Mode = enMode.eUpdate;
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static clsTestType FindTestTypeByID(int  TestTypeID)
        {
          string  TestTypeTitle = "";
          string  TestTypeDescription = "";
           float TestTypeFees = 0;
            if (clsTestTypesDataAccess.FindTestTypeByID(TestTypeID , ref TestTypeTitle , ref TestTypeDescription , ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }


        public static clsTestType FindTestTypeByTitle(string TestTypeTitle)
        {
            int TestTypeID = 0;
            string TestTypeDescription = "";
            float TestTypeFees = 0;
            if (clsTestTypesDataAccess.FindTestTypeByTitle(ref TestTypeID, TestTypeTitle, ref TestTypeDescription, ref TestTypeFees))
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.GetAllTestTypes();
        }

        public bool Save()
        {
            return clsTestTypesDataAccess.UpdateTestType(this.TestTypeID, this);
        }

    }
}

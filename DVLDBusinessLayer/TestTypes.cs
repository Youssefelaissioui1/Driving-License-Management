using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class TestTypes
    {
        

        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }

        public double TestTypeFees { set; get; }

        public TestTypes(int TestTypeID, string TestTypeTitle,string TestTypeDescription, double TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }



        public bool UpdateTestTypes()
        {
            //call DataAccess Layer 

            return DVLDDataAccessLayer.TestTypes.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);

        }

        public static DataTable GetTestTypes()
        {
            return DVLDDataAccessLayer.TestTypes.GetAllTestTypes();

        }



        public static TestTypes FindTestTypesByID(int TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription="";
            double TestTypeFees = 0.0;


            if (DVLDDataAccessLayer.TestTypes.GetInfoTestTypesID(TestTypeID, ref TestTypeTitle,ref TestTypeDescription, ref TestTypeFees))

                return new TestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class Tests
    {
       public int TestID;
       public   int TestAppointmentID;
        public bool TestResult;
        public string Notes;
        public int CreatedByUserID;

       public Tests()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult =false;
            Notes = "";
            CreatedByUserID = -1;
        }
        public Tests(int TestID,int TestAppointmentID, bool TestResult, string Notes,int CreatedByUserID )
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }



                public bool _AddNewTests()
        {
            //call DataAccess Layer 

            this.TestID = clsTests.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes,
                this.CreatedByUserID);

            return (this.TestID != -1);
        }


        public static Tests GetTestbyID(int TestAppointmentID)
        {

            int TestID = -1;
            bool TestResult =false;
            string Notes = "";
            int CreatedByUserID = -1;




            if (clsTests.GetTestsInfoByID(ref TestID,  TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))

                return new Tests(TestID,TestAppointmentID,TestResult,Notes, CreatedByUserID);
            else
                return null;

        }


        public bool isExist()
        {
            int TestAppointmentID = -1;

            if (clsTests.IsTestExistID(this.TestAppointmentID))
                return true;
            else
                return false;



        }

        public bool IsTestExistIDAndIsPassofVision(int TestAppointmentID)
        {
             //TestAppointmentID = -1;

            if (clsTests.IsTestExistIDAndIsPassofVision(TestAppointmentID))
                return true;
            else
                return false;
        }

        public bool IsTestExistIDAndIsPassofWriting(int TestAppointmentID)
        {
            //TestAppointmentID = -1;

            if (clsTests.IsTestExistIDAndIsPassofWriting(TestAppointmentID))
                return true;
            else
                return false;
        }

        public bool IsTestExistIDAndIsPassofStreet(int TestAppointmentID)
        {
            //TestAppointmentID = -1;

            if (clsTests.IsTestExistIDAndIsPassofStreet(TestAppointmentID))
                return true;
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class TestAppointment
    {

        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }
        public int TestAppointmentID { set; get; }
        public int testTypeID { set; get; }
        public int localDrivingLicenseApplicationID { set; get; }


        public DateTime appointmentDate { set; get; }
        public decimal paidFees { set; get; }
        public int createdByUserID { set; get; }

        public bool isLocked { set; get; }

        public TestAppointment(int TestAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate,
            decimal paidFees,int createdByUserID,bool isLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.testTypeID = testTypeID;
            this.localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.appointmentDate = appointmentDate;
            this.paidFees = paidFees;
            this.createdByUserID = createdByUserID;
            this.isLocked = isLocked;


        }

        public TestAppointment() { }


        public bool AddTestAppointmentAndGetID()
        {
            this.TestAppointmentID = clsTestAppointments.AddTestAppointmentAndGetID(this.testTypeID, this.localDrivingLicenseApplicationID,
                this.appointmentDate, this.paidFees, this.createdByUserID, this.isLocked);
            return (this.TestAppointmentID != -1);
        }

        

        public static DataTable GetAllTestOfVision(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfVision(LocalDrivingLicenseApplicationID);

        }
        public static int GetAllTestOfVision_Count(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfVision_Count(LocalDrivingLicenseApplicationID);

        }
        public static DataTable GetAllTestOfWriting(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfWriting(LocalDrivingLicenseApplicationID);

        }

        public static int GetAllTestOfWriting_Count(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfWriting_Count(LocalDrivingLicenseApplicationID);

        }

        public static DataTable GetAllTestOfStreet(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfStreet(LocalDrivingLicenseApplicationID);

        }
        public static int GetAllTestOfStreet_Count(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetAllTestOfStreet_Count(LocalDrivingLicenseApplicationID);

        }




        public static DataRow GetInfoTestID(int LocalDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.GetInfoTestID(LocalDrivingLicenseApplicationID);

        }
        public static TestAppointment GetInfoTestID2(int localDrivingLicenseApplicationID)
        {
            int testAppointmentID = -1, testTypeID = -1, createdByUserID = -1;
            DateTime appointmentDate = DateTime.MinValue;
            decimal paidFees = 0;
            bool isLocked = false;

            if (clsTestAppointments.GetInfoTestID2(localDrivingLicenseApplicationID, ref testAppointmentID, ref testTypeID, ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked))
            {
                return new TestAppointment(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked);
            }
            else
            {
                return null;
            }
        }

        public static bool  isExist(int localDrivingLicenseApplicationID)
        {
            return DVLDDataAccessLayer.clsTestAppointments.isExist(localDrivingLicenseApplicationID);

        }


        public bool UpdateTestAppointments(int TestAppointmentID,bool IsLocked,DateTime appointmentDate)
        {
            //call DataAccess Layer 

            return DVLDDataAccessLayer.clsTestAppointments.UpdateTestAppointments(TestAppointmentID, IsLocked,appointmentDate);

        }


        public static TestAppointment GetInfoTestIDtest(int testAppointmentID)
        {
            int localDrivingLicenseApplicationID = -1, testTypeID = -1, createdByUserID = -1;
            DateTime appointmentDate = DateTime.MinValue;
            decimal paidFees = 0;
            bool isLocked = false;

            if (clsTestAppointments.GetInfoTestIDtest(ref localDrivingLicenseApplicationID,  testAppointmentID, ref testTypeID, ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked))
            {
                return new TestAppointment(testAppointmentID, testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked);
            }
            else
            {
                return null;
            }
        }
    }
}

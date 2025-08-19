using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DVLDBusinessLayer;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class Licence
    {
     public int LicenseID;
     public  int ApplicationID;
     public  int DriverID;
     public  int LicenseClass;
     public  DateTime IssueDate;
     public  DateTime ExpirationDate;
     public  string Notes;
     public  float PaidFees;
     public  bool IsActive;
     public  short IssueReason;
     public  int CreatedByUserID;


        public Licence()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate=DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = "";
            this.PaidFees= 0;
            this.IsActive = false;
            this.IssueReason = -1;
            this.CreatedByUserID = -1;

        }

        public Licence(int LicenseID, int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,DateTime ExpirationDate,
            string Notes,float PaidFees,bool IsActive,short IssueReason,int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID =ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

        }


        public bool AddNewLicence()
        {
            //call DataAccess Layer 

            this.LicenseID = clsLicences.AddNewLicence(this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,this.IsActive,this.IssueReason,this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        public static bool IsLicenseExist(int ID)
        {
            return DVLDDataAccessLayer.clsLicences.IsLicenseExist(ID);
        }



        public static Licence GetLicenseInfoByApplicationID(int ApplicationID)
        {
            int LicenseID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = null;
            float PaidFees = 0f;
            bool IsActive = false;
            short IssueReason = -1;
            int CreatedByUserID = -1;

            bool found = clsLicences.GetLicenseInfoByApplicationID(
                ApplicationID,ref LicenseID, ref driverID, ref licenseClass,ref IssueDate,ref ExpirationDate,
                ref Notes,ref PaidFees,ref IsActive,ref IssueReason, ref CreatedByUserID);

            if (!found)
                return null; 

            Licence licence = new Licence
            {
                LicenseID = LicenseID,
                ApplicationID = ApplicationID,
                DriverID = driverID,
                LicenseClass = licenseClass,
                IssueDate = IssueDate,
                ExpirationDate = ExpirationDate,
                Notes = Notes,
                PaidFees = PaidFees,
                IsActive = IsActive,
                IssueReason = (short)IssueReason,
                CreatedByUserID = CreatedByUserID
            };

            return licence;
        }


        public static DataTable GetAllLicense_Of_Person(int PersonID)
        {
            return DVLDDataAccessLayer.clsLicences.GetAllLicense_Of_Person(PersonID);

        }

        public static DataTable GetAllInternationalLicense(int driverID)
        {
            return DVLDDataAccessLayer.clsLicences.GetAllInternationalLicense(driverID);

        }

        public static Licence GetLicenseInfoByLicenseID(int LicenseID)
        {
            int ApplicationID = -1;
            int driverID = -1;
            int licenseClass = -1;
            DateTime IssueDate = DateTime.MinValue;
            DateTime ExpirationDate = DateTime.MinValue;
            string Notes = null;
            float PaidFees = 0f;
            bool IsActive = false;
            short IssueReason = -1;
            int CreatedByUserID = -1;

            bool found = clsLicences.GetLicenseInfoByLicenseID(
                ref ApplicationID,  LicenseID, ref driverID, ref licenseClass, ref IssueDate, ref ExpirationDate,
                ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID);

            if (!found)
                return null;

            Licence licence = new Licence
            {
                LicenseID = LicenseID,
                ApplicationID = ApplicationID,
                DriverID = driverID,
                LicenseClass = licenseClass,
                IssueDate = IssueDate,
                ExpirationDate = ExpirationDate,
                Notes = Notes,
                PaidFees = PaidFees,
                IsActive = IsActive,
                IssueReason = (short)IssueReason,
                CreatedByUserID = CreatedByUserID
            };

            return licence;
        }

        public static bool UpdateIsActiveLIcenseToFalse(int ID)
        {
            return DVLDDataAccessLayer.clsLicences.UpdateIsActiveLIcenseToFalse(ID);
        }

        public static bool UpdateIsDetainedLIcenseToTrue(int ID)
        {
            return DVLDDataAccessLayer.clsLicences.UpdateIsDetainedLIcenseToTrue(ID);
        }

    }
}



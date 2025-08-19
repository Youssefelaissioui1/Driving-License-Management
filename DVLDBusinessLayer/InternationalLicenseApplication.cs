using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class InternationalLicenseApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; set; } = enMode.AddNew;
        public int InternationalLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public InternationalLicenseApplication()
        {
            InternationalLicenseApplicationID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedUsingLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now.AddYears(1);
            IsActive = true;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }
        public InternationalLicenseApplication(int id, int applicationID, int driverID, int issuedUsingLocalLicenseID,
            DateTime issueDate, DateTime expirationDate, bool isActive, int createdByUserID)
        {
            InternationalLicenseApplicationID = id;
            ApplicationID = applicationID;
            DriverID = driverID;
            IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            IsActive = isActive;
            CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }

        public bool AddNew()
        {
            this.InternationalLicenseApplicationID = DVLDDataAccessLayer.clsInternationalLicenseApplication.AddNewLocalDrivingLicenseApplications(
                this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate,
                this.ExpirationDate, this.IsActive, this.CreatedByUserID);
            return (this.InternationalLicenseApplicationID != -1);
        }

        public static bool IsInternationalLicenseApplicationExist(int DriverID)
        {
            return DVLDDataAccessLayer.clsInternationalLicenseApplication.IsLocalDrivingLicenseApplicationsExist(DriverID);
        }

          public static DataTable GetAll()
        {
            return clsInternationalLicenseApplication.GetAllinternationalLocalDrivingLicenseApplications();
        }
    }
}

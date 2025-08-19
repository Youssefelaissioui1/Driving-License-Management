using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class LocalDrivingLicenseApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode { get; set; } = enMode.AddNew;

        // Properties
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; } // Assuming exists in DB
        public int LicenseClassID { get; set; } // Assuming exists in DB
        public string ClassName { get; set; }
        public string NationalNo { get; set; }
        public string CountryName { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public float PassedTestCount { get; set; }
        public string Status { get; set; }

        public LocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            ClassName = string.Empty;
            NationalNo = string.Empty;
            CountryName = string.Empty;
            FullName = string.Empty;
            ApplicationDate = DateTime.Now;
            PassedTestCount = 0;
            Status = string.Empty;
            Mode = enMode.AddNew;
        }

        public LocalDrivingLicenseApplication(int id, int applicationID, int licenseClassID,
            string className, string nationalNo, string countryName, string fullName,
            DateTime appDate, float passedTests, string status)
        {
            LocalDrivingLicenseApplicationID = id;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;
            ClassName = className;
            NationalNo = nationalNo;
            CountryName = countryName;
            FullName = fullName;
            ApplicationDate = appDate;
            PassedTestCount = passedTests;
            Status = status;
            Mode = enMode.Update;
        }

        public LocalDrivingLicenseApplication(int id, int applicationID, int licenseClassID)
        {
            LocalDrivingLicenseApplicationID = id;
            ApplicationID = applicationID;
            LicenseClassID = licenseClassID;
      
            Mode = enMode.Update;
        }


        public bool AddNew()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplications
                .AddNewLocalDrivingLicenseApplications(this.ApplicationID, this.LicenseClassID);

            return LocalDrivingLicenseApplicationID != -1;
        }

        private bool Update()
        {
            return clsLocalDrivingLicenseApplications
                .UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        public bool Save()
        {
            if (Mode == enMode.AddNew)
            {
                if (AddNew())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
            }
            else
            {
                return Update();
            }
        }

        public static LocalDrivingLicenseApplication Find(int id)
        {
            int appID = -1, licenseClassID = -1;
            string className = "", nationalNo = "", fullName = "", countryName = "", status = "";
            DateTime appDate = DateTime.Now;
            float passedTestCount = 0;

            bool found = clsLocalDrivingLicenseApplications
                .GetLocalDrivingLicenseApplicationsInfoByID(id, ref className, ref nationalNo, ref fullName,
                                                            ref appDate, ref passedTestCount, ref status);

            if (found)
            {
                return new LocalDrivingLicenseApplication(id, appID, licenseClassID, className,
                    nationalNo, countryName, fullName, appDate, passedTestCount, status);
            }

            return null;
        }
        public static LocalDrivingLicenseApplication Find2notview(int id)
        {
            int  ApplicationID=-1, licenseClassID = -1;
       

            bool found = clsLocalDrivingLicenseApplications
                .GetLocalDrivingLicenseApplicationsInfoByIDnotView(id, ref ApplicationID ,ref licenseClassID);

            if (found)
            {
                return new LocalDrivingLicenseApplication(id,  ApplicationID,  licenseClassID);
            }

            return null;
        }

        public static LocalDrivingLicenseApplication FindByNationalNo_AND_Class(string nationalNo,string ClassName)
        {
            int id = -1, appID = -1, licenseClassID = -1;
            string  fullName = "", countryName = "", status = "";
            DateTime appDate = DateTime.Now;
            float passedTestCount = 0;

            bool found = clsLocalDrivingLicenseApplications
                .GetLocalDrivingLicenseApplicationsInfoByNationalNo_ClassName(ref id, ClassName, nationalNo,
                                                                    ref fullName, ref appDate,
                                                                    ref passedTestCount, ref status);

            if (found)
            {
                return new LocalDrivingLicenseApplication(id, appID, licenseClassID, ClassName,
                    nationalNo, countryName, fullName, appDate, passedTestCount, status);
            }

            return null;
        }

        public static DataTable GetAll()
        {
            return clsLocalDrivingLicenseApplications.GetAllLocalDrivingLicenseApplications();
        }

        public static bool Cancel(int id)
        {
            return clsLocalDrivingLicenseApplications.CancelLocalDrivingLicenseApplications(id);
        }
        public static bool Delete(int id)
        {
            return clsLocalDrivingLicenseApplications.DeleteLocalDrivingLicenseApplications(id);
        }

        public static bool Exists(int id)
        {
            return clsLocalDrivingLicenseApplications.IsLocalDrivingLicenseApplicationsExist(id);
        }

        public static bool ExistsByName(string className, string fullName)
        {
            return clsLocalDrivingLicenseApplications.IsLocalDrivingLicenseApplicationsExistByName(className, fullName);
        }


        public static bool IsExistLocalDrivingLicenseApplicationsInfoByNationalNo_ClassNameAnd_Status(string nationalNo, string className)
        {
            return clsLocalDrivingLicenseApplications
                .GetLocalDrivingLicenseApplicationsInfoByNationalNo_ClassNameAnd_Status(
                    nationalNo, className
                );
        }
    }
}

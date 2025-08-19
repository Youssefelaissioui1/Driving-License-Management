using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
 

        public class  Applications {

        public int ApplicationID;
        public int ApplicantPersonID;
        public DateTime ApplicationDate;
        public int ApplicationTypeID;
        public int ApplicationStatus;
        public DateTime LastStatusDate;
        public float PaidFees;
        public int CreatedByUserID;

        public Applications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = -1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = -1;
            this.CreatedByUserID = -1;
        }

        public Applications(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, int applicationStatus, DateTime lastStatusDate, float paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
           this.ApplicationDate = applicationDate;
           this.ApplicationTypeID = applicationTypeID;
           this.ApplicationStatus = applicationStatus;
           this.LastStatusDate = lastStatusDate;
           this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
        }

        public  bool _AddNewApplications()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplications.AddNewApplications(this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,
                this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }
        public static Applications GetApplicationsbyID(int ApplicationsID)
        {

            int ApplicantPersonID = 0;
            DateTime ApplicationDate = DateTime.MinValue;
            int ApplicationTypeID = 0;
            int ApplicationStatus = 0;
            DateTime LastStatusDate = DateTime.MinValue;
            float PaidFees = 0;
            int CreatedByUserID = 0;




            if (clsApplications.GetApplicationsInfoByID(ApplicationsID, ref ApplicantPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees,ref CreatedByUserID))

                return new Applications(ApplicationsID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,  ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID);
            else
                return null;

        }


        public bool isExist()
        {
            int ApplicationsPersonID = -1;
            int ApplicationTypeID = -1;

            if(clsApplications.IsLocalDrivingLicenseApplicationsExistByName(this.ApplicantPersonID,this.ApplicationTypeID))
                return true;
            else
                return false;
        }

        public bool Update()
        {
            int ApplicationID = -1;
            if (clsApplications.UpdateApplicationsStatutToCompleted(this.ApplicationID))
                return true;
            else
                return false;




        }

        }

    }

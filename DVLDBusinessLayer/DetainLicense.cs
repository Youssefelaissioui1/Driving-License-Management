using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class DetainLicense
    {

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFess { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleasedApplicationID { get; set; }

        public DetainLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.MinValue;
            FineFess = 0.0f;
            CreatedByUserID = -10;
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = -1;
            ReleasedApplicationID = -1;
        }
        public DetainLicense(int detainID, int licenseID,DateTime DetainDate, float fineFess, int createdByUserID, bool isReleased, DateTime releaseDate, int releasedByUserID, int releasedApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            this.DetainDate = DetainDate;
            FineFess = fineFess;
            CreatedByUserID = createdByUserID;
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            ReleasedApplicationID = releasedApplicationID;
        }

        public static bool IsLicenseDetained(int ID)
        {
            return DVLDDataAccessLayer.ClsDetaindLicense.IsLicenseDetained(ID);
        }

        public bool AddNewDetainedLicense()
        {
            this.DetainID = DVLDDataAccessLayer.ClsDetaindLicense.InsertDetainedLicense(this.LicenseID,this.DetainDate, this.FineFess, this.CreatedByUserID, this.IsReleased, this.ReleaseDate, this.ReleasedByUserID, this.ReleasedApplicationID);
            return (this.DetainID != -1);
        }

        public static bool ReleaseDetainedLicense(int detainID, DateTime releaseDate, int releasedByUserID, int releaseApplicationID)
        {
            return DVLDDataAccessLayer.ClsDetaindLicense.ReleaseDetainedLicense(detainID, releaseDate, releasedByUserID, releaseApplicationID);
        }

        public static DetainLicense GetDetainedLicenseInfo( int licenseID)
        {
            int detainID = -1;
            DateTime detainDate = DateTime.MinValue;
            float fineFees = 0.0f;
            int createdByUserID = -1;
            DateTime releaseDate = DateTime.MinValue;
            bool isReleased = false;
            int releasedByUserID = -1;
            int releasedApplicationID = -1;
           if(ClsDetaindLicense.GetDetainedLicenseInfo(ref detainID,  licenseID, ref detainDate, ref fineFees, ref createdByUserID, ref isReleased, ref releaseDate, ref releasedByUserID, ref releasedApplicationID))
            {
                return new DetainLicense(detainID, licenseID, detainDate, fineFees, createdByUserID, isReleased, releaseDate, releasedByUserID, releasedApplicationID);
            }
            else
            {
                return null;
            }

             
        }
        public static DataTable GetAll()
        {
        
            return DVLDDataAccessLayer.ClsDetaindLicense.GetAll();

        
    }
    }
}

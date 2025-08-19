using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class LicenseClasses
    {

        //public enum enMode { AddNew = 0, Update = 1 };
        //public enMode Mode = enMode.AddNew;
        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public int MinimumAllowedAge { set; get; }
        public int DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }




        public LicenseClasses()

        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = -1;
            this.DefaultValidityLength = -1;
            this.ClassFees = 0;



            //Mode = enMode.AddNew;

        }

        private LicenseClasses(int LicenseClassID,  string ClassName,  string ClassDescription,  int MinimumAllowedAge,  int DefaultValidityLength,  float ClassFees)

        {
            this.LicenseClassID =LicenseClassID;
            this.ClassName =ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            //Mode = enMode.Update;

        }

        //private bool _AddNewLicenseClasses()
        //{
        //    //call DataAccess Layer 

        //    this.ID = clsLicenseClassesData.AddNewLicenseClasses(this.LicenseClassesName);

        //    return (this.ID != -1);
        //}

        //private bool _UpdateContact()
        //{
        //    //call DataAccess Layer 

        //    return clsLicenseClassesData.UpdateLicenseClasses(this.ID, this.LicenseClassesName);

        //}

        public static LicenseClasses Find(int ID)
        {

            string ClassName = "";
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = 0;



            if (clsLicenseClassesData.GetLicenseClassesInfoByID(ID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new LicenseClasses(ID, ClassName, ClassDescription,  MinimumAllowedAge,  DefaultValidityLength,  ClassFees);
            }
            else
                return null;


        }

        public static LicenseClasses Find(string ClassName)
        {

            int ID = -1;
            string ClassDescription = "";
            int MinimumAllowedAge = -1;
            int DefaultValidityLength = -1;
            float ClassFees = 0;

            if (clsLicenseClassesData.GetLicenseClassInfoByName(ref ID,  ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))

                return new LicenseClasses( ID, ClassName,  ClassDescription,  MinimumAllowedAge,  DefaultValidityLength,  ClassFees);
            else
                return null;

        }


        //public bool Save()
        //{


        //    switch (Mode)
        //    {
        //        case enMode.AddNew:
        //            if (_AddNewLicenseClasses())
        //            {

        //                Mode = enMode.Update;
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        case enMode.Update:

        //            return _UpdateContact();

        //    }

        //    return false;
        //}

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();

        }

        //public static bool DeleteLicenseClasses(int ID)
        //{
        //    return LicenseClasses.DeleteLicenseClasses(ID);
        //}

        //public static bool isLicenseClassesExist(int ID)
        //{
        //    return LicenseClasses.IsLicenseClassesExist(ID);
        //}

        //public static bool isLicenseClassesExist(string LicenseClassesName)
        //{
        //    return LicenseClasses.IsLicenseClassesExist(LicenseClassesName);

        //}


    }
}

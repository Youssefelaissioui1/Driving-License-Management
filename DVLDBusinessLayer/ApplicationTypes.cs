using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class ApplicationTypes
    {
     
        public int ApplicationTypeID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public double ApplicationFees { set; get; }

       public ApplicationTypes(int applicationTypeID,string ApplicationTypeTitle,double ApplicationFees)
        {  this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }
        public ApplicationTypes()
        {
            this.ApplicationTypeID = 0;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0.0;
        }



        public bool UpdateApplicationTypes()
        {
            //call DataAccess Layer 

            return DVLDDataAccessLayer.ApplicationTypes.UpdateApplicationTypes(this.ApplicationTypeID,this.ApplicationTypeTitle,this.ApplicationFees);

        }

        public static DataTable GetApplicationTypes()
        {
            return DVLDDataAccessLayer.ApplicationTypes.GetAllApplicationTypes();

        }



        public static ApplicationTypes FindApplicationTypeByID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle="";
            double ApplicationFees=0.0;


            if (DVLDDataAccessLayer.ApplicationTypes.GetInfoApplicationsTypesID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))

                return new ApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            else
                return null;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class Driver
    {
        public int DriverID;
        public int PersonID;
        public int CreatedByUserID;
        public DateTime CreatedDate;


        public Driver()
        {
            this.DriverID = -1;
            this.PersonID= -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
        }
        public Driver(int driverID, int personID, int CreatedByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = createdDate;
        }

        public static Driver GetInfoDriver(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (DVLDDataAccessLayer.clsDriver.GetInfoDriver(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }

        public static Driver GetInfoDriverByPersonID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;
            if (DVLDDataAccessLayer.clsDriver.GetInfoDriverByPersonID( DriverID,ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new Driver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            else
                return null;
        }


        public bool AddNewDriver()
        {

            this.DriverID = clsDriver.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return (this.DriverID != -1);
        }



        public static bool isDriverExist(int PersonID)
        {
            return DVLDDataAccessLayer.clsDriver.IsDriverExist(PersonID);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriver.GetAllDrivers();

        }
    }
}
